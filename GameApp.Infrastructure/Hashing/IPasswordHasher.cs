using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameApp.Infrastructure.Hashing
{
    public interface IPasswordHasher
    {
        string Hash(string password);
    }
}
