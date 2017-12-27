using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChatProtos.Networking;

namespace CoreClient.HCommands
{
    public class HCommandManager
    {
        private readonly List<IPendingCommand> _pendingCommands = new List<IPendingCommand>();
        private readonly object _lock = new object();
        private readonly HConnection _hConnection;

        public HCommandManager(HConnection hConnection)
        {
            _hConnection = hConnection;
        }


        public void AddPendingCommand(IPendingCommand command)
        {
            lock (_lock)
            {
                _pendingCommands.Add(command);
            }
        }

        private void RemoveFinishedCommands(IEnumerable<IPendingCommand> commands)
        {
            lock (_lock)
            {
                foreach (var finishedCommand in commands.Where(command => command.IsFinished())) 
                    _pendingCommands.Remove(finishedCommand);
            }
        }

        public async Task TryExecutePendingCommands(ResponseMessage message)
        {
            IEnumerable<Task> tasks;
            IEnumerable<IPendingCommand> commands;
            lock (_lock)
            {
                commands = _pendingCommands.Where(command => message?.Type == command.GetRequiredType());
                tasks = commands.Select(async command => await command.Execute(this, _hConnection, message));
            }
            await Task.WhenAll(tasks);
            RemoveFinishedCommands(commands);
        }

        public async Task ExecuteClientCommand(IClientCommand command)
        {
            await command.Execute(this, _hConnection);
        }
    }
}
