using GameApp.Infrastructure.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameApp.Model.Dtos
{
    public class GameListDto: BaseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CategoryName { get; set; }        
    }
}
