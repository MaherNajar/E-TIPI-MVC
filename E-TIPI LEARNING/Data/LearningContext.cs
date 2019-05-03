using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace E_TIPI_LEARNING.Models
{
    public class LearningContext : DbContext
    {
        public LearningContext (DbContextOptions<LearningContext> options)
            : base(options)
        {
        }

        public DbSet<LearningRessource> LearningRessources { get; set; }
    }
}
