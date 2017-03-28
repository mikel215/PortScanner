using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace dfnPortScanner
{
    public delegate void SinglePortScanFinishEventHandler(object sender, PortScannerEventArgs e);

    public class PortScan
    {
        private IPAddress address;
        private int[] ports;

        public IPAddress Address
        {
            get { return address; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException(nameof(Address));

                if (value.Equals(IPAddress.Any)
                 || value.Equals(IPAddress.IPv6Any)
                 || value.Equals(IPAddress.None)
                 || value.Equals(IPAddress.IPv6None))
                    throw new ArgumentException(@"IP Address cannot be any.", nameof(Address));

                address = value;
            }
        }

        public int[] Ports
        {
            get { return ports; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException(nameof(value));

                if (value.Length == 0)
                    throw new ArgumentException(@"Cannot be empty.", nameof(value));

                ports = value;
            }
        }

        public event SinglePortScanFinishEventHandler OnSinglePortScanFinish;

        public PortScan(IPAddress address, params int[] ports)
        {
            Address = address;
            Ports = ports;
        }

        public virtual PortScanResult[] Scan()
        {
            return InternalScan();
        }

        public Poe

    }

    // PortScanner event
    public class PortScannerEventArgs : EventArgs
    {
        // PortScanResult Property
        public PortScanResult PortScanResult { get; set; }

        // PortScannerEventArgs method
        public PortScannerEventArgs(PortScanResult result)
        {
            // 
            PortScanResult = result;
        }
    }

    // PortScanResult class
    public class PortScanResult
    {
        //  Port property   
        public int Port { get; set; }

        // Open property
        public bool Open { get; set; }
    }
}