using System;
using System.Windows;
using System.Timers;
using System.Windows.Media;

using System.IO.Ports;

namespace ArduinoRGB_LED_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        byte RedVal = 255;
        byte GreenVal = 155;
        byte BlueVal = 55;

        bool end = false;
        Timer timer = new Timer(43);
        SerialPort port;

        public MainWindow()
        {
            InitializeComponent();

            port = new SerialPort("COM3", 9600);
        }

        void UpdateData()
        {
            try
            {
                ColorDisplay.Fill = new SolidColorBrush(Color.FromRgb(RedVal, GreenVal, BlueVal));
                port.Open();
                if (port.IsOpen)
                {
                    string output = GernerateSerialString(RedVal, GreenVal, BlueVal) + '\n';
                    port.Write(output);

                    try
                    {
                        Console.WriteLine(port.ReadLine());
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Error: {0}", e.StackTrace);
                    }

                    Console.WriteLine(output);
                }
                port.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }

        public string GernerateSerialString(byte red, byte green, byte blue)
        {
            return String.Format("{0}:{1}:{2}", red.ToString(), green.ToString(), blue.ToString());
        }

        private void RedSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            RedVal = (byte)e.NewValue;
            UpdateData();
        }

        private void BlueSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            BlueVal = (byte)e.NewValue;
            UpdateData();
        }

        private void GreenSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            GreenVal = (byte)e.NewValue;
            UpdateData();
        }
    }
}
