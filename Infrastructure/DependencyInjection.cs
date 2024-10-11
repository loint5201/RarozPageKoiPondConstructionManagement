using Domain.Interfaces;
using Infrastructure.Data;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            //Guard.Against.Null(connectionString, message: "Connection string 'DefaultConnection' not found.");

            //services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
            //services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();

            services.AddDbContext<KoiPondConstructionManagementContext>((sp, options) =>
            {
                //options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());

                options.UseSqlServer(connectionString)
                .EnableSensitiveDataLogging()
                .LogTo(Console.WriteLine);
            });

            //services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

            //services.AddScoped<ApplicationDbContextInitialiser>();

            //services
            //    .AddDefaultIdentity<ApplicationUser>()
            //    .AddRoles<IdentityRole>()
            //    .AddEntityFrameworkStores<ApplicationDbContext>();

            //services.AddSingleton(TimeProvider.System);
            //services.AddTransient<IIdentityService, IdentityService>();

            //services.AddAuthorization(options =>
            //    options.AddPolicy(Policies.CanPurge, policy => policy.RequireRole(Roles.Administrator)));

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(BaseRepository<>));

            return services;
        }
    }
}
