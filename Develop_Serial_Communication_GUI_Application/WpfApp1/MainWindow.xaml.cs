using System;
using System.IO.Ports;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace WpfApp1
{
    public partial class MainWindow : Window
    {
        private SerialPort serialPort;
        private string defaultSerialPort = "COM2";
        private int defaultBaudRate = 115200;

        public MainWindow()
        {
            InitializeComponent();
            comboBoxPorts.Items.Add("COM1");
            comboBoxPorts.Items.Add("COM2");
            comboBoxPorts.SelectedItem = defaultSerialPort;
            textBoxBaudRate.Text = defaultBaudRate.ToString();
            UpdateLampStatus(false); 
        }

        private void ButtonOpenPort_Click(object sender, RoutedEventArgs e)
        {
            string selectedPort = comboBoxPorts.SelectedItem?.ToString();
            if (string.IsNullOrWhiteSpace(selectedPort))
            {
                selectedPort = defaultSerialPort;
            }

            int selectedBaudRate;
            if (!int.TryParse(textBoxBaudRate.Text, out selectedBaudRate))
            {
                selectedBaudRate = defaultBaudRate;
            }

            serialPort = new SerialPort(selectedPort, selectedBaudRate, Parity.None, 8, StopBits.One);
            serialPort.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);
            
            try
            {
                serialPort.Open();
                UpdateLampStatus(true); 
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening serial port: {ex.Message}");
                UpdateLampStatus(false); 
            }
        }

        private void ButtonSendData_Click(object sender, RoutedEventArgs e)
        {
            if (serialPort == null || !serialPort.IsOpen)
            {
                MessageBox.Show("Serial port is not open.");
                return;
            }

            string decimalData = textBoxDataToSend.Text;
            if (decimalData.ToLower() == "exit")
            {
                Application.Current.Shutdown();
                return;
            }

            try
            {
                byte[] messageBytes = ConvertDecimalStringToByteArray(decimalData);
                serialPort.Write(messageBytes, 0, messageBytes.Length);
                
            }
            catch (FormatException)
            {
                MessageBox.Show("Invalid decimal format. Please try again.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error sending message: {ex.Message}");
            }
        }

        private void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            int bytesToRead = sp.BytesToRead;
            byte[] buffer = new byte[bytesToRead];
            sp.Read(buffer, 0, bytesToRead);

            string newDecimalValues = string.Join(" ", buffer.Select(b => b.ToString()));

            Dispatcher.Invoke(() => textBoxReceivedData.Text += "Data Received : " + newDecimalValues + "\n");
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

        private void ButtonExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void UpdateLampStatus(bool isConnected)
        {
            redLamp.Visibility = isConnected ? Visibility.Collapsed : Visibility.Visible;
            greenLamp.Visibility = isConnected ? Visibility.Visible : Visibility.Collapsed;
        }
    }
}
