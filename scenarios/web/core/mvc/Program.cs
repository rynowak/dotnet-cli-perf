﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace core
{
    public class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length >= 1 && args[0].Equals("--mode=singleRequest", StringComparison.OrdinalIgnoreCase)) {
                var sw = Stopwatch.StartNew();
                BuildWebHost(args).RunAsync();
                var response = (new HttpClient()).GetStringAsync("http://localhost:5000").Result;
                sw.Stop();

                Console.WriteLine(response);
                Console.WriteLine(sw.Elapsed);
            }
            else {
                BuildWebHost(args).Run();
            }
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
