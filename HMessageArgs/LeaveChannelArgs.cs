using System;
using ChatProtos.Networking;
using ChatProtos.Networking.Messages;

namespace CoreClient.HMessageArgs
{
    public class LeaveChannelArgs : EventArgs
    {
        public HConnection Connection { get; }
        public HEvents Handlers { get; }
        public ResponseStatus Status { get; }
        public LeaveChannelMessageResponse Message { get; }

        public LeaveChannelArgs(HConnection connection, HEvents handlers, ResponseStatus status,
            LeaveChannelMessageResponse message)
        {
            Connection = connection;
            Handlers = handlers;
            Status = status;
            Message = message;
        }
    }
}