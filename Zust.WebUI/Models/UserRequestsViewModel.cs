namespace Zust.WebUI.Models
{
    public class UserRequestsViewModel
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public bool HasRequestPending { get; set; }
        public bool HasReceivedRequest { get; set; }
        public  int RequestId { get; set; }
        public string? ReceiverId { get;  set; }
        public string? ImageUrl { get;  set; }
    }
}
