using System;
using ChatProtos.Networking;
using ChatProtos.Networking.Messages;

namespace CoreClient.HMessageArgs
{
    public class RemoveRoleArgs : EventArgs
    {
        public HConnection Connection { get; }
        public HEvents Events { get; }
        public ResponseStatus Status { get; }
        public RemoveRoleMessageResponse Message { get; }

        public RemoveRoleArgs(HConnection connection, HEvents events, ResponseStatus status, RemoveRoleMessageResponse message)
        {
            Connection = connection;
            Events = events;
            Status = status;
            Message = message;
        }
    }
}