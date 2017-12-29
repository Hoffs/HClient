using System;
using System.Threading.Tasks;

namespace CoreClient.HCommands
{
    public interface IClientCommand
    {
        Task Execute(Action<IMessageHandler> addPending, HConnection hConnection);
    }
}