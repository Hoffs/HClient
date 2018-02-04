using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using ChatProtos.Networking;
using ChatProtos.Networking.Messages;
using Google.Protobuf;
using HChatClient.HMessageArgs;
using HServer.Networking;

namespace HChatClient.HCommands
{
    public class JoinChannelCommand : IClientCommand
    {
        private readonly BlockingCollection<Guid> _list;
        private readonly string _channelId;

        // TODO: Actually add the ChannelManager to add channel to joined channel sist on JoinChannel handler.
        public JoinChannelCommand(BlockingCollection<Guid> list, string channelId)
        {
            _list = list;
            _channelId = channelId;
        }

        public async Task Execute(HChatEvents events, HConnection hConnection)
        {
            events.JoinChannelEventHandler += joinChannelHandler;
            var result = await hConnection.SendAyncTask(new RequestMessage
            {
                Type = (int)RequestType.JoinChannel,
                Message = new JoinChannelRequest
                {
                    ChannelId = _channelId,
                }.ToByteString()
            }.ToByteArray());
            if (!result) events.JoinChannelEventHandler -= joinChannelHandler;
        }

        private async void joinChannelHandler(object sender, JoinChannelArgs e)
        {
            if (e.Status == ResponseStatus.Success && e.Message.ChannelId == _channelId)
            {
                Console.WriteLine("Joined channel");
                _list.TryAdd(Guid.Parse(e.Message.ChannelId));
                e.Events.JoinChannelEventHandler -= joinChannelHandler;
            }
            else if (e.Message.ChannelId == _channelId)
            {
                e.Events.JoinChannelEventHandler -= joinChannelHandler;
            }
        }
    }
}