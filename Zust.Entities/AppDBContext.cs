﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zust.Entities
{
    public class AppDBContext:IdentityDbContext<AppUser,AppRole,string>
    {
        public AppDBContext(DbContextOptions<AppDBContext> options)
            :base(options)
        {
            
        }
    }
}
