using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FSL.DatabaseImageBankInMvc.Startup))]
namespace FSL.DatabaseImageBankInMvc
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
