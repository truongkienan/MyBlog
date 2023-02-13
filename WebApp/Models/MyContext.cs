using Microsoft.EntityFrameworkCore;

namespace WebApp.Models
{
    public class MyContext:DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<CategoryAndTopic> CategoryAndTopics { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<ResponseLogin> ResponseLogins { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<RoleChecked> RoleCheckeds { get; set; }
        public DbSet<StatisticTopic> StatisticTopics { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<CategoryAndTopic>().HasNoKey();
            modelBuilder.Entity<ResponseLogin>().HasNoKey();
            modelBuilder.Entity<RoleChecked>().HasNoKey();
            modelBuilder.Entity<StatisticTopic>().HasNoKey();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            base.OnConfiguring(builder);
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();

            builder.UseSqlServer(configuration.GetConnectionString("MyBlog"));
        }
    }
}
