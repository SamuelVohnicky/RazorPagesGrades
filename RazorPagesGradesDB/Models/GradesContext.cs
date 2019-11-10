using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RazorPagesGrades.Models
{
    public class GradesContext : DbContext
    {
        public GradesContext(DbContextOptions<GradesContext> options) : base(options)
        {
        }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Grade> Grades { get; set; }
        
    }
}
