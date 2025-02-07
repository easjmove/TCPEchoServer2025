using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;

namespace TCPEchoServer
{
    public class ClientHandler
    {
        public static void HandleClient(TcpClient client)
        {
            Console.WriteLine(client.Client.RemoteEndPoint);
            NetworkStream ns = client.GetStream();
            StreamReader reader = new StreamReader(ns);
            StreamWriter writer = new StreamWriter(ns);

            string? message = reader.ReadLine();
            Console.WriteLine(message);
            writer.WriteLine(message);
            writer.Flush();

            client.Close();
        }
    }
}
