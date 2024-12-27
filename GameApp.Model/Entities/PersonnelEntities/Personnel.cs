using GameApp.Infrastructure.Models.Entities;

namespace GameApp.Model.Entities.PersonnelEntities
{
    public class Personnel : BaseEntity<int>
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string EPosta { get; set; }
        public string PasswordHash { get; set; }
        public string Phone { get; set; }
    }
}
