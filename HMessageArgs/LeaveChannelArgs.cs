using System;
using ChatProtos.Networking;
using ChatProtos.Networking.Messages;

namespace CoreClient.HMessageArgs
{
    public class LeaveChannelArgs : EventArgs
    {
        public HConnection Connection { get; }
        public HEvents Events { get; }
        public ResponseStatus Status { get; }
        public LeaveChannelMessageResponse Message { get; }

        public LeaveChannelArgs(HConnection connection, HEvents events, ResponseStatus status,
            LeaveChannelMessageResponse message)
        {
            Connection = connection;
            Events = events;
            Status = status;
            Message = message;
        }
    }
}