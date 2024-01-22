using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zust.Core.Abstraction;

namespace Zust.Entities
{
    public class FriendRequest:IEntity
    {
        public int Id { get; set; }
        public string? Content { get; set; }
        public string? SenderId { get; set; }
        public virtual AppUser? Sender { get; set; }
        public string? ReceiverId { get; set; }
    }
}
