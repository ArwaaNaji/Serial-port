using System;
using System.IO.Ports;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("-----------Serial Communication-----------:");

        string defaultSerialPort = "COM2";
        int defaultBaudRate = 115200;
        SerialPort serialPort;
        string selectedPort = "";

        Console.Write($"Choose the serial port (default port '{defaultSerialPort}'): ");

        while (true)
        {
            selectedPort = Console.ReadLine();

            if (selectedPort == "COM1" || selectedPort == "COM2" || string.IsNullOrWhiteSpace(selectedPort))
            {
                if (string.IsNullOrWhiteSpace(selectedPort))
                {
                    selectedPort = defaultSerialPort;
                }
                break;
            }
            else
            {
                Console.WriteLine("Select COM1 or COM2");
            }
        }

        Console.Write($"Choose the serial baud rate (default '{defaultBaudRate}'): ");
        string baudrateInput = Console.ReadLine();
        int selectedBaudrate;

        if (string.IsNullOrWhiteSpace(baudrateInput) || !int.TryParse(baudrateInput, out selectedBaudrate))
        {
            selectedBaudrate = defaultBaudRate;
        }

        serialPort = new SerialPort(selectedPort, selectedBaudrate, Parity.None, 8, StopBits.One);
        serialPort.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);

        try
        {
            serialPort.Open();
            Console.WriteLine("Serial port opened successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error opening serial port: {ex.Message}");
            return;
        }

        Console.WriteLine("Enter 'exit' to close the application.");

        while (true)
        {
            Console.Write("Write decimal data to send:");
            string decimalData = Console.ReadLine();

            if (decimalData.ToLower() == "exit")
                break;

            try
            {
                byte[] messageBytes = ConvertDecimalStringToByteArray(decimalData);
                serialPort.Write(messageBytes, 0, messageBytes.Length);
                Console.WriteLine("Message sent: " + BitConverter.ToString(messageBytes).Replace("-", " "));
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid decimal format. Please try again.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending message: {ex.Message}");
            }
        }

        if (serialPort.IsOpen)
        {
            serialPort.Close();
        }
    }

    private static void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
    {
        SerialPort sp = (SerialPort)sender;
        int bytesToRead = sp.BytesToRead;
        byte[] buffer = new byte[bytesToRead];
        sp.Read(buffer, 0, bytesToRead);

        string decimalValues = string.Join(" ", buffer.Select(b => b.ToString()));
        Console.WriteLine("Data Received (as decimal): " + decimalValues);
    }

    private static byte[] ConvertDecimalStringToByteArray(string decimalString)
    {
        if (!int.TryParse(decimalString, out int decimalValue))
        {
            throw new FormatException("Invalid decimal format.");
        }

        byte[] byteArray = BitConverter.GetBytes(decimalValue);

        if (BitConverter.IsLittleEndian)
        {
            Array.Reverse(byteArray);
        }

        int firstNonZeroIndex = Array.FindIndex(byteArray, b => b != 0);

        return byteArray.Skip(firstNonZeroIndex).ToArray();
    }
}
