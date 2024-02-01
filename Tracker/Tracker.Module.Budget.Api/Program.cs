namespace Tracker.Module.Budget.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var startup = new BudgetStartup(builder.Configuration);

            startup.SetupServices(builder.Services);
            WebApplication app = builder.Build();
            startup.SetupApplication(app);

            app.Run();
        }
    }
}