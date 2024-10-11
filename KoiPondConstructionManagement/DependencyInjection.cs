namespace Microsoft.Extensions.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddWebServices(this IServiceCollection services)
        {
            //services.AddDatabaseDeveloperPageExceptionFilter();

            //services.AddScoped<IUser, CurrentUser>();

            //services.AddHttpContextAccessor();

            //services.AddHealthChecks()
            //    .AddDbContextCheck<ApplicationDbContext>();

            //services.AddExceptionHandler<CustomExceptionHandler>();

            //services.AddRazorPages();

            //// Customise default API behaviour
            //services.Configure<ApiBehaviorOptions>(options =>
            //    options.SuppressModelStateInvalidFilter = true);

            //services.AddEndpointsApiExplorer();

            //services.AddOpenApiDocument((configure, sp) =>
            //{
            //    configure.Title = "test API";

            //});

            return services;
        }
    }
}
