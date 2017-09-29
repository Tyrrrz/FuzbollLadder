using System;
using System.Reflection;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace FuzbollLadder
{
    public class Program
    {
        public static Version Version { get; private set; }

        public static void Main(string[] args)
        {
            Version = Assembly.GetEntryAssembly().GetName().Version;
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseSetting("detailedErrors", "true")
                .UseStartup<Startup>()
                .CaptureStartupErrors(true)
                .Build();
    }
}