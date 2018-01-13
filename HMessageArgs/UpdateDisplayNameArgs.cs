using System;
using HChatClient.ChatProtos.Networking;
using HChatClient.ChatProtos.Networking.Messages;

namespace HChatClient.HMessageArgs
{
    public class UpdateDisplayNameArgs : EventArgs
    {
        public HConnection Connection { get; }
        public HChatEvents Events { get; }
        public ResponseStatus Status { get; }
        public UpdateDisplayMessageResponse Message { get; }

        public UpdateDisplayNameArgs(HConnection connection, HChatEvents events, ResponseStatus status, UpdateDisplayMessageResponse message)
        {
            Connection = connection;
            Events = events;
            Status = status;
            Message = message;
        }
    }
}