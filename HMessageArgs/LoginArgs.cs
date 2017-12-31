using System;
using ChatProtos.Networking;
using ChatProtos.Networking.Messages;

namespace CoreClient.HMessageArgs
{
    public class LoginArgs : EventArgs
    {
        public LoginMessageResponse Message { get; }
        public ResponseStatus Status { get; }
        public HEvents Events { get; }
        public HConnection Connection { get; }

        public LoginArgs(HConnection connection, HEvents events, ResponseStatus status, LoginMessageResponse message)
        {
            Connection = connection;
            Events = events;
            Status = status;
            Message = message;
        }

    }
}