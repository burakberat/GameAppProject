using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameApp.Model.Dtos.PersonnelDtos
{
    public class PersonnelRegisterDto: BaseDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string EPosta { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
    }
}
