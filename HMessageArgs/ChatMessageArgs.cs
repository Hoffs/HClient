using System;
using ChatProtos.Networking.Messages;
using HServer.Networking;

namespace HChatClient.HMessageArgs
{
    public class ChatMessageArgs : EventArgs
    {
        public HConnection Connection { get; }
        public HChatEvents Events { get; }
        public ResponseStatus Status { get; }
        public ChatMessageResponse Message { get; }

        public ChatMessageArgs(HConnection connection, HChatEvents events, ResponseStatus status, ChatMessageResponse message)
        {
            Connection = connection;
            Events = events;
            Status = status;
            Message = message;
        }
    }
}