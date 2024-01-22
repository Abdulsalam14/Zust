using Zust.Entities;

namespace Zust.WebUI.Models
{
    public class ChatViewModel
    {
        public AppUser Sender { get; set; }

        public string LastMessage {  get; set; }
        public int HasntSeenCount {  get; set; }
        public DateTime Time { get; set; }
    }
}
