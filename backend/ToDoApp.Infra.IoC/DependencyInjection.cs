using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ToDoApp.Application.Interfaces;
using ToDoApp.Application.Services;
using ToDoApp.Domain.Entities;
using ToDoApp.Domain.Repositories;
using ToDoApp.Infra.Data;
using ToDoApp.Infra.Data.Repositories;

namespace ToDoApp.Infra.IoC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ToDoDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), x => x.MigrationsAssembly(typeof(ToDoDbContext).Assembly.FullName)));

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<ITaskService, TaskService>();
            return services;
        }
    }
}
