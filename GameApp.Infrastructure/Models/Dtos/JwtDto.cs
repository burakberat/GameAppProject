using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameApp.Infrastructure.Models.Dtos
{
    public class JwtDto
    {
        public string Token { get; set; }
        public string RefToken { get; set; }
        public DateTime EndDate { get; set; }
    }
}
