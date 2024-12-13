using GameApp.Infrastructure.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameApp.Model.Entities
{
    public class Categories : BaseEntity<int>
    {
        public string Name { get; set; }
        public DateTime LastTransactionDate { get; set; }
    }
}
