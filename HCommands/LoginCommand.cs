using System;
using System.Threading.Tasks;
using ChatProtos.Networking;
using ChatProtos.Networking.Messages;
using Google.Protobuf;

namespace CoreClient.HCommands
{
    public class LoginCommand : IClientCommand
    {
        private readonly string _username;
        private readonly string _password;
        private readonly string _token;
        private readonly HClient _client;

        public LoginCommand(HClient client, string username, string password, string token)
        {
            _client = client;
            _username = username;
            _password = password;
            _token = token;
        }
        
        public async Task Execute(Action<IMessageHandler> addPending, HConnection connection)
        {
            var message = new RequestMessage
            {
                Type = RequestType.Login,
                Message = new LoginMessageRequest
                {
                    Username = _username,
                    Password = _password,
                    Token = _token
                }.ToByteString()
            };
            await connection.SendAync(message);
            var pending = new LoginConfirmHandler(_client);
            addPending?.Invoke(pending);
        }
    }
}