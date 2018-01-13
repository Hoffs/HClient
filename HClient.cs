using System;
using System.IO;
using System.Threading.Tasks;
using HChatClient.HCommands;

namespace HChatClient
{
    public class HClient
    {
        public bool IsAuthenticated { get; set; } = false;
        public bool IsRunning { get; set; } = false;
        public HChatEvents Events { get; } = new HChatEvents();
        private readonly HConnection _hConnection;

        public HClient(string address, int port)
        {
            _hConnection = new HConnection(address, port);
        }

        public HConnection GetConnection()
        {
            return _hConnection;
        }

        public async Task Connect()
        {
            await _hConnection.Connect();
        }

        public async Task StartClient()
        {
            Console.WriteLine("[CLIENT] Starting client routines");
            IsRunning = true;
            await StartMessageProcessing();
        }

        public async Task StartMessageProcessing()
        {
            Console.WriteLine("[CLIENT] Starting to read messages");
            while (_hConnection.IsConnected() || IsRunning)
            {
                try
                {
                    var message = await _hConnection.ReadMessageTask();
                    if (message != null)
                    {
                        await Events.InvokeEvent(this, message);
                    }
                }
                catch (IOException)
                {
                    Console.WriteLine("[CLIENT] Connection might have dropped.");
                    if (!IsRunning) break;
                    Console.WriteLine("[CLIENT] Trying to reconnect.");
                    await Connect();
                }
            }
        }
        
        public async Task ExecuteCommandTask(IClientCommand command)
        {
            await command.Execute(Events, _hConnection);
        }

        public async Task CloseAsync()
        {
            await Task.Yield();
            IsRunning = false;
            Close();
        }

        private void Close()
        {
            _hConnection?.CloseTask();
            IsAuthenticated = false;
        }
    }
}
