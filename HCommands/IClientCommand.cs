using System.Threading.Tasks;

namespace CoreClient.HCommands
{
    public interface IClientCommand
    {
        Task Execute(HCommandManager manager, HConnection connection);
    }
}