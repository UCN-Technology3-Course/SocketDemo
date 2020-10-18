using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SocketDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            // create socket
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            // establish connection
            IPAddress ipAddress = new IPAddress(new byte[] { 127, 0, 0, 1 });
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11000);

            // listening after incoming connections
            socket.Bind(localEndPoint);
            socket.Listen(100);

            do
            {
                Console.WriteLine("Waiting for incoming connections...");
                using (var incoming = socket.Accept())
                {
                    Console.WriteLine(incoming.RemoteEndPoint);

                    // handle incoming connection
                    byte[] buffer = new byte[1024];

                    while (incoming.Receive(buffer) > 0)
                    {
                        foreach (var v in buffer)
                        {
                            Console.Write($"{v}:");
                        }
                    }

                    //incoming.Send(Encoding.ASCII.GetBytes("Fuck off"));

                    incoming.Disconnect(false);
                    incoming.Close();
                }


            } while (true);
        }
    }
}
