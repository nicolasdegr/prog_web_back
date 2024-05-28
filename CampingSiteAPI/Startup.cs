public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // ConfigureServices configureert de services die door de applicatie worden gebruikt.
    public void ConfigureServices(IServiceCollection services)
    {
        // Voegt controllers toe aan de services collectie.
        services.AddControllers();
        // Voegt de LiteDbContext als singleton service toe.
        services.AddSingleton<LiteDbContext>();

        // Add CORS
        services.AddCors(options =>
        {
            options.AddDefaultPolicy(builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyHeader()
                       .AllowAnyMethod();
            });
        });
        // Voegt Swagger toe voor API documentatie.
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "CampingSiteAPI", Version = "v1" });
        });
    }
    // Configure configureert de HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        // Specifieke configuratie voor de ontwikkelingsomgeving.
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CampingSiteAPI v1"));
        }

        app.UseRouting();

        // gebruik CORS (noodzakelijk!)
        app.UseCors();

        app.UseAuthorization();
        // Configureert de endpoints voor de applicatie.
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}
