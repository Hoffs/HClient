using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ChatProtos.Networking;
using ChatProtos.Data;
using Google.Protobuf;

namespace CoreClient
{
    public class HClient
    {
        private TcpClient _tcpClient;
        private NetworkStream stream;

        public bool IsReceiving { set; get; }

        public bool IsConnected
        {
            get
            {
                try
                {
                    if (_tcpClient?.Client == null || !_tcpClient.Client.Connected) return false;
                    /* pear to the documentation on Poll:
                         * When passing SelectMode.SelectRead as a parameter to the Poll method it will return 
                         * -either- true if Socket.Listen(Int32) has been called and a connection is pending;
                         * -or- true if data is available for reading; 
                         * -or- true if the connection has been closed, reset, or terminated; 
                         * otherwise, returns false
                         */

                    // Detect if client disconnected
                    if (!_tcpClient.Client.Poll(0, SelectMode.SelectRead)) return true;
                    var buff = new byte[1];
                    return _tcpClient.Client.Receive(buff, SocketFlags.Peek) != 0;
                }
                catch
                {
                    return false;
                }
            }
        }

        public HClient()
        {
            _tcpClient = new TcpClient();
            _tcpClient.Client.SetSocketOption(SocketOptionLevel.Socket,
                SocketOptionName.KeepAlive,
                true);
        }

        public async Task Connect(string address, int port)
        {
            try
            {
                Console.WriteLine("[Client] Connecting to server");
                await _tcpClient.ConnectAsync(address, port);
                Console.WriteLine("[Client] Finished connecting");
                // Handle SSL too
                if (IsConnected) // Smarter solution for handling correct connection with retries
                {
                    stream = _tcpClient.GetStream();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task SendAync(RequestMessage message)
        {
            try
            {
                var messageBytes = message.ToByteArray();
                Console.WriteLine("[Client] Starting to send message with length {0}", messageBytes.Length);
                var packet = new byte[4 + messageBytes.Length];
                System.Buffer.BlockCopy(BitConverter.GetBytes(messageBytes.Length), 0, packet, 0, 4);
                System.Buffer.BlockCopy(messageBytes, 0, packet, 4, messageBytes.Length);

                await stream.WriteAsync(packet, 0, packet.Length); // Cancelation token?
                await stream.FlushAsync();

                Console.WriteLine("[Client] Finished sending");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task CloseAsync()
        {
            await Task.Yield();
            Close();
        }

        private void Close()
        {
            _tcpClient?.Dispose();
            stream?.Dispose();
        }
    }
}
