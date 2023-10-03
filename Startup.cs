using BlazorDapper;
using BlazorDapper.Data;
using BlazorDapper.Model;
using Microsoft.Data.Sqlite;


namespace BlazorDapper
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddSingleton<WeatherForecastService>();
            services.AddTransient<CarService>();
            services.AddTransient<Car>();
            var connectionString = Configuration.GetConnectionString("CarsDB");
            services.AddTransient<SqliteConnection>(_ => new  SqliteConnection(connectionString));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, CarService carService)
        {
            if(env.IsDevelopment()) 
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

           carService.InitializeDatabase();
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }


            


            
    








 

    }

}
