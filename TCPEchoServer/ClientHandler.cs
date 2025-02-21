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
            List<string> options = new List<string>() { "4", "6", "8", "10", "12", "20" };
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
                else if (message == "ToUpper")
                {
                    writer.WriteLine("write to convert to uppercase");
                    writer.Flush();
                    string? messageToConvert = reader.ReadLine();
                    messageToConvert = messageToConvert.ToUpper();
                    writer.WriteLine(messageToConvert);
                    writer.Flush();
                }
                else if (message == "RollDice")
                {
                    writer.WriteLine("which die 4/6/8/10/12/20");
                    writer.Flush();
                    string number = reader.ReadLine();
                    
                    if (options.Contains(number))
                    {
                        int maxInt = int.Parse(number);
                        Random random = new Random();
                        int result = random.Next(1, maxInt + 1);
                        writer.WriteLine(result);
                        writer.Flush();
                    } else
                    {
                        writer.WriteLine("Du er en hat!");
                        writer.Flush();
                    }
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
