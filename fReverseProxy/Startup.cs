using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ProxyKit;

/// <summary>
/// Sample using the .net core reverse proxy ProxyKit (https://github.com/damianh/ProxyKit)
/// </summary>
namespace fReverseProxy
{
    public static class ReverseProxyExtensions {

        /// <summary>
        /// Add an tutu header on the fly
        /// </summary>
        /// <param name="forwardContext"></param>
        /// <returns></returns>
        public static ForwardContext AddTutuHeader(this ForwardContext forwardContext)
        {
            var headers = forwardContext.UpstreamRequest.Headers;
            headers.Add("tutu", "tutu-string");
            return forwardContext;
        }
    }

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddProxy();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();

            // Configure the reverse proxy pipeline
            app.RunProxy(async context =>
            {
                var forwardToUrl = "https://localhost:44343";

                var response = await context
                    .ForwardTo(forwardToUrl)
                    .AddXForwardedHeaders()
                    .AddTutuHeader() // Add the tutu header
                    .Send(); // Forward the request

                // Update the JSON response on the fly
                var content = await response.Content.ReadAsStringAsync();
                response.Content = new StringContent(content.Replace("]", @", ""added-on-the-fly"" ] "));

                // Add an new header after the request has been processed
                response.Headers.Add("HeaderFinal", "123");

                // Remove header after the request has been processed
                // response.Headers.Remove("MachineID");

                return response;
            });
        }
    }
}
