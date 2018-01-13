using System;
using HChatClient.ChatProtos.Networking;
using HChatClient.ChatProtos.Networking.Messages;

namespace HChatClient.HMessageArgs
{
    public class RemoveRoleArgs : EventArgs
    {
        public HConnection Connection { get; }
        public HChatEvents Events { get; }
        public ResponseStatus Status { get; }
        public RemoveRoleMessageResponse Message { get; }

        public RemoveRoleArgs(HConnection connection, HChatEvents events, ResponseStatus status, RemoveRoleMessageResponse message)
        {
            Connection = connection;
            Events = events;
            Status = status;
            Message = message;
        }
    }
}