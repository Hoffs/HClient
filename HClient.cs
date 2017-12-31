using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using ChatProtos.Networking;
using ChatProtos.Networking.Messages;
using CoreClient.HCommands;
using CoreClient.HMessageArgs;

namespace CoreClient
{
    public class HClient
    {
        // TODO: Rework command manager to be based on EventHandler's
        // TODO: Use blocking collections
        private readonly HConnection _hConnection;
        public HEvents Events { get; } = new HEvents();

        public bool IsAuthenticated { get; set; } = false;
        public bool IsRunning { get; set; } = false;

        public HClient(string address, int port)
        {
            _hConnection = new HConnection(address, port);
            HEvents.RegisterDefaultHandlers(Events);
        }

        public HConnection GetConnection()
        {
            return _hConnection;
        }

        public async Task Connect()
        {
            Console.WriteLine("[CLIENT] Connecting client...");
            await _hConnection.Connect();
        }

        public async Task StartClient()
        {
            Console.WriteLine("[CLIENT] Starting client routines");
            IsRunning = true;
            var tasks = new List<Task>();
            tasks.Add(StartMessageProcessing());
            await Task.WhenAll(tasks);
        }

        public async Task StartMessageProcessing()
        {
            Console.WriteLine("[CLIENT] Starting to read messages");
            while (_hConnection.IsConnected() || IsRunning)
            {
                try
                {
                    var message = await _hConnection.ReadMessage();
                    Events.InvokeEvent(this, message);
                }
                catch (IOException ex)
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
