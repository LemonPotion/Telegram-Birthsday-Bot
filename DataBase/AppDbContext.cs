using Microsoft.EntityFrameworkCore;
namespace WebApplication3.DataBase
{
    public class AppDbContext: DbContext
    {
        protected readonly IConfiguration configuration;

        public AppDbContext(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)//метод из DbContext
        {
            optionsBuilder.UseNpgsql(configuration.GetConnectionString("ApiDb")) ;//Подключение бд
        }
        public DbSet<Employees> entities { get; set; }//сотрудники - модель
    }
}
