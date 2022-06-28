using Abp.AspNetCore;
using Abp.AspNetCore.TestBase;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Charismatic.EntityFrameworkCore;
using Charismatic.Web.Startup;
using Microsoft.AspNetCore.Mvc.ApplicationParts;

namespace Charismatic.Web.Tests
{
    [DependsOn(
        typeof(CharismaticWebMvcModule),
        typeof(AbpAspNetCoreTestBaseModule)
    )]
    public class CharismaticWebTestModule : AbpModule
    {
        public CharismaticWebTestModule(CharismaticEntityFrameworkModule abpProjectNameEntityFrameworkModule)
        {
            abpProjectNameEntityFrameworkModule.SkipDbContextRegistration = true;
        } 
        
        public override void PreInitialize()
        {
            Configuration.UnitOfWork.IsTransactional = false; //EF Core InMemory DB does not support transactions.
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(CharismaticWebTestModule).GetAssembly());
        }
        
        public override void PostInitialize()
        {
            IocManager.Resolve<ApplicationPartManager>()
                .AddApplicationPartsIfNotAddedBefore(typeof(CharismaticWebMvcModule).Assembly);
        }
    }
}