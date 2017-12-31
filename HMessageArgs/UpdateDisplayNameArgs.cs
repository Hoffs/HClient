using System;
using ChatProtos.Networking;
using ChatProtos.Networking.Messages;

namespace CoreClient.HMessageArgs
{
    public class UpdateDisplayNameArgs : EventArgs
    {
        public HConnection Connection { get; }
        public HEvents Events { get; }
        public ResponseStatus Status { get; }
        public UpdateDisplayMessageResponse Message { get; }

        public UpdateDisplayNameArgs(HConnection connection, HEvents events, ResponseStatus status, UpdateDisplayMessageResponse message)
        {
            Connection = connection;
            Events = events;
            Status = status;
            Message = message;
        }
    }
}