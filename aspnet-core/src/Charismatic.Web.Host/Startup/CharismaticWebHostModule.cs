using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Charismatic.Configuration;

namespace Charismatic.Web.Host.Startup
{
    [DependsOn(
       typeof(CharismaticWebCoreModule))]
    public class CharismaticWebHostModule: AbpModule
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public CharismaticWebHostModule(IWebHostEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(CharismaticWebHostModule).GetAssembly());
        }
    }
}
