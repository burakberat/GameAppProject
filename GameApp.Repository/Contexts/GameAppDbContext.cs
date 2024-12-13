using GameApp.Model.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameApp.Repository.Contexts
{
    public class GameAppDbContext: DbContext
    {
        public GameAppDbContext(DbContextOptions<GameAppDbContext> options) : base(options)
        {
        }

        public DbSet<Games> Games { get; set; }
        public DbSet<Categories> Categories { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Designation> Designation { get; set; }
    }
}
