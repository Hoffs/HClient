using System;
using HChatClient.ChatProtos.Networking;
using HChatClient.ChatProtos.Networking.Messages;

namespace HChatClient.HMessageArgs
{
    public class KickUserArgs : EventArgs
    {
        public HConnection Connection { get; }
        public HChatEvents Events { get; }
        public ResponseStatus Status { get; }
        public KickUserMessageResponse Message { get; }

        public KickUserArgs(HConnection connection, HChatEvents events, ResponseStatus status, KickUserMessageResponse message)
        {
            Connection = connection;
            Events = events;
            Status = status;
            Message = message;
        }
    }
}