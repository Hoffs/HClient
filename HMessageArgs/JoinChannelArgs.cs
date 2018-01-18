using System;
using ChatProtos.Networking.Messages;
using HServer.Networking;

namespace HChatClient.HMessageArgs
{
    public class JoinChannelArgs : EventArgs
    {
        public JoinChannelResponse Message { get; }
        public ResponseStatus Status { get; }
        public HChatEvents Events { get; }
        public HConnection Connection { get; }

        public JoinChannelArgs(HConnection connection, HChatEvents events, ResponseStatus status, JoinChannelResponse message)
        {
            Connection = connection;
            Events = events;
            Status = status;
            Message = message;
        }

    }
}