using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zust.Core.Abstraction;

namespace Zust.Entities
{
    public class Comment:IEntity
    {
        public int Id { get; set; }

        public string Content { get; set; }
        public AppUser Sender { get; set; }
        public int PostId { get; set; }
        public Post Post { get; set; }

        public DateTime Time { get; set; }
    }
}
