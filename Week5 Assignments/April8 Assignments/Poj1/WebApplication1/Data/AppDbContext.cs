using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using System;

namespace WebApplication1.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Person> Person { get; set; } = null!;
    }
}
