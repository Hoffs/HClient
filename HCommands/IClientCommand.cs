using System;
using System.Threading.Tasks;

namespace CoreClient.HCommands
{
    public interface IClientCommand
    {
        Task Execute(HEvents events, HConnection hConnection);
    }
}