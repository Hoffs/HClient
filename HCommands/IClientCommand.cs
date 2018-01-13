using System.Threading.Tasks;

namespace HChatClient.HCommands
{
    public interface IClientCommand
    {
        Task Execute(HChatEvents events, HConnection hConnection);
    }
}