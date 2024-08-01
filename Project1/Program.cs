using Microsoft.AspNetCore;


namespace Project1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();

            builder.Build().Run();
        }
    }
}
