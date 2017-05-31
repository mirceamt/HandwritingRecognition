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
            socket = null;
            connected = false;
            try
            {
                socket = tcpListener.AcceptSocket();
            }
            catch (SocketException e)
            {
                if (e.SocketErrorCode == SocketError.Interrupted)
                {
                    // program closed and therefore interrupted the AcceptSocket() call
                    return;
                }

                // otherwise we should try to restart listening
                socket = null;
                Thread th = new Thread(new ThreadStart(ListenToConnectionsOnAnotherThread));
                th.Start();
                return;
            }

            connected = true;
            bool tryRestartServer = true;
            while (true)
            {
                Tuple<int, byte[]> receivedPack = ConnectionManager.ReceiveBytes();
                int receivedBytesCount = receivedPack.Item1;
                byte[] receivedBytes = receivedPack.Item2;

                if (receivedBytesCount == 0 && receivedBytes == null)
                {
                    // ConnectionReset - remote peer reset the connection
                    Logger.LogError("Restart Python Client!");
                    ApplicationUseManager.Instance.TriggerApplicationNotReady();
                    tryRestartServer = true;
                    break;
                }
                else if (receivedBytesCount == -1  && receivedBytes == null)
                {
                    // ConnectionAborted - The connection was aborted by the .NET Framework or the underlying socket provider.
                    tryRestartServer = false;
                    break;
                }

                // TODO
                // move action on another thread????
                MessagesInterpreter.interpretMessageAndDoAction(receivedBytesCount, receivedBytes);
            }
            if (tryRestartServer)
            {
                tcpListener.Stop();
                socket = null;
                Thread th2 = new Thread(new ThreadStart(ListenToConnectionsOnAnotherThread));
                th2.Start();
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
                if (e.SocketErrorCode == SocketError.ConnectionReset)
                {
                    Logger.LogError("Restart Python Client!");
                    ApplicationUseManager.Instance.TriggerApplicationNotReady();
                    return new Tuple<int, byte[]>(0, null);
                }
                else if (e.SocketErrorCode == SocketError.ConnectionAborted)
                {
                    return new Tuple<int, byte[]>(-1, null);
                }
                return new Tuple<int, byte[]>(-2, null);
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

        internal static void SendLinearizedImageForClassification(string lastImageLinearizedAsString)
        {
            byte[] bytesToSend = new byte[1030];
            bytesToSend[0] = 1;
            int cnt = 0;

            if (lastImageLinearizedAsString.Length != 1024)
            {
                throw new Exception("linearized image does not have a length of 1024!!!!!!!");
            }

            for (int i = 0; i < lastImageLinearizedAsString.Length; i++)
            {
                bytesToSend[++cnt] = (byte) lastImageLinearizedAsString[i];
            }

            ConnectionManager.SendBytes(bytesToSend);
        }
    }
}
