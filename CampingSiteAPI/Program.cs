namespace CampingSiteAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Maakt en start de host.
            CreateHostBuilder(args).Build().Run();
        }

        // Configureert de host builder.
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    // Specificeert dat de Startup-klasse moet worden gebruikt om de applicatie te configureren.
                    webBuilder.UseStartup<Startup>();
                });
    }
}
