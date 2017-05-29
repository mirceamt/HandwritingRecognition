using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;

using HandwritingRecognition.Utils;

namespace HandwritingRecognition.Communication
{
    class ConnectionManager
    {
        private static IPAddress address = IPAddress.Parse("127.0.0.1");
        private static Int32 port = 13000;
        public static TcpListener tcpListener = null;
        public static Socket socket = null;

        private static bool connected = false;

        private static void ListenToConnectionsOnAnotherThread()
        {
            tcpListener.Start();
            socket = tcpListener.AcceptSocket();
            connected = true;
            while(true)
            {
                Tuple<int, byte[]> receivedPack = ConnectionManager.ReceiveBytes();
                int receivedBytesCount = receivedPack.Item1;
                byte[] receivedBytes = receivedPack.Item2;

                if (receivedBytesCount == 0 || receivedBytes == null)
                {
                    Logger.LogError("Restart Python Client!");
                    ApplicationUseManager.Instance.TriggerApplicationNotReady();
                    break;
                }

                // TODO
                // move action on another thread????
                MessagesInterpreter.interpretMessageAndDoAction(receivedBytesCount, receivedBytes);
                
            }
        }

        public static void StartListeningToConnections()
        {
            tcpListener = new TcpListener(address, port);
            Thread th = new Thread(new ThreadStart(ListenToConnectionsOnAnotherThread));
            th.Start();
        }

        public static void SendBytes(byte[] bytesToSend)
        {
            try
            {
                socket.Send(bytesToSend);
            }
            catch (SocketException e)
            {
                Logger.LogError("Restart Python Client!");
                ApplicationUseManager.Instance.TriggerApplicationNotReady();
            }
        }

        public static Tuple<int, byte[]> ReceiveBytes()
        {
            byte[] receivedBytes = new byte[2048];
            try
            {
                int cnt = socket.Receive(receivedBytes);
                return new Tuple<int, byte[]>(cnt, receivedBytes);
            }
            catch(SocketException e)
            {
                Logger.LogError("Restart Python Client!");
                ApplicationUseManager.Instance.TriggerApplicationNotReady();
                return new Tuple<int, byte[]>(0, null);
            }
        }

        internal static void ShutdownConnectionManager()
        {
            // WARNING! use with caution!
            if (ConnectionManager.tcpListener != null)
            {
                ConnectionManager.tcpListener.Server.Close(0);
                ConnectionManager.tcpListener.Stop();
            }
            if (ConnectionManager.socket != null)
            {
                ConnectionManager.socket.Close();
            }
        }
    }
}
