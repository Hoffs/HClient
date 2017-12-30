using System;
using ChatProtos.Networking;
using ChatProtos.Networking.Messages;

namespace CoreClient.HMessageArgs
{
    public class LogoutArgs : EventArgs
    {
        public LogoutMessageResponse Message { get; }
        public ResponseStatus Status { get; }
        public HEvents Handlers { get; }
        public HConnection Connection { get; }

        public LogoutArgs(HConnection connection, HEvents handlers, ResponseStatus status, LogoutMessageResponse message)
        {
            Connection = connection;
            Handlers = handlers;
            Status = status;
            Message = message;
        }

    }
}