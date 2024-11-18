// -----------------------------------------------------------------------
//  <copyright file="Program.cs" company="Hancock Software Solutions Limited">
//      Copyright (c) Hancock Software Solutions Limited 2024. All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Website.Client
{
    using Core.Client.Extensions;
    using Microsoft.AspNetCore.Components.Web;
    using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
    using Shared.Common;

    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            builder.Services.AddCore(opt =>
            {
                opt.Hub = new Uri(new Uri(builder.HostEnvironment.BaseAddress), "events");
                opt.Assembly = typeof(Constants).Assembly;
            });

            builder.Services.AddScoped(sp => new HttpClient {BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)});

            await builder.Build().RunAsync();
        }
    }
}