using System;
using ChatProtos.Networking.Messages;
using HServer.Networking;

namespace HChatClient.HMessageArgs
{
    public class LogoutArgs : EventArgs
    {
        public LogoutResponse Message { get; }
        public ResponseStatus Status { get; }
        public HChatEvents Events { get; }
        public HConnection Connection { get; }

        public LogoutArgs(HConnection connection, HChatEvents events, ResponseStatus status, LogoutResponse message)
        {
            Connection = connection;
            Events = events;
            Status = status;
            Message = message;
        }

    }
}