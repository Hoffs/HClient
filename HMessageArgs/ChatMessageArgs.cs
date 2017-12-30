using System;
using ChatProtos.Networking;
using ChatProtos.Networking.Messages;

namespace CoreClient.HMessageArgs
{
    public class ChatMessageArgs : EventArgs
    {
        public HConnection Connection { get; }
        public HEvents Handlers { get; }
        public ResponseStatus Status { get; }
        public ChatMessageResponse Message { get; }

        public ChatMessageArgs(HConnection connection, HEvents handlers, ResponseStatus status, ChatMessageResponse message)
        {
            Connection = connection;
            Handlers = handlers;
            Status = status;
            Message = message;
        }
    }
}