using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Drawing;
using System.Windows.Media;
using dfnPortScanner.Properties;
namespace dfnPortScanner
{
    public partial class MainWindow : Window
    {
        private int[] ports;
        private IPAddress address;

        public MainWindow()
        {
            InitializeComponent();
        }

       
        private void clearButton_Click(object sender, RoutedEventArgs e)
        {
            lvOutput.Items.Clear();
        }
        

        private async void scanButton_Click_1(object sender, RoutedEventArgs e)
        {
            if (!ParseAddress())
            {
                MessageBox.Show(@"Failed to parse address");
                return;
            }

            if (!ParsePorts())
            {
                MessageBox.Show(@"Failed to parse port(s)");
                return;
            }

            lvOutput.Items.Clear();

            var portScanner = new PortScan.PortScanner(address, ports);
            portScanner.OnSinglePortScanFinish += (o, args) =>
            {
                Dispatcher.Invoke((Action)(() =>
                {
                    var result = args.PortScanResult;
                    var open = result.Open;
                    var port = result.Port;

                    List<PortScan.PortScanResult> items = new List<PortScan.PortScanResult>();
                    items.Add(new PortScan.PortScanResult() { Open = open, Port = port });
                    foreach(PortScan.PortScanResult i in items)
                    {
                        lvOutput.Items.Add(i);
                    }

                }));

            };

            await portScanner.ScanTaskAsync();
        }

        private void input_TextChanged(object sender, EventArgs e)
        {
            scanButton.IsEnabled = !string.IsNullOrWhiteSpace(ipBox.Text) && !string.IsNullOrWhiteSpace(fromBox.Text) && !string.IsNullOrWhiteSpace(ipBox.Text);
        }

        private bool ParsePorts()
        {
            int starPort = int.Parse(fromBox.Text.Trim());
            int endPort = int.Parse(toBox.Text.Trim());

            ports = Enumerable.Range(starPort, endPort).ToArray();

            return true;
        }

    

        private bool ParseAddress()
        {
            var rawAddress = ipBox.Text.Trim();

            return IPAddress.TryParse(rawAddress, out address);

        }

        private static int? ConvertPort(string raw)
        {
            int result;
            if (!int.TryParse(raw, out result))
            {
                return null;
            }

            return result;
        }

        
    }
}
