using GameApp.Model.Dtos.PersonnelDtos;
using GameApp.Model.Dtos.UserDtos;
using GameApp.Model.Entities;
using GameApp.Model.Entities.PersonnelEntities;
using GameApp.Model.Entities.UserEntities;
using Microsoft.EntityFrameworkCore;

namespace GameApp.Repository.Contexts
{
    public class GameAppDbContext : DbContext
    {
        public GameAppDbContext(DbContextOptions<GameAppDbContext> options) : base(options)
        {
        }

        public DbSet<Games> Games { get; set; }
        public DbSet<Categories> Categories { get; set; }

        //User
        public DbSet<User> User { get; set; }
        public DbSet<UserRole> UserRole { get; set; }        
        public DbSet<UserRoles> UserRoles { get; set; }
        public DbSet<UserDetailDto> UserDetailDtos { get; set; }

        //Personnel
        public DbSet<Personnel> Personnel { get; set; }
        public DbSet<PersonnelRole> PersonnelRole { get; set; }
        public DbSet<PersonnelRoles> PersonnelRoles { get; set; }
        public DbSet<PersonnelDetailDto> PersonnelDetailDtos { get; set; }

    }
}
