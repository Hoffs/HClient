using System;
using System.Threading.Tasks;
using ChatProtos.Networking;

namespace CoreClient.HCommands
{
    public class LoginConfirmCommand : IPendingCommand
    {
        private readonly HClient _client;
        private bool _isFinished = false;

        public LoginConfirmCommand(HClient client)
        {
            _client = client;
        }

        public async Task Execute(HCommandManager manager, HConnection connection, ResponseMessage message)
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

        public RequestType GetRequiredType()
        {
            return RequestType.Login;
        }
    }
}