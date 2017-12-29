using System;
using System.Threading.Tasks;
using ChatProtos.Networking;

namespace CoreClient.HCommands
{
    public interface IMessageHandler
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="addHandler">Function that lets you add another handler.</param>
        /// <param name="connection">Connection to the server.</param>
        /// <param name="message">Received message.</param>
        /// <returns></returns>
        Task Execute(Action<IMessageHandler> addHandler, HConnection connection, ResponseMessage message);

        /// <summary>
        /// Called during cleaning of handler list to see if handler should be removed.
        /// </summary>
        /// <returns></returns>
        bool IsFinished();
    }
}