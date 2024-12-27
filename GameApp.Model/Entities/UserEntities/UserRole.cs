using GameApp.Infrastructure.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameApp.Model.Entities.UserEntities
{
    public class UserRole : BaseEntity<short>
    {
        public string RoleName { get; set; }
    }
}
