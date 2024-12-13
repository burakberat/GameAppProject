using GameApp.Infrastructure.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameApp.Model.Entities
{
    public class Games: BaseEntity
    {
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public decimal Price { get; set; }
        public string Publisher { get; set; }
        public string Developer { get; set; }
        public DateTime ReleaseDate { get; set; }
        public virtual Categories Category { get; set; }
    }
}
