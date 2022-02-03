using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models
{
    public class FilmDBContext:DbContext
    {
        public FilmDBContext(DbContextOptions<FilmDBContext> options) : base(options)
        { }
            public DbSet<UserInformations> userinfo { get; set; }
        public DbSet<Film> film { get; set; }


    

    }
}
