﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameApp.Model.Dtos
{
    public class AddRoleDto: BaseDto
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
    }
}
