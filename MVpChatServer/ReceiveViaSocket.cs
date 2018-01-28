using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using MvpChatServer.View;

namespace MvpChatServer
{
    public class ReceiveViaSocket
    {
        private ReceiveForm _receiveForm;
        Socket serverSocket;
        byte[] byteData = new byte[1024];

        public ReceiveViaSocket(ReceiveForm receiveForm)
        {
            _receiveForm = receiveForm;

            serverSocket = new Socket(AddressFamily.InterNetwork,
                                      SocketType.Stream,
                                      ProtocolType.Tcp);

            IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Any, 5000);

            serverSocket.Bind(ipEndPoint);
            serverSocket.Listen(4);

            serverSocket.BeginAccept(new AsyncCallback(OnAccept), null);
        }

        private void OnAccept(IAsyncResult ar)
        {
            Socket clientSocket = serverSocket.EndAccept(ar);

            serverSocket.BeginAccept(new AsyncCallback(OnAccept), null);

            clientSocket.BeginReceive(byteData, 0, byteData.Length, SocketFlags.None,
                new AsyncCallback(OnReceive), clientSocket);
        }

        public void OnReceive(IAsyncResult ar)
        {
            Socket clientSocket = (Socket)ar.AsyncState;
            clientSocket.EndReceive(ar);

            BinaryFormatter formatter = new BinaryFormatter();
            string result = (string)formatter.Deserialize(new MemoryStream(byteData));

            _receiveForm.DisplayMessage(result);
        }
    }
}
