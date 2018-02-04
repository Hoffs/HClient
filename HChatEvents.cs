using System;
using System.Threading.Tasks;
using ChatProtos.Networking;
using ChatProtos.Networking.Messages;
using Google.Protobuf;
using HChatClient.HMessageArgs;
using HServer.Networking;
using JetBrains.Annotations;

namespace HChatClient
{
    public class HChatEvents
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

        public async Task InvokeEvent([NotNull] HClient client, [NotNull] byte[] message)
        {
            await Task.Yield();
            try
            {
                var responseMessage = ResponseMessage.Parser.ParseFrom(message);
                switch ((RequestType)responseMessage.Type)
                {
                    case RequestType.Login:
                        var login = LoginResponse.Parser.ParseFrom(responseMessage.Message);
                        OnLoginEventHandler(new LoginArgs(client.GetConnection(), this, responseMessage.Status, login));
                        break;
                    case RequestType.Logout:
                        var logout = LogoutResponse.Parser.ParseFrom(responseMessage.Message);
                        OnLogoutEventHandler(new LogoutArgs(client.GetConnection(), this, responseMessage.Status, logout));
                        break;
                    case RequestType.JoinChannel:
                        Console.WriteLine("[CLIENT] Handler count: {0}", JoinChannelEventHandler?.GetInvocationList().Length);
                        var joinChannel = JoinChannelResponse.Parser.ParseFrom(responseMessage.Message);
                        OnJoinChannelEventHandler(
                            new JoinChannelArgs(client.GetConnection(), this, responseMessage.Status, joinChannel));
                        break;
                    case RequestType.LeaveChannel:
                        var leaveChannel = LeaveChannelResponse.Parser.ParseFrom(responseMessage.Message);
                        OnLeaveChannelEventHandler(
                            new LeaveChannelArgs(client.GetConnection(), this, responseMessage.Status, leaveChannel));
                        break;
                    case RequestType.BanUser:
                        var banUser = BanUserResponse.Parser.ParseFrom(responseMessage.Message);
                        OnBanUserEventHandler(new BanUserArgs(client.GetConnection(), this, responseMessage.Status, banUser));
                        break;
                    case RequestType.KickUser:
                        var kickUser = KickUserResponse.Parser.ParseFrom(responseMessage.Message);
                        OnKickUserEventHandler(
                            new KickUserArgs(client.GetConnection(), this, responseMessage.Status, kickUser));
                        break;
                    case RequestType.AddRole:
                        var addRole = AddRoleResponse.Parser.ParseFrom(responseMessage.Message);
                        OnAddRoleEventHandler(new AddRoleArgs(client.GetConnection(), this, responseMessage.Status, addRole));
                        break;
                    case RequestType.RemoveRole:
                        var removeRole = RemoveRoleResponse.Parser.ParseFrom(responseMessage.Message);
                        OnRemoveRoleEventHandler(
                            new RemoveRoleArgs(client.GetConnection(), this, responseMessage.Status, removeRole));
                        break;
                    case RequestType.UpdateDisplayName:
                        var updateDisplayName = UpdateDisplayResponse.Parser.ParseFrom(responseMessage.Message);
                        OnUpdateDisplayNamEventHandler(
                            new UpdateDisplayNameArgs(
                                client.GetConnection(),
                                this,
                                responseMessage.Status,
                                updateDisplayName));
                        break;
                    case RequestType.UserInfo:
                        var userInfo = UserInfoResponse.Parser.ParseFrom(responseMessage.Message);
                        OnUserInfoEventHandler(
                            new UserInfoArgs(client.GetConnection(), this, responseMessage.Status, userInfo));
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

        }

        /// <summary>
        /// Method to be called on Login Event.
        /// </summary>
        /// <param name="e">
        /// The login arguments.
        /// </param>
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
