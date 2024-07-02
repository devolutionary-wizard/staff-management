using dotenv.net;

namespace Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            DotEnv.Load();
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            var port = Environment.GetEnvironmentVariable("PORT");
            return Host.CreateDefaultBuilder(args)
                    .ConfigureWebHostDefaults(webBuilder =>
                    {
                        var url = $"http://localhost:{port}";
                        webBuilder.UseStartup<Startup>()
                        .UseUrls(url);
                    });
        }

    }
}