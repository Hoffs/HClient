using System;
using System.Net.Sockets;
using System.Threading.Tasks;
using ChatProtos.Networking;
using ChatProtos.Networking.Messages;
using Google.Protobuf;

namespace CoreClient
{
    public class HConnection
    {
        private readonly TcpClient _tcpClient;
        private NetworkStream _stream;

        private string _ip;
        private int _port;

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
                // Handle SSL too
                if (IsConnected()) // Smarter solution for handling correct connection with retries
                {
                    _stream = _tcpClient.GetStream();
                    /*
                    var readingTask = StartReadingTask(_stream);
                    if (readingTask.IsFaulted)
                        readingTask.Wait();*/
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<ResponseMessage> ReadMessage()
        {
            await Task.Yield(); // Forces Async
            ResponseMessage message = null;
            var packetSizeBytes = new byte[4];
            await _stream.ReadAsync(packetSizeBytes, 0, 4);
            
            var size = BitConverter.ToInt32(packetSizeBytes, 0);

            var buffer = new byte[size];
            var byteCount = await _stream.ReadAsync(buffer, 0, size);

            if (byteCount <= 0) return null;

            try
            {
                message = ResponseMessage.Parser.ParseFrom(buffer);
            }
            catch (InvalidProtocolBufferException e)
            {
                Console.WriteLine(e);
                Console.WriteLine("Couldn't parse protobuf");
            }

            return message;
        }

        public async Task SendAync(RequestMessage message)
        {
            try
            {
                var messageBytes = message.ToByteArray();
                Console.WriteLine("[Client] Starting to send message with length {0}", messageBytes.Length);
                var packet = new byte[4 + messageBytes.Length];
                Buffer.BlockCopy(BitConverter.GetBytes(messageBytes.Length), 0, packet, 0, 4);
                Buffer.BlockCopy(messageBytes, 0, packet, 4, messageBytes.Length);
                await _stream.WriteAsync(packet, 0, packet.Length); // Cancelation token?
                await _stream.FlushAsync();
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
            _stream?.Dispose();
        }
    }
}