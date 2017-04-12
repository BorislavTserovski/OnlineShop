using OnlineShop.Migrations;
using OnlineShop.Models;
using Microsoft.Owin;
using Owin;
using System.Data.Entity;

[assembly: OwinStartupAttribute(typeof(OnlineShop.Startup))]
namespace OnlineShop
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            Database.SetInitializer(
                new MigrateDatabaseToLatestVersion<ShopDbContext, Configuration>());
            ConfigureAuth(app);
        }
    }
}
