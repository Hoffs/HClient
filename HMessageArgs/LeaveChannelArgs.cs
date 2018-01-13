using System;
using HChatClient.ChatProtos.Networking;
using HChatClient.ChatProtos.Networking.Messages;

namespace HChatClient.HMessageArgs
{
    public class LeaveChannelArgs : EventArgs
    {
        public HConnection Connection { get; }
        public HChatEvents Events { get; }
        public ResponseStatus Status { get; }
        public LeaveChannelMessageResponse Message { get; }

        public LeaveChannelArgs(HConnection connection, HChatEvents events, ResponseStatus status,
            LeaveChannelMessageResponse message)
        {
            Connection = connection;
            Events = events;
            Status = status;
            Message = message;
        }
    }
}