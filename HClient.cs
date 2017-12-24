using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ChatProtos.Networking;
using ChatProtos.Data;
using ChatProtos.Networking.Messages;
using Google.Protobuf;

namespace CoreClient
{
    public class HClient
    {
        private TcpClient _tcpClient;
        private NetworkStream stream;

        public bool IsReceiving { set; get; }

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
                if (IsConnected()) // Smarter solution for handling correct connection with retries
                {
                    stream = _tcpClient.GetStream();
                    var readingTask = StartReadingTask(stream);
                    if (readingTask.IsFaulted)
                        readingTask.Wait();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task StartReadingTask(NetworkStream stream)
        {
            await Task.Yield();
            while (IsConnected())
            {
                var packetSizeBytes = new byte[4];
                await stream.ReadAsync(packetSizeBytes, 0, 4);
                var size = BitConverter.ToInt32(packetSizeBytes, 0);

                var buffer = new byte[size];
                var byteCount = await stream.ReadAsync(buffer, 0, size);

                if (byteCount <= 0) continue;

                try
                {
                    var message = ResponseMessage.Parser.ParseFrom(buffer);
                    Console.WriteLine("[CLIENT] Server wrote protobuf of type: {0}", message.Type);
                    if (message.Type == RequestType.ChatMessage)
                    {
                        var chat = ChatMessageResponse.Parser.ParseFrom(message.Message);
                        Console.WriteLine("[CHAT_MESSAGE] " + chat.Message.Text);
                    }
                    // await _messageProcessor.ProcessMessage(message, hClient);
                    // Console.WriteLine("[SERVER] Processed message from Client {0}", hClient.GetDisplayName());
                }
                catch (InvalidProtocolBufferException e)
                {
                    Console.WriteLine(e);
                    Console.WriteLine("Couldn't parse protobuf");
                }
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
