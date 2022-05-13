using Hangfire;
using Microsoft.Owin;
using Owin;
using System;
using System.Threading;
using System.Threading.Tasks;

[assembly: OwinStartup(typeof(QuanLiSoTietKiem.Startup))]

namespace QuanLiSoTietKiem
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            GlobalConfiguration.Configuration.UseSqlServerStorage("Server=localhost,1434;Database=manage_saving_dev;User Id=sa;Password=Vovanhoangtuan1;");

            app.UseHangfireDashboard();
            app.UseHangfireServer();
        }

    }
}
