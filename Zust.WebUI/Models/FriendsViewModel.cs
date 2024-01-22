using Zust.Entities;

namespace Zust.WebUI.Models
{
    public class FriendsViewModel
    {
        //public List<AppUser> YouKnowUsers { get; set; }
        //public List<FriendRequest> FriendRequests { get; set; }

        public List<UserRequestsViewModel> UserRequests { get; set; }
        public  string  CurrentUserId { get; set; }
    }
}
