using Lab37Erik.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab37Erik.Data
{
    public class GamesDbContext : DbContext 
    {
        public GamesDbContext(DbContextOptions<GamesDbContext> options) : base(options)
        {

        }

        public DbSet<Games> Games { get; set; }
    }
}
