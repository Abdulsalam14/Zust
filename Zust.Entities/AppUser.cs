using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zust.Core.Abstraction;

namespace Zust.Entities
{
    public class AppUser:IdentityUser,IEntity
    {

        public string? ImageUrl {  get; set; }
        public string? CoverImageUrl {  get; set; }
        public bool IsOnline { get; set; }
        public DateTime DisconnectTime { get; set; } = DateTime.Now;
        public string ConnectTime { get; set; } = "";
        public bool IsFriend { get; set; }
        public bool HasRequestPending { get; set; }
        public bool HasReceivedRequest { get; set; }
        public int LastChatId { get; set; }
        public virtual ICollection<Friend>? Friends { get; set; }
        public virtual ICollection<FriendRequest>? FriendRequests { get; set; }
        public virtual ICollection<Notification>? Notifications { get; set; }
        public virtual ICollection<Chat>? Chats { get; set; }
        public virtual ICollection<Post>? Posts { get; set; }
        public virtual ICollection<Comment>? Comments { get; set; }


        public AppUser()
        {
            Friends = new List<Friend>();
            FriendRequests = new List<FriendRequest>();
            Notifications = new List<Notification>();
            Chats = new List<Chat>();
            Comments = new List<Comment>();
            Posts = new List<Post>();
        }
    }
}
