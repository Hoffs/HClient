using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using Google.Protobuf;
using HChatClient;
using HChatClient.ChatProtos.Networking;
using HChatClient.ChatProtos.Networking.Messages;
using HChatClient.HCommands;
using HChatClient.HMessageArgs;

namespace ChatClient
{
    public class JoinChannelCommand : IClientCommand
    {
        private readonly BlockingCollection<Guid> _list;
        private readonly string _channelId;

        // TODO: Actually add the ChannelManager to add channel to joined channel list on JoinChannel handler.
        public JoinChannelCommand(BlockingCollection<Guid> list, string channelId)
        {
            _list = list;
            _channelId = channelId;
        }

        public async Task Execute(HChatEvents events, HConnection hConnection)
        {
            await hConnection.SendAyncTask(new RequestMessage
            {
                Type = RequestType.JoinChannel,
                Message = new JoinChannelMessageRequest
                {
                    ChannelId = _channelId,
                    ChannelName = _channelId
                }.ToByteString()
            }.ToByteArray());

            events.JoinChannelEventHandler += joinChannelHandler;
        }

        private async void joinChannelHandler(object sender, JoinChannelArgs e)
        {
            if (e.Status == ResponseStatus.Success && e.Message.ChannelId == _channelId)
            {
                Console.WriteLine("Joined channel");
                _list.TryAdd(Guid.Parse(e.Message.ChannelId));
                e.Events.JoinChannelEventHandler -= joinChannelHandler;
            } else if (e.Message.ChannelId == _channelId)
            {
                e.Events.JoinChannelEventHandler -= joinChannelHandler;
            }

        }
    }
}