using System;
using ChatProtos.Networking.Messages;
using HServer.Networking;

namespace HChatClient.HMessageArgs
{
    public class AddRoleArgs : EventArgs
    {
        public HConnection Connection { get; }
        public HChatEvents Events { get; }
        public ResponseStatus Status { get; }
        public AddRoleResponse Message { get; }

        public AddRoleArgs(HConnection connection, HChatEvents events, ResponseStatus status, AddRoleResponse message)
        {
            Connection = connection;
            Events = events;
            Status = status;
            Message = message;
        }
    }
}