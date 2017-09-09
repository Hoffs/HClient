using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ChatProtos.Networking;

namespace CoreClient.HInput
{
    class HConsoleProcessor
    {
        private IDictionary<string, ChatProtos.Networking.RequestType> _commands;

        public HConsoleProcessor()
        {
            _commands = new Dictionary<string, ChatProtos.Networking.RequestType>()
            {
                { "/login", RequestType.Login },
                { "/logout", RequestType.Logout },
                { "/join", RequestType.JoinChannel },
                { "/leave", RequestType.LeaveChannel },
                { "/chat", RequestType.ChatMessage }
            };
        }

        public async Task<String> ProcessMessageTask(string message)
        {
            var command = message.Split(' ')[0];
            string response = "";
            try
            {
                if (_commands[command] != null)
                {
                    response = "exists";
                }
            }
            catch (KeyNotFoundException e)
            {
                response = "doesnt";
            }
            return response;
        }

        public async Task<RequestMessage> MakeRequestMessageTask(string message)
        {
            
        }
    }
}
