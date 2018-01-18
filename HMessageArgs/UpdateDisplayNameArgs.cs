using System;
using ChatProtos.Networking.Messages;
using HServer.Networking;

namespace HChatClient.HMessageArgs
{
    public class UpdateDisplayNameArgs : EventArgs
    {
        public HConnection Connection { get; }
        public HChatEvents Events { get; }
        public ResponseStatus Status { get; }
        public UpdateDisplayResponse Message { get; }

        public UpdateDisplayNameArgs(HConnection connection, HChatEvents events, ResponseStatus status, UpdateDisplayResponse message)
        {
            Connection = connection;
            Events = events;
            Status = status;
            Message = message;
        }
    }
}