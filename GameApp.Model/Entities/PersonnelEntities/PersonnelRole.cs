using GameApp.Infrastructure.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameApp.Model.Entities.PersonnelEntities
{
    public class PersonnelRole : BaseEntity<short>
    {
        public string RoleName { get; set; }
    }
}
