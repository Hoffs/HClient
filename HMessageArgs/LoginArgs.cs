using System;
using ChatProtos.Networking;
using ChatProtos.Networking.Messages;

namespace CoreClient.HMessageArgs
{
    public class LoginArgs : EventArgs
    {
        public LoginMessageResponse Message { get; }
        public ResponseStatus Status { get; }
        public HEvents Handlers { get; }
        public HConnection Connection { get; }

        public LoginArgs(HConnection connection, HEvents handlers, ResponseStatus status, LoginMessageResponse message)
        {
            Connection = connection;
            Handlers = handlers;
            Status = status;
            Message = message;
        }

    }
}