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

            bool isRunning = true;

            while (isRunning)
            {
                string? message = reader.ReadLine();
                Console.WriteLine(message);
                if (message == "time")
                {
                    writer.WriteLine(DateTime.Now.ToString("dd/MM/yy - HH:mm"));
                    writer.Flush();
                }
                else if (message == "close")
                {
                    writer.WriteLine("connection closed");
                    writer.Flush();
                    isRunning = false;
                }

            }
            client.Close();
        }
    }
}
