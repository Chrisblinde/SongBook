using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SongBook.WebMVC.Startup))]
namespace SongBook.WebMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
