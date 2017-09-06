using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace ServiceBrowser
{
    public class Program
    {
        public static void Main(string[] args)
            => CreateWebHost(args).Run();

        public static IWebHost CreateWebHost(string[] args)
            => WebHost.CreateDefaultBuilder()
                .UseStartup<Startup>()
                .Build();
    }
}