using Microsoft.EntityFrameworkCore;
namespace WebApplication3.DataBase
{
    public class AppDbContext: DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)//метод из DbContext
        {
            optionsBuilder.UseNpgsql("Host=localhost; Database=Employees; Username=postgres; Password=123") ;//Подключение бд
        }
        public DbSet<Employees> entities { get; set; }//сотрудники - модель
    }
}
