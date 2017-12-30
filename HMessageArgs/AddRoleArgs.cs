using System;
using ChatProtos.Networking;
using ChatProtos.Networking.Messages;

namespace CoreClient.HMessageArgs
{
    public class AddRoleArgs : EventArgs
    {
        public HConnection Connection { get; }
        public HEvents Handlers { get; }
        public ResponseStatus Status { get; }
        public AddRoleMessageResponse Message { get; }

        public AddRoleArgs(HConnection connection, HEvents handlers, ResponseStatus status, AddRoleMessageResponse message)
        {
            Connection = connection;
            Handlers = handlers;
            Status = status;
            Message = message;
        }
    }
}