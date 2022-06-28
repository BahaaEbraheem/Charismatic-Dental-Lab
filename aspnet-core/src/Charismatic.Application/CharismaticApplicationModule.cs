using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Charismatic.Authorization;
using Charismatic.Cases.Dto;
using Charismatic.Helpers;
using Charismatic.Models;
using static Charismatic.Enums;

namespace Charismatic
{
    [DependsOn(
        typeof(CharismaticCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class CharismaticApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<CharismaticAuthorizationProvider>();
            Configuration.Modules.AbpAutoMapper().Configurators.Add(config =>
            {
                config.CreateMap<Case, CaseListDto>()
                      .ForMember(u => u.DoctorName, options => options.MapFrom(input => input.Doctor.ResponsipleName))
                      .ForMember(u => u.PatientReferraisName, options => options.MapFrom(input => input.PatientReferrais.FirstName + " " + input.PatientReferrais.LastName))
                      .ForMember(u => u.CaseNumber, options => options.MapFrom(input => input.Name))
                      .ForMember(u => u.CaseEvaluationType, options => options.MapFrom(input => input.CaseEvaluationType != null ? EnumHelper<CaseEvaluationType>.GetDisplayValue((CaseEvaluationType)input.CaseEvaluationType) : "Not defined"))
                      .ForMember(u => u.CaseStatus, options => options.MapFrom(input => EnumHelper<CaseStatus>.GetDisplayValue(input.CaseStatus)))
                      .ForMember(u => u.Type, options => options.MapFrom(input => EnumHelper<Charismatic.Enums.CaseType>.GetDisplayValue(input.Type)));
            });
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(CharismaticApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddMaps(thisAssembly)
            );
        }
    }
}
