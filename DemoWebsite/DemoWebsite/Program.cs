// Copyright (c) 2019 connexion OG / Roman Wienicke
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace DemoWebsite
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
