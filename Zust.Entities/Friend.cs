using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zust.Core.Abstraction;

namespace Zust.Entities
{
    public class Friend:IEntity
    {
        public int Id { get; set; }
        public string? OwnId { get; set; }
        public string? YourFriendId { get; set; }
        public virtual AppUser? YourFriend { get; set; }
    }
}
