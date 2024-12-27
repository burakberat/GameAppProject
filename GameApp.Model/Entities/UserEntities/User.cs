using GameApp.Infrastructure.Models.Entities;

namespace GameApp.Model.Entities.UserEntities
{
    public class User : BaseEntity<int>
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string EPosta { get; set; }
        public string PasswordHash { get; set; }
        public string Phone { get; set; }
    }
}
