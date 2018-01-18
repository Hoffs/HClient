using System;
using ChatProtos.Networking.Messages;
using HServer.Networking;

namespace HChatClient.HMessageArgs
{
    public class UserInfoArgs : EventArgs
    {
        public HConnection Connection { get; }
        public HChatEvents Events { get; }
        public ResponseStatus Status { get; }
        public UserInfoResponse Message { get; }

        public UserInfoArgs(HConnection connection, HChatEvents events, ResponseStatus status, UserInfoResponse message)
        {
            Connection = connection;
            Events = events;
            Status = status;
            Message = message;
        }
    }
}