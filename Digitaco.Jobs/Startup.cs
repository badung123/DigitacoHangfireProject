using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hangfire;
using Hangfire.Annotations;
using Hangfire.Dashboard;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Digitaco.Jobs
{
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
            services.AddMvc();
            services.AddHangfire(config =>
                config.UseSqlServerStorage(Configuration.GetConnectionString("DefaultConnection")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
    //        var hangFireAuth = new DashboardOptions
    //        {
    //            Authorization = new[]
    //{
    //    new HangFireAuthorization(app.ApplicationServices.GetService<iauthorizationservice>(),
    //        app.ApplicationServices.GetService<Ihttpcontextaccessor>())
    //}
    //        };

            app.UseMvc();          
            app.UseHangfireServer();
            app.UseHangfireDashboard("/hangfire");
            TestScheduleJobs();
        }

        private void ConfigureHangfire(IApplicationBuilder app)
        {
           
        }

        private void TestScheduleJobs()
        {
            RecurringJob.AddOrUpdate(() => Console.WriteLine("Minutely Job executed"), Cron.Minutely);
        }
    }
}
