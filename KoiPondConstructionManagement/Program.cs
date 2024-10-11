using Microsoft.AspNetCore.Authentication.Cookies;

namespace KoiPondConstructionManagement
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();

            builder.Services.AddApplicationServices();
            builder.Services.AddInfrastructureServices(builder.Configuration);
            builder.Services.AddWebServices();

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
               .AddCookie(options =>
               {
                   options.LoginPath = "/LoginRegister"; // Đường dẫn đến trang đăng nhập
                   options.LogoutPath = "/Logout"; // Đường dẫn đến trang đăng xuất
                   options.SlidingExpiration = true; // Kích hoạt thời gian hết hạn trượt
               });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller}/{action=Index}/{id?}");

            app.UseRouting();
            app.UseAuthorization();
            app.MapRazorPages();

            app.Run();
        }
    }
}
