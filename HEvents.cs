using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatProtos.Networking;
using CoreClient.HEventHandlers;
using CoreClient.HMessageArgs;

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

        

        public static void RegisterDefaultHandlers(HEvents events)
        {
            HDefaultHandlers defaultHandlers = new HDefaultHandlers();
            defaultHandlers.RegisterHandlers(events);
        }

        public virtual void OnLoginEventHandler(LoginArgs e)
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
