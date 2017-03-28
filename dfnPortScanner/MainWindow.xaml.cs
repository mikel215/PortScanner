using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Drawing;
using System.Windows.Forms;
using dfnPortScanner.Properties;

namespace dfnPortScanner
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int[] ports;
        private IPAddress address;

        public MainWindow()
        {
            InitializeComponent();
        }

        private async void scanButton_Click_1(object sender, RoutedEventArgs e)
        {
            if(!ParseAddress())
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

            var portScanner = new dfnPortScanner.PortScanner
        }

        

        private bool ParsePorts()
        {
            IEnumerable<int> portRange = Enumerable.Range(Convert.ToInt32(fromBox.Text.Trim()), Convert.ToInt32(toBox.Text.Trim()));

            var tempPorts = portRange.ToArray();

            ports = new int[tempPorts.Length];

            for (int i = 0; i < tempPorts.Length; i++)
            {
                var portResult = ConvertPort(tempPorts[i]);
                if (!portResult.HasValue)
                {
                    return false;
                }

                ports[i] = portResult.Value;

            }

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
