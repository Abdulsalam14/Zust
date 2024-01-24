using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zust.Core.Abstraction;

namespace Zust.Entities
{
    public class Post:IEntity
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string? ImageUrl { get; set; }
        public string? VideoUrl { get; set; }
        public int LikeCount { get; set; }
        public DateTime Time { get; set; }
        public List<Comment> Comments { get; set; }
        public AppUser User { get; set; }

        public string UserId { get; set; }
    }
}
