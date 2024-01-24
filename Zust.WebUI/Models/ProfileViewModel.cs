using Zust.Entities;

namespace Zust.WebUI.Models
{
    public class ProfileViewModel
    {
        public AppUser CurrentUser { get; set; }
        public List<Post> Posts { get; set; }
    }
}
