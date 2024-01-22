namespace Zust.WebUI.Models
{
    public class MessageModel
    {
        public string SenderId { get; set; }
        public string ReceiverId { get; set; }
        public string?Content { get; set; }
        public int ChatId {  get; set; }
    }
}
