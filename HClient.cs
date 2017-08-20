using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CoreClient
{
    public class HClient
    {
        private TcpClient client;
        private NetworkStream stream;

        public bool IsReceiving { set; get; }

        public bool IsConnected => client != null && client.Connected;

        public HClient()
        {
            client = new TcpClient();
            client.Client.SetSocketOption(SocketOptionLevel.Socket,
                SocketOptionName.KeepAlive,
                true);
        }

        public async Task Connect(string address, int port)
        {
            try
            {
                Console.WriteLine("[Client] Connecting to server");
                await client.ConnectAsync(address, port);
                Console.WriteLine("[Client] Finished connecting");
                // Handle SSL too
                if (IsConnected) // Smarter solution for handling correct connection with retries
                {
                    stream = client.GetStream();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task SendAync(byte[] data)
        {
            try
            {
                Console.WriteLine("[Client] Starting to send");
                await stream.WriteAsync(data, 0, data.Length); // Cancelation token?
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
            this.Close();
        }

        private void Close()
        {
            client?.Dispose();
            stream?.Dispose();
        }
    }
}
