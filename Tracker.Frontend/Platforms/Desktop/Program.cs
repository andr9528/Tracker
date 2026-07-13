using Uno.UI.Hosting;

namespace Tracker.Frontend;

internal class Program
{
    [STAThread]
    public static async Task Main(string[] args)
    {

        var host = UnoPlatformHostBuilder.Create()
            .App(() => new App())
            .UseX11()
            .UseLinuxFrameBuffer()
            .UseMacOS()
            .UseWin32()
            .Build();

        await host.RunAsync();
    }
}
