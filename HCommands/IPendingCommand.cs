using System;
using System.Threading.Tasks;
using ChatProtos.Networking;

namespace CoreClient.HCommands
{
    public interface IPendingCommand
    {
        Task Execute(HCommandManager manager, HConnection connection, ResponseMessage message);

        bool IsFinished();

        RequestType GetRequiredType();
    }
}