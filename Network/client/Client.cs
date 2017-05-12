using System;
using System.Net.Sockets;

namespace Network {
    public class Client {
        public void StartUp() {
            var tcpclnt = new TcpClient();
            Console.WriteLine("Connecting.....");

            tcpclnt.Connect("localhost", 8001);
            // use the ipaddress as in the server program

            Console.WriteLine("Connected");
            Console.Write("Enter the string to be transmitted : ");
        }
    }
}