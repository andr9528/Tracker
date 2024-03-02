namespace Tracker.Shared.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
            var startup = new MasterStartup(builder.Configuration);

            startup.SetupServices(builder.Services);
            WebApplication app = builder.Build();
            startup.SetupApplication(app);

            app.Run();
        }
    }
}