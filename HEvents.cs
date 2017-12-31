using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatProtos.Networking;
using ChatProtos.Networking.Messages;
using CoreClient.HEventHandlers;
using CoreClient.HMessageArgs;
using Google.Protobuf;
using JetBrains.Annotations;

namespace CoreClient
{
    public class HEvents
    {
        public event EventHandler<LoginArgs> LoginEventHandler;
        public event EventHandler<LogoutArgs> LogoutEventHandler;
        public event EventHandler<JoinChannelArgs> JoinChannelEventHandler;
        public event EventHandler<LeaveChannelArgs> LeaveChannelEventHandler;
        public event EventHandler<AddRoleArgs> AddRoleEventHandler;
        public event EventHandler<RemoveRoleArgs> RemoveRoleEventHandler;
        public event EventHandler<UpdateDisplayNameArgs> UpdateDisplayNamEventHandler;
        public event EventHandler<UserInfoArgs> UserInfoEventHandler;
        public event EventHandler<BanUserArgs> BanUserEventHandler;
        public event EventHandler<KickUserArgs> KickUserEventHandler;
        public event EventHandler<ChatMessageArgs> ChatMessagEventHandler;

        // Could maybe be async for speed?
        public void InvokeEvent([NotNull] HClient client, [NotNull] ResponseMessage message)
        {
            try
            {
                switch (message.Type)
                {
                    case RequestType.Login:
                        var login = LoginMessageResponse.Parser.ParseFrom(message.Message);
                        OnLoginEventHandler(new LoginArgs(client.GetConnection(), this, message.Status, login));
                        break;
                    case RequestType.Logout:
                        var logout = LogoutMessageResponse.Parser.ParseFrom(message.Message);
                        OnLogoutEventHandler(new LogoutArgs(client.GetConnection(), this, message.Status, logout));
                        break;
                    case RequestType.JoinChannel:
                        var joinChannel = JoinChannelMessageResponse.Parser.ParseFrom(message.Message);
                        OnJoinChannelEventHandler(new JoinChannelArgs(client.GetConnection(), this, message.Status,
                            joinChannel));
                        break;
                    case RequestType.LeaveChannel:
                        var leaveChannel = LeaveChannelMessageResponse.Parser.ParseFrom(message.Message);
                        OnLeaveChannelEventHandler(new LeaveChannelArgs(client.GetConnection(), this, message.Status,
                            leaveChannel));
                        break;
                    case RequestType.BanUser:
                        var banUser = BanUserMessageResponse.Parser.ParseFrom(message.Message);
                        OnBanUserEventHandler(new BanUserArgs(client.GetConnection(), this, message.Status, banUser));
                        break;
                    case RequestType.KickUser:
                        var kickUser = KickUserMessageResponse.Parser.ParseFrom(message.Message);
                        OnKickUserEventHandler(new KickUserArgs(client.GetConnection(), this, message.Status,
                            kickUser));
                        break;
                    case RequestType.AddRole:
                        var addRole = AddRoleMessageResponse.Parser.ParseFrom(message.Message);
                        OnAddRoleEventHandler(new AddRoleArgs(client.GetConnection(), this, message.Status, addRole));
                        break;
                    case RequestType.RemoveRole:
                        var removeRole = RemoveRoleMessageResponse.Parser.ParseFrom(message.Message);
                        OnRemoveRoleEventHandler(new RemoveRoleArgs(client.GetConnection(), this, message.Status,
                            removeRole));
                        break;
                    case RequestType.UpdateDisplayName:
                        var updateDisplayName = UpdateDisplayMessageResponse.Parser.ParseFrom(message.Message);
                        OnUpdateDisplayNamEventHandler(new UpdateDisplayNameArgs(client.GetConnection(), this,
                            message.Status, updateDisplayName));
                        break;
                    case RequestType.UserInfo:
                        var userInfo = UserInfoMessageResponse.Parser.ParseFrom(message.Message);
                        OnUserInfoEventHandler(new UserInfoArgs(client.GetConnection(), this, message.Status,
                            userInfo));
                        break;
                    case RequestType.ChatMessage:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            catch (InvalidProtocolBufferException ex)
            {
                Console.WriteLine("Couldn't parse Protobuf message");
                Console.WriteLine(ex);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine("No such request type");
                Console.WriteLine(ex);
            }

        }

        public static void RegisterDefaultHandlers(HEvents events)
        {
            var defaultHandlers = new HDefaultHandlers();
            defaultHandlers.RegisterHandlers(events);
        }

        protected virtual void OnLoginEventHandler(LoginArgs e)
        {
            LoginEventHandler?.Invoke(this, e);
        }

        protected virtual void OnLogoutEventHandler(LogoutArgs e)
        {
            LogoutEventHandler?.Invoke(this, e);
        }

        protected virtual void OnJoinChannelEventHandler(JoinChannelArgs e)
        {
            JoinChannelEventHandler?.Invoke(this, e);
        }

        protected virtual void OnLeaveChannelEventHandler(LeaveChannelArgs e)
        {
            LeaveChannelEventHandler?.Invoke(this, e);
        }

        protected virtual void OnAddRoleEventHandler(AddRoleArgs e)
        {
            AddRoleEventHandler?.Invoke(this, e);
        }

        protected virtual void OnRemoveRoleEventHandler(RemoveRoleArgs e)
        {
            RemoveRoleEventHandler?.Invoke(this, e);
        }

        protected virtual void OnUpdateDisplayNamEventHandler(UpdateDisplayNameArgs e)
        {
            UpdateDisplayNamEventHandler?.Invoke(this, e);
        }

        protected virtual void OnUserInfoEventHandler(UserInfoArgs e)
        {
            UserInfoEventHandler?.Invoke(this, e);
        }

        protected virtual void OnBanUserEventHandler(BanUserArgs e)
        {
            BanUserEventHandler?.Invoke(this, e);
        }

        protected virtual void OnKickUserEventHandler(KickUserArgs e)
        {
            KickUserEventHandler?.Invoke(this, e);
        }

        protected virtual void OnChatMessagEventHandler(ChatMessageArgs e)
        {
            ChatMessagEventHandler?.Invoke(this, e);
        }
    }
}
