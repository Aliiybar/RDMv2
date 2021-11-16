using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Sign.Models.Entities;
using System;

namespace Sign.Db
{
    public class SignContext:DbContext
    {
 
 
        public SignContext(DbContextOptions<SignContext> options) : base(options)
        {
        }
        
        public DbSet<Models.Entities.Sign> Signs { get; set; }


    }
}
