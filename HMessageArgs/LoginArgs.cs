using System;
using ChatProtos.Networking.Messages;
using HServer.Networking;

namespace HChatClient.HMessageArgs
{
    public class LoginArgs : EventArgs
    {
        public LoginResponse Message { get; }
        public ResponseStatus Status { get; }
        public HChatEvents Events { get; }
        public HConnection Connection { get; }

        public LoginArgs(HConnection connection, HChatEvents events, ResponseStatus status, LoginResponse message)
        {
            Connection = connection;
            Events = events;
            Status = status;
            Message = message;
        }

    }
}