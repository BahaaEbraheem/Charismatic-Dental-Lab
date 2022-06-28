using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Charismatic.Authorization.Users;
using Charismatic.CrudAppServiceBase;
using Charismatic.DoctorCenters.Dto;
using Charismatic.DoctorSpecialties.Dto;
using Charismatic.Models;
using ITLand.CMMS.Libs.DevExtreme;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charismatic.DoctorCenters
{
    public class DoctorCentersAppService : CharismaticAsyncCrudAppService<DoctorCenter, DoctorCenterDto, int, CharismaticBaseListInputDto, CreateDoctorCenterDto, EditDoctorCenterDto>, IDoctorCentersAppService
    {
        private readonly IRepository<DoctorCenter> _doctorCenterrepository;
        private readonly UserManager _userManager;
        public DoctorCentersAppService(IRepository<DoctorCenter> doctorCenterrepository, UserManager userManager)
            : base(doctorCenterrepository)
        {
            _doctorCenterrepository = doctorCenterrepository;
            _userManager = userManager;
        }
        protected override IQueryable<DoctorCenter> CreateFilteredQuery(CharismaticBaseListInputDto input)
        {
            var data = base.CreateFilteredQuery(input);
            //data = data.WhereIf(input.ReasonRelatedTo.HasValue, i => i.RelatedTo == input.ReasonRelatedTo.Value);

            if (input.HasFilter)
            {
                data = new DataSourceLoaderImpl<DoctorCenter>(data, input, default, true).LoadAsync().Result;
            }

            return data;
        }
        public  async Task<PagedResultDto<DoctorCenterDto>> GetAllDoctorCentersAsync(CharismaticBaseListInputDto input)
        {
            try
            {
                var data = CreateFilteredQuery(input);
                var totalCount = await AsyncQueryableExecuter.CountAsync(data);
                data = ApplySorting(data, input);
                data = ApplyPaging(data, input);
                var list = await AsyncQueryableExecuter.ToListAsync(data);
                var listDto = ObjectMapper.Map<List<DoctorCenterDto>>(list);
                return new PagedResultDto<DoctorCenterDto>(totalCount, listDto);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public override async Task<DoctorCenterDto> CreateAsync(CreateDoctorCenterDto input)
        {
            try
            {
                var doctorCenter = MapToEntity(input);
                var ExistdoctorCenterId = Repository.FirstOrDefaultAsync(a => a.CenterId == doctorCenter.CenterId && a.DoctorId == doctorCenter.DoctorId).Result;
                if (ExistdoctorCenterId == null)
                {
                    await Repository.InsertAsync(doctorCenter);
                }
                var doctorCenterDto = MapToEntityDto(doctorCenter);
                if (doctorCenterDto.CreatorUserId.HasValue)
                    doctorCenterDto.CreatorUserName = (await _userManager.GetUserByIdAsync(doctorCenterDto.CreatorUserId.Value)).Name;
                //doctorCenterDto.LockedStatus = false;

                return doctorCenterDto;
            }
            catch (NullReferenceException e)
            {

                throw;
            }
           
                
                
            
        }

    }
}
