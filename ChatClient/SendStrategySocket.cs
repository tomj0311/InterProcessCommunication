using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace ChatClient
{
    public class SendStrategySocket : SendStrategy
    {
        public Socket clientSocket; 
        private byte[] byteData = new byte[1024];

        public SendStrategySocket()
        {
            clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddress, 5000);

            clientSocket.Connect(ipEndPoint);
        }

        public override void SendMessage(string message)
        {
            BinaryFormatter formatter = new BinaryFormatter();

            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream())
            {
                bf.Serialize(ms, message);
                if (clientSocket != null)
                {
                    clientSocket.Send(ms.ToArray());
                }
            }
        }
    }
}
