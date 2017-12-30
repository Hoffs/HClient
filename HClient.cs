using System;
using System.Collections.Generic;
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

        private readonly HConnection _hConnection;
        private readonly HCommandManager _commandManager;
        public HEvents EventHandlers { get; } = new HEvents();

        public bool IsAuthenticated { get; set; } = false;

        public HClient(string address, int port)
        {
            _hConnection = new HConnection(address, port);
            _commandManager = new HCommandManager(_hConnection);
            HEvents.RegisterDefaultHandlers(EventHandlers);
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
            await _commandManager.ExecuteClientCommand(new LoginCommand(this, "user", "pass", "token"));
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
                Console.WriteLine("Type : {0}", message.Type);
                if (message.Type == RequestType.Login)
                {
                    EventHandlers.OnLoginEventHandler(new LoginArgs(_hConnection, EventHandlers, message.Status, LoginMessageResponse.Parser.ParseFrom(message.Message)));
                }
            }
        } 
    }
}
