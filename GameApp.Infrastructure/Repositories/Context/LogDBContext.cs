using GameApp.Infrastructure.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameApp.Infrastructure.Repositories.Context
{
    public class LogDBContext : DbContext
    {
        public LogDBContext(DbContextOptions<LogDBContext> options) : base(options)
        {
        }
        public virtual DbSet<LogTable> LogTable { get; set; }
        public virtual DbSet<ErrorLogTable> ErrorLogTable { get; set; }
    }
}
