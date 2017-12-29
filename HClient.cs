using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ChatProtos.Networking;
using ChatProtos.Data;
using ChatProtos.Networking.Messages;
using CoreClient.HCommands;
using Google.Protobuf;

namespace CoreClient
{
    public class HClient
    {
        // TODO: Rework command manager to be based on EventHandler's

        private readonly HConnection _hConnection;
        private readonly HCommandManager _commandManager;

        public bool IsAuthenticated { get; set; } = false;

        public HClient(string address, int port)
        {
            _hConnection = new HConnection(address, port);
            _commandManager = new HCommandManager(_hConnection);
        }

        public HConnection GetConnection()
        {
            return _hConnection;
        }

        public HCommandManager GetCommandManager()
        {
            return _commandManager;
        }

        public async Task Connect()
        {
            Console.WriteLine("[CLIENT] Connecting client...");
            await _hConnection.Connect();
            await _commandManager.ExecuteClientCommand(new LoginCommand(this, "memer", "memer", "meeeeee"));
        }

        public async Task StartClient()
        {
            Console.WriteLine("[CLIENT] Starting client routines");
            var tasks = new List<Task>();
            tasks.Add(StartMessageProcessing());

            await Task.WhenAll(tasks);
        }

        public async Task StartMessageProcessing()
        {
            Console.WriteLine("[CLIENT] Starting to read messages");
            while (_hConnection.IsConnected())
            {
                var message = await _hConnection.ReadMessage();
                await _commandManager.TryExecutePendingCommands(message);
            }
        } 
    }
}
