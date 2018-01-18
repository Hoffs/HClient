using System;
using ChatProtos.Networking.Messages;
using HServer.Networking;

namespace HChatClient.HMessageArgs
{
    public class BanUserArgs : EventArgs
    {
        public HConnection Connection { get; }
        public HChatEvents Events { get; }
        public ResponseStatus Status { get; }
        public BanUserResponse Message { get; }

        public BanUserArgs(HConnection connection, HChatEvents events, ResponseStatus status, BanUserResponse message)
        {
            Connection = connection;
            Events = events;
            Status = status;
            Message = message;
        }
    }
}