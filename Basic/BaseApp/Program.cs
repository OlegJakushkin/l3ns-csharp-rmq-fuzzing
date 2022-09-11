using System;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Threading;

namespace BaseApp
{
    internal class Program
    {
        private static void Main()
        {
            Console.WriteLine("Hello World!");

            var Ips = NetworkInterface
                .GetAllNetworkInterfaces()
                .Select(e=>e.GetIPProperties().UnicastAddresses
                    .FirstOrDefault(ip => ip.Address.AddressFamily == AddressFamily.InterNetwork)?.Address)
                .Where(e=> e!= null)
                .ToList();

            var loopCount = 0;

            while(true) {
                Ips.ForEach(e =>
                    Console.WriteLine("IP Address {0}", e)
                );
                Console.WriteLine("iter: {0}, kill to quit", loopCount++);
                Thread.Sleep(1000);
            }
        }
    }
}
