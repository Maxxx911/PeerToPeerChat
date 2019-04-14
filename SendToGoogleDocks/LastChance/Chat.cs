using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace LastChance
{
    internal class Chat
    {
        private List<string> addresses;
        public IPAddress MyIpAddress { get; set; }
        public int MyPort { get; set; }
        private UdpClient sender;


        public Chat()
        {
            addresses = new List<string>();
            MyIpAddress = GetMyLocalcAddress();
            sender = new UdpClient(new IPEndPoint(MyIpAddress, MyPort));
            MyPort = new Random().Next(3000, 65000);
            Thread receiver = new Thread(new ThreadStart(Receiver));
            receiver.Start();
        }

        private IPAddress GetMyLocalcAddress()
        {
            string host = Dns.GetHostName();
            IPAddress ip = Dns.GetHostByName(host).AddressList[0];
            return ip;
        }

        public void GetListAddresses()
        {
            addresses = DBIntegration.GetIpAddress();
        }

        public void Send(string message)
        {
            GetListAddresses();
           
            foreach (string address in addresses)
            {
                string[] fields = address.Split(':');
                IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse(fields[0]), Convert.ToInt32(fields[1]));
                byte[] bytes = Encoding.UTF8.GetBytes(message);
                sender.Send(bytes, bytes.Length, endPoint);
            }
        }

        public void Receiver()
        {
            UdpClient receivingUdpClient = new UdpClient(MyPort);
            IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any,MyPort);
            while (true)
            {
                // Ожидание дейтаграммы
                byte[] receiveBytes = receivingUdpClient.Receive(ref RemoteIpEndPoint);

                // Преобразуем и отображаем данные
                string returnData = Encoding.UTF8.GetString(receiveBytes);
                Console.WriteLine(" --> " + returnData.ToString());
            }

        }

        
    }
}
