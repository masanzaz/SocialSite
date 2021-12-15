using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Social.Application.Interfaces;
using Social.Application.Interfaces.Repositories;
using Social.Infrastructure.Persistence;
using Social.Infrastructure.Repositories;
using Social.Infrastructure.Services;

namespace Social.Infrastructure
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
               options.UseSqlServer(
                   configuration.GetConnectionString("DefaultConnection"),
                //b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
                b => b.MigrationsAssembly("SocialSite")));
            services.AddTransient<IDateTime, DateTimeService>();


            #region Repositories
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient<IDisabilityRepository, DisabilityRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IGenreRepository, GenreRepository>();
            services.AddTransient<IHobbyRepository, HobbyRepository>();
            services.AddTransient<IPersonRepository, PersonRepository>();

            services.AddTransient<IAccountService, AccountService>();
            #endregion

            return services;
        }
    }
}
