using GameApp.Infrastructure.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameApp.Model.Entities.UserEntities
{
    public class UserRoles : BaseEntity<int>
    {
        public int UserId { get; set; }
        public Int16 RoleId { get; set; }
    }
}
