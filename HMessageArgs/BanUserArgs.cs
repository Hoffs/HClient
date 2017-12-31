using System;
using ChatProtos.Networking;
using ChatProtos.Networking.Messages;

namespace CoreClient.HMessageArgs
{
    public class BanUserArgs : EventArgs
    {
        public HConnection Connection { get; }
        public HEvents Events { get; }
        public ResponseStatus Status { get; }
        public BanUserMessageResponse Message { get; }

        public BanUserArgs(HConnection connection, HEvents events, ResponseStatus status, BanUserMessageResponse message)
        {
            Connection = connection;
            Events = events;
            Status = status;
            Message = message;
        }
    }
}