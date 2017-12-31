using System;
using ChatProtos.Networking;
using ChatProtos.Networking.Messages;

namespace CoreClient.HMessageArgs
{
    public class JoinChannelArgs : EventArgs
    {
        public JoinChannelMessageResponse Message { get; }
        public ResponseStatus Status { get; }
        public HEvents Events { get; }
        public HConnection Connection { get; }

        public JoinChannelArgs(HConnection connection, HEvents events, ResponseStatus status, JoinChannelMessageResponse message)
        {
            Connection = connection;
            Events = events;
            Status = status;
            Message = message;
        }

    }
}