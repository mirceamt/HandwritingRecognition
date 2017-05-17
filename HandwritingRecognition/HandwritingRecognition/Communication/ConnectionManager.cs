using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace HandwritingRecognition.Communication
{
    class ConnectionManager
    {
        private static IPAddress address = IPAddress.Parse("127.0.0.1");
        private static Int32 port = 13000;
        private static TcpListener tcpListener = null;
        private static Socket socket = null;

        private static bool connected = false;


        private static void ListenToConnectionsOnAnotherThread()
        {
            tcpListener.Start();
            socket = tcpListener.AcceptSocket();
            connected = true;
        }

        public static void StartListeningToConnections()
        {
            tcpListener = new TcpListener(address, port);
            Thread th = new Thread(new ThreadStart(ListenToConnectionsOnAnotherThread));
            th.Start();
        }

        public static void SendBytes(byte[] bytesToSend)
        {
            socket.Send(bytesToSend);
        }
    }
}
