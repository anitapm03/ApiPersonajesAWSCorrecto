using ApiPersonajesAWSCorrecto.Data;
using ApiPersonajesAWSCorrecto.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ApiPersonajesAWSCorrecto;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container
    public void ConfigureServices(IServiceCollection services)
    {
        string connectionString =
            Configuration.GetConnectionString("MySql");

        services.AddTransient<RepositoryPersonajes>();

        services.AddDbContext<PersonajesContext>
            (options => options.UseMySql(connectionString,
            ServerVersion.AutoDetect(connectionString)));
        
        //en este tipo de apis al usar PROXY es necesario
        //habilitar cors para las peticiones
        services.AddCors(options =>
        {
            options.AddPolicy("AllowOrigin", x => x.AllowAnyOrigin());
        });

        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
            {
                Title = "Api aws personajes lunes",
                Version = "v1",
            });
        });

        services.AddControllers();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseCors(options => options.AllowAnyOrigin());

        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint(url: "swagger/v1/swagger.json",
                "ApiAWSPersonajes");
            options.RoutePrefix = "";
        });

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapGet("/", async context =>
            {
                await context.Response.WriteAsync("Welcome to running ASP.NET Core on AWS Lambda");
            });
        });
    }
}