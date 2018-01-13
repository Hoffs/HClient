using System;
using HChatClient.ChatProtos.Networking;
using HChatClient.ChatProtos.Networking.Messages;

namespace HChatClient.HMessageArgs
{
    public class LoginArgs : EventArgs
    {
        public LoginMessageResponse Message { get; }
        public ResponseStatus Status { get; }
        public HChatEvents Events { get; }
        public HConnection Connection { get; }

        public LoginArgs(HConnection connection, HChatEvents events, ResponseStatus status, LoginMessageResponse message)
        {
            Connection = connection;
            Events = events;
            Status = status;
            Message = message;
        }

    }
}