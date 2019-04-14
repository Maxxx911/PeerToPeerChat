using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LastChance
{
    class Program
    {
        private static int port = 8888;

        static void Main(string[] args)
        {
            Chat chat = new Chat();
           
            string publicAdress =  StunIntegration.GetPublicIpAdress(chat.MyIpAddress,chat.MyPort);
            Console.WriteLine(publicAdress);
            DBIntegration.InserIpAddress(publicAdress);

            while (true)
            {
                chat.Send(Console.ReadLine());
            }
           
        }
    }
}
