using LumiSoft.Net.STUN.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace LastChance
{
    static class StunIntegration
    {
        private const string stunServer = "stun2.l.google.com";
        private const int stunPort = 19302;
        private static STUN_Result STUN;
        private static Socket socket;

        public static string GetPublicIpAdress(IPAddress ip, int port)
        {
          
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            //сюда вставить локальный ip адрес
            socket.Bind(new IPEndPoint(ip, port));
            STUN = STUN_Client.Query(stunServer, stunPort, socket);            
            return string.Format("{0}:{1}", STUN.PublicEndPoint.Address.ToString(),STUN.PublicEndPoint.Port);
        }
    }
}
