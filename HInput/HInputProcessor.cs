using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ChatProtos.Data;
using ChatProtos.Networking;
using ChatProtos.Networking.Messages;

namespace CoreClient.HInput
{
    class HInputProcessor
    {
        private IDictionary<string, RequestType> _commands;

        public HInputProcessor()
        {
            _commands = new Dictionary<string, RequestType>
            {
                { "/login", RequestType.Login },
                { "/logout", RequestType.Logout },
                { "/join", RequestType.JoinChannel },
                { "/leave", RequestType.LeaveChannel }
            };
        }

        public async Task<RequestMessage> ProcessMessageTask(string message)
        {
            var command = message.Split(' ')[0];
            var remainingMessage = message.Substring(command.Length);

            if (remainingMessage.StartsWith(" "))
            {
                remainingMessage = remainingMessage.Substring(1); // Looks still ugly, but works.
            }

            RequestMessage requestMessage;
            if (_commands.TryGetValue(command, out var requestType))
            {
                requestMessage = await MakeRequestMessageTask(requestType, remainingMessage);
            }
            else
            {
                requestMessage = await MakeRequestMessageTask(RequestType.ChatMessage, remainingMessage);
            }
            return requestMessage;
        }

        public async Task<RequestMessage> MakeRequestMessageTask(RequestType requestType, string message)
        {
            RequestMessage requestMessage = new RequestMessage {Type = requestType};
            var realMessage = GetRequestMessageOfType(requestType);
            var split = message.Split(' '); 
            switch (requestType)
            {
                case RequestType.Login:
                    if (split.Length >= 2)
                    {
                        realMessage.Username = split[0];
                        realMessage.Password = split[1]; // Change this to something more sensible
                        // realMessage.Token = split[1];
                    }
                    break;
                case RequestType.Logout:
                    realMessage.Token = message;
                    break;
                case RequestType.JoinChannel:
                    if (split.Length >= 1)
                    {
                        realMessage.ChannelId = split[0]; // Change this to something more sensible
                        realMessage.ChannelName = split[0];
                    }
                    break;
                case RequestType.LeaveChannel:
                    if (split.Length >= 1)
                    {
                        realMessage.ChannelId = split[0]; // Change this to something more sensible
                        realMessage.ChannelName = split[0];
                    }
                    break;
                case RequestType.ChatMessage:
                    if (split.Length >= 1)
                    {
                        ChatMessage chatMessage = new ChatMessage();
                        var channelId = message.Split(' ')[0];
                        chatMessage.Text = message.Substring(channelId.Length);
                        realMessage.ChannelId = channelId;
                    }
                    break;
                case RequestType.BanUser:
                    break;
                case RequestType.KickUser:
                    break;
                case RequestType.UpdateDisplayName:
                    break;
                case RequestType.AddRole:
                    break;
                case RequestType.RemoveRole:
                    break;
                case RequestType.UserInfo:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(requestType), requestType, null);
            }
            requestMessage.Message = Google.Protobuf.MessageExtensions.ToByteString(realMessage);
            return requestMessage;
        }

        private static dynamic GetRequestMessageOfType(RequestType requestType)
        {
            var typeName = "ChatProtos.Networking.Messages." + requestType;
            if (typeName.EndsWith("Message"))
            {
                typeName += "Request";
            }
            else
            {
                typeName += "MessageRequest";
            }
            return Activator.CreateInstance(Type.GetType(typeName));
        }
    }
}
