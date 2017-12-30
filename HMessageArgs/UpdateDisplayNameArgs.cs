using System;
using ChatProtos.Networking;
using ChatProtos.Networking.Messages;

namespace CoreClient.HMessageArgs
{
    public class UpdateDisplayNameArgs : EventArgs
    {
        public HConnection Connection { get; }
        public HEvents Handlers { get; }
        public ResponseStatus Status { get; }
        public UpdateDisplayMessageResponse Message { get; }

        public UpdateDisplayNameArgs(HConnection connection, HEvents handlers, ResponseStatus status, UpdateDisplayMessageResponse message)
        {
            Connection = connection;
            Handlers = handlers;
            Status = status;
            Message = message;
        }
    }
}