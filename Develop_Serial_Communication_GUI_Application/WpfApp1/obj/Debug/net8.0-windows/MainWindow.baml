   M S B A M L   `   `   `  ����  WpfApp1    WpfApp1.MainWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  Serial Communication��5   	   $	��350��6&   $	��480��63   .��5      �  � RowDefinitions 5   
   �� 5       � ColumnDefinitions 5	   
   �� 5
      $
��208* �6   �� 5      $	��17* �6   .+�5   
   �� $��Choose the serial port:��6   $
��Left=�62   $	��Top=�6M   $��	10,13,0,0q�6e   $��26��6x   $	��129��6�   �� 5   
   -   5         ��Name$ comboBoxPorts��5      $
��Left=�6*   $	��Top=�6E   $	��100��6]   $��
183,15,0,0q�6i   $��22��6}   �� 5   
   $"��Choose the serial baud rate:��6   $
��Left=�67   $	��Top=�6R   $��	10,50,0,0q�6j   $��26��6}   $	��158��6�   �� 5   
   -   5         ��Name$ textBoxBaudRate��5      $
��Left=�6+   $	��Top=�6F   $	��100��6^   $��
183,54,0,0q�6j   $��18��6~   �� 5   
   -   5         ��Name$ buttonConnectDisconnect��5      $��Connect��62   $
��Left=�6D   $	��Top=�6_   $	��100��6w   $��
316,16,0,0q�6�   $��20��6�   �� 5   
   $!��Write decimal data to send:��6   $
��Left=�66   $	��Top=�6Q   $��	10,90,0,0q�6i   $��26��6|   $	��155��6�   �� 5   
   -   5        $ textBoxDataToSend��5      $
��Left=�6-   $	��Top=�6H   $	��100��6`   $��
183,94,0,0q�6l   $��18��6�   �� 5   
   -   5        $��	Send Data��5      $
��Left=�6%   $	��Top=�6@   $	��100��6X   $��
316,93,0,0q�6d   $��20��6�   �� 5   
   -   5        $ textBoxReceivedData��5      $
��Left=�6/   $	��Top=�6J   $	��138��6b   $	��406��6o   $��
10,145,0,0q�6{    �� 
IsReadOnly$
 True��6�   �� 5   
   -   5        $
��Exit��5      $
��Left=�6    $	��Top=�6;   $	��100��6S   $��316,120,0,0q�6_   $��20��6�   A� 5   
   -   5         A�Name$ redLamp��5      
���  ��$��20��6.   $��20��69   $
��Left=�6E   $	��Top=�6`   $��4,15,0,0q�6x   $y�Visible=�6�   $��1��6�   A� 5   
   -	   5        $ 	greenLamp��5      
��� � �$��20��62   $��20��6=   $
��Left=�6I   $	��Top=�6d   $��4,15,0,0q�6|   $y�	Collapsed=�6�   $��1��6�   �� 5!   
   -
   5         ��Name$ statusLabel��5!      $�� ��6%   $
��Left=�60   $	��Top=�6K   $��
10,120,0,0q�6c   $��1��6w   5"      5#      