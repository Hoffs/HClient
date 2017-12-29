using System;
using System.Threading.Tasks;

namespace CoreClient.HCommands
{
    public interface IClientCommand
    {
        Task Execute(Action<IPendingCommand> addPending, HConnection hConnection);
    }
}