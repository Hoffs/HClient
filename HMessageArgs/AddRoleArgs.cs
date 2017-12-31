using System;
using ChatProtos.Networking;
using ChatProtos.Networking.Messages;

namespace CoreClient.HMessageArgs
{
    public class AddRoleArgs : EventArgs
    {
        public HConnection Connection { get; }
        public HEvents Events { get; }
        public ResponseStatus Status { get; }
        public AddRoleMessageResponse Message { get; }

        public AddRoleArgs(HConnection connection, HEvents events, ResponseStatus status, AddRoleMessageResponse message)
        {
            Connection = connection;
            Events = events;
            Status = status;
            Message = message;
        }
    }
}