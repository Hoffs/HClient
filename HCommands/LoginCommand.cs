using System;
using System.Threading.Tasks;
using ChatProtos.Networking;
using ChatProtos.Networking.Messages;
using CoreClient.HMessageArgs;
using Google.Protobuf;
using JetBrains.Annotations;

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
        
        public async Task Execute(HEvents events, HConnection connection)
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
            events.LoginEventHandler += OnLoginConfirm;
        }

        public async void OnLoginConfirm([NotNull] object sender, [NotNull] LoginArgs args)
        {
            await Task.Yield();
            if (args.Status != ResponseStatus.Success) return;
            _client.IsAuthenticated = true;
            args.Events.LoginEventHandler -= OnLoginConfirm;
        }
    }
}