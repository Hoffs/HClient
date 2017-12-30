using System;
using ChatProtos.Networking;
using ChatProtos.Networking.Messages;

namespace CoreClient.HMessageArgs
{
    public class JoinChannelArgs : EventArgs
    {
        public JoinChannelMessageResponse Message { get; }
        public ResponseStatus Status { get; }
        public HEvents Handlers { get; }
        public HConnection Connection { get; }

        public JoinChannelArgs(HConnection connection, HEvents handlers, ResponseStatus status, JoinChannelMessageResponse message)
        {
            Connection = connection;
            Handlers = handlers;
            Status = status;
            Message = message;
        }

    }
}