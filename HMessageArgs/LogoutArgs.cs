using System;
using ChatProtos.Networking;
using ChatProtos.Networking.Messages;

namespace CoreClient.HMessageArgs
{
    public class LogoutArgs : EventArgs
    {
        public LogoutMessageResponse Message { get; }
        public ResponseStatus Status { get; }
        public HEvents Events { get; }
        public HConnection Connection { get; }

        public LogoutArgs(HConnection connection, HEvents events, ResponseStatus status, LogoutMessageResponse message)
        {
            Connection = connection;
            Events = events;
            Status = status;
            Message = message;
        }

    }
}