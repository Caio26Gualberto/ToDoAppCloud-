using Microsoft.EntityFrameworkCore;
using ToDoApp.Domain.Entities;
using ToDoApp.Domain.Enums;

namespace ToDoApp.Infra.Data
{
    public class ToDoDbContext : DbContext
    {
        public ToDoDbContext(DbContextOptions<ToDoDbContext> options) : base(options) { }

        public DbSet<TaskItem> Tasks { get; set; }
        public DbSet<TaskCategoryItem> TasksCategories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TaskItem>()
                .HasMany(t => t.TaskCategories)
                .WithOne(t => t.TaskItem)
                .HasForeignKey(t => t.TaskItemId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    } 
}
