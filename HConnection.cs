using System;
using System.Net.Sockets;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace HChatClient
{
    public class HConnection
    {
        private readonly TcpClient _tcpClient;
        private NetworkStream _stream;

        private readonly string _ip;
        private readonly int _port;

        public HConnection(string ip, int port)
        {
            _ip = ip;
            _port = port;
            _tcpClient = new TcpClient();
            _tcpClient.Client.SetSocketOption(SocketOptionLevel.Socket,
                SocketOptionName.KeepAlive,
                true);
        }

        public bool IsConnected()
        {
            try
            {
                if (_tcpClient?.Client == null || !_tcpClient.Client.Connected) return false;
                if (!_tcpClient.Client.Poll(0, SelectMode.SelectRead)) return true;
                var buff = new byte[1];
                return _tcpClient.Client.Receive(buff, SocketFlags.Peek) != 0;
            }
            catch
            {
                return false;
            }
        }

        public async Task Connect()
        {
            try
            {
                Console.WriteLine("[Client] Connecting to server");
                await _tcpClient.ConnectAsync(_ip, _port);
                Console.WriteLine("[Client] Finished connecting");
                // TODO: Handle SSL too
                if (IsConnected()) // Smarter solution for handling correct connection with retries
                {
                    _stream = _tcpClient.GetStream();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        /// <summary>
        /// Reads messages from the server.
        /// </summary>
        /// <returns>Byte array of message</returns>
        [ItemCanBeNull]
        public async Task<byte[]> ReadMessageTask()
        {
            await Task.Yield();
            var packetSizeBytes = new byte[4];
            await _stream.ReadAsync(packetSizeBytes, 0, 4);
            var size = BitConverter.ToInt32(packetSizeBytes, 0);
            var buffer = new byte[size];
            var byteCount = await _stream.ReadAsync(buffer, 0, size);
            return byteCount <= 0 ? null : buffer;
        }

        /// <summary>
        /// Sends the byte array of the message with 4 bytes specifying length
        /// </summary>
        /// <param name="message">Byte array of message contents</param>
        /// <returns></returns>
        public async Task SendAyncTask([NotNull] byte[] message)
        {
            var packet = new byte[4 + message.Length];
            Buffer.BlockCopy(BitConverter.GetBytes(message.Length), 0, packet, 0, 4);
            Buffer.BlockCopy(message, 0, packet, 4, message.Length);
            await _stream.WriteAsync(packet, 0, packet.Length); // Cancelation token?
            await _stream.FlushAsync();
            Console.WriteLine("[Client] Finished sending");
        }

        public async Task CloseTask()
        {
            await Task.Yield();
            Close();
        }

        private void Close()
        {
            _tcpClient?.Dispose();
            _stream?.Dispose();
        }
    }
}