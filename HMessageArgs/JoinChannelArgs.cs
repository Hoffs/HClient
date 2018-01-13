using System;
using HChatClient.ChatProtos.Networking;
using HChatClient.ChatProtos.Networking.Messages;

namespace HChatClient.HMessageArgs
{
    public class JoinChannelArgs : EventArgs
    {
        public JoinChannelMessageResponse Message { get; }
        public ResponseStatus Status { get; }
        public HChatEvents Events { get; }
        public HConnection Connection { get; }

        public JoinChannelArgs(HConnection connection, HChatEvents events, ResponseStatus status, JoinChannelMessageResponse message)
        {
            Connection = connection;
            Events = events;
            Status = status;
            Message = message;
        }

    }
}