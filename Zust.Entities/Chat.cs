using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zust.Core.Abstraction;

namespace Zust.Entities
{
    public class Chat:IEntity
    {
        public int Id { get; set; }
        public string? ReceiverId { get; set; }
        public virtual AppUser? Receiver { get; set; }
        public string? SenderId { get; set; }
        public virtual List<Message>? Messages { get; set; }
        public Chat()
        {
            Messages = new List<Message>();
        }
    }
}
