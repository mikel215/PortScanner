using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace PortScan
{
    public delegate void SinglePortScanFinishEventHandler(object sender, PortScannerEventArgs e);

    // PortScanner class
    public class PortScanner
    {
       
        // Fields for the address and ports
        private IPAddress address;
        private int[] ports;

        // Address property
        public IPAddress Address
        {
            get { return address; }
            set
            {
                // Throw exceptions if value is empty or IP is invalid
                if (value == null)
                    throw new ArgumentNullException(nameof(Address));

                if (value.Equals(IPAddress.Any)
                 || value.Equals(IPAddress.IPv6Any)
                 || value.Equals(IPAddress.None)
                 || value.Equals(IPAddress.IPv6None))
                    throw new ArgumentException(@"IP address cannot be any.", nameof(Address));

                address = value;
            }
        }

        // Ports property
        public int[] Ports
        {
            get { return ports; }
            set
            {
                // Throw exception if ports field is empty
                if (value == null)
                    throw new ArgumentNullException(nameof(value));

                // Throw exception if array is empty
                if (value.Length == 0)
                    throw new ArgumentException(@"Cannot be empty.", nameof(value));

                ports = value;
            }
        }

        public event SinglePortScanFinishEventHandler OnSinglePortScanFinish;

        // PortScanner method
        public PortScanner(IPAddress address, params int[] ports)
        {
            Address = address;
            Ports = ports;
        }

        public virtual PortScanResult[] Scan()
        {
            return InternalScan();
        }

        public virtual async Task<PortScanResult[]> ScanTaskAsync()
        {
            return await Task.Run(() => InternalScan());
        }


        private PortScanResult[] InternalScan()
        {

            var results = new PortScanResult[ports.Length];
            for (int i = 0; i < ports.Length; i++)
            {
                try
                {
                    using (var sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
                    {
                        var connectResult = sock.BeginConnect(address, ports[i], null, null);

                        var open = connectResult.AsyncWaitHandle.WaitOne(1000);

                        var result = new PortScanResult
                        {
                            Port = ports[i],
                            Open = open
                        };

                        OnSinglePortScanFinish?.Invoke(this, new PortScannerEventArgs(result));

                        results[i] = result;
                    }
                }
                catch(Exception)
                {

                }
            }
            return results;
        }
    }



        // PortScanner event
        public class PortScannerEventArgs : EventArgs
        {
            // PortScanResult Property
            public PortScanResult PortScanResult { get; set; }

            // PortScannerEventArgs method
            public PortScannerEventArgs(PortScanResult result)
            {
                // result parameter passes to PortScanResult property
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