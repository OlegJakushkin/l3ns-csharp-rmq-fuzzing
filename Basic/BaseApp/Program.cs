using System;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace BaseApp
{
    internal class Program
    {
        private static void Main()
        {
            Console.WriteLine("Hello World!");

            NetworkInterface
                .GetAllNetworkInterfaces()
                .Select(e=>e.GetIPProperties().UnicastAddresses
                    .FirstOrDefault(ip => ip.Address.AddressFamily == AddressFamily.InterNetwork)?.Address)
                .Where(e=> e!= null)
                .ToList()
                .ForEach(e=>
                    Console.WriteLine("IP Address {0}", e)
                );
        }
    }
}
