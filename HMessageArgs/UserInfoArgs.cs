using System;
using ChatProtos.Networking;
using ChatProtos.Networking.Messages;

namespace CoreClient.HMessageArgs
{
    public class UserInfoArgs : EventArgs
    {
        public HConnection Connection { get; }
        public HEvents Handlers { get; }
        public ResponseStatus Status { get; }
        public UserInfoMessageResponse Message { get; }

        public UserInfoArgs(HConnection connection, HEvents handlers, ResponseStatus status, UserInfoMessageResponse message)
        {
            Connection = connection;
            Handlers = handlers;
            Status = status;
            Message = message;
        }
    }
}