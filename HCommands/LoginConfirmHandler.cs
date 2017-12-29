using System;
using System.Threading.Tasks;
using ChatProtos.Networking;
using CoreClient.HAttributes;

namespace CoreClient.HCommands
{
    [HCommandSignature(RequestType.Login)]
    public class LoginConfirmHandler : IMessageHandler
    {
        private readonly HClient _client;
        private bool _isFinished = false;

        public LoginConfirmHandler(HClient client)
        {
            _client = client;
        }

        public async Task Execute(Action<IMessageHandler> addHandler, HConnection connection, ResponseMessage message)
        {
            if (message.Status != ResponseStatus.Success) return;
            _client.IsAuthenticated = true;
            _isFinished = true;
            Console.WriteLine("[CLIENT] Authenticated");
        }

        public bool IsFinished()
        {
            return _isFinished;
        }
    }
}