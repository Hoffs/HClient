using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using ChatProtos.Networking;
using CoreClient.HAttributes;

namespace CoreClient.HCommands
{
    public class HCommandManager
    {
        private readonly List<IMessageHandler> _messageHandlers = new List<IMessageHandler>();
        private readonly object _lock = new object();
        private readonly HConnection _hConnection;

        public HCommandManager(HConnection hConnection)
        {
            _hConnection = hConnection;
        }


        public void AddPendingCommand(IMessageHandler command)
        {
            lock (_lock)
            {
                _messageHandlers.Add(command);
            }
        }

        private void RemoveFinishedCommands(IEnumerable<IMessageHandler> commands)
        {
            lock (_lock)
            {
                foreach (var finishedCommand in commands.Where(command => command.IsFinished())) 
                    _messageHandlers.Remove(finishedCommand);
            }
        }

        public async Task TryExecutePendingCommands(ResponseMessage message)
        {
            IEnumerable<Task> tasks;
            IEnumerable<IMessageHandler> commands;
            lock (_lock)
            {
                commands = _messageHandlers.Where(command =>
                {
                    var attr = command.GetType().GetTypeInfo().GetCustomAttribute<HCommandSignatureAttribute>();
                    return attr?.GetRequestType() != null && message?.Type == attr.GetRequestType();
                });
                tasks = commands.Select(async command => await command.Execute(AddPendingCommand, _hConnection, message));
            }
            await Task.WhenAll(tasks);
            RemoveFinishedCommands(commands);
        }

        public async Task ExecuteClientCommand(IClientCommand command)
        {
            await command.Execute(AddPendingCommand, _hConnection);
            // await command.Execute(this, _hConnection);
        }
    }
}
