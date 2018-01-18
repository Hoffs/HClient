using System;
using ChatProtos.Networking.Messages;
using HServer.Networking;

namespace HChatClient.HMessageArgs
{
    public class LeaveChannelArgs : EventArgs
    {
        public HConnection Connection { get; }
        public HChatEvents Events { get; }
        public ResponseStatus Status { get; }
        public LeaveChannelResponse Message { get; }

        public LeaveChannelArgs(HConnection connection, HChatEvents events, ResponseStatus status,
            LeaveChannelResponse message)
        {
            Connection = connection;
            Events = events;
            Status = status;
            Message = message;
        }
    }
}