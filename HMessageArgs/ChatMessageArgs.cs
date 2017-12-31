using System;
using ChatProtos.Networking;
using ChatProtos.Networking.Messages;

namespace CoreClient.HMessageArgs
{
    public class ChatMessageArgs : EventArgs
    {
        public HConnection Connection { get; }
        public HEvents Events { get; }
        public ResponseStatus Status { get; }
        public ChatMessageResponse Message { get; }

        public ChatMessageArgs(HConnection connection, HEvents events, ResponseStatus status, ChatMessageResponse message)
        {
            Connection = connection;
            Events = events;
            Status = status;
            Message = message;
        }
    }
}