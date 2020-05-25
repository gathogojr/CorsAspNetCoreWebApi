using CorsAspNetCoreWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CorsAspNetCoreWebApi.Data
{
    public class MoviesDbContext : DbContext
    {
        public MoviesDbContext(DbContextOptions<MoviesDbContext> options) : base(options)
        {
        }

        public DbSet<Movie> Movies { get; set; }
    }
}
