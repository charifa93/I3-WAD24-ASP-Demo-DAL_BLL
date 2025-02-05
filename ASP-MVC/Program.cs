using ASP_MVC.Handlers;
using Common.Repositories;

namespace ASP_MVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            //ajoute d'implrmentation du service d'acces a l'httpContext
            //(dans le but d'atteindre nos variable 
            

            //Ajout Configuration de session :
            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddSession(
                options =>
                {
                    options.Cookie.Name = "CookieWad24";
                    options.Cookie.HttpOnly = true;
                    options.Cookie.IsEssential = true;
                    options.IdleTimeout = TimeSpan.FromSeconds(10);
                }
                );
            builder.Services.Configure<CookiePolicyOptions>(options => {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            //Ajoute de notre service de sessionManager
            builder.Services.AddScoped<SessionManager>();

            //Ajout de nos services : Ceux de la BLL et ceux de la DAL
            builder.Services.AddScoped<IUserRepository<BLL.Entities.User>, BLL.Services.UserService>();
            builder.Services.AddScoped<IUserRepository<DAL.Entities.User>, DAL.Services.UserService>();

            builder.Services.AddScoped<ICocktailRepository<BLL.Entities.Cocktail> , BLL.Services.CocktailService>();
            builder.Services.AddScoped<ICocktailRepository<DAL.Entities.Cocktail>, DAL.Services.CocktailService>();

             var app = builder.Build();


            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //demarrer la Session 
            app.UseSession();
            // des regles pour Utiliser la session
            app.UseCookiePolicy();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
