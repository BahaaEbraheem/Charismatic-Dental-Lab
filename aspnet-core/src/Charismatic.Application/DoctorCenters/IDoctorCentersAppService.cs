using Abp.Application.Services.Dto;
using Charismatic.CrudAppServiceBase;
using Charismatic.DoctorCenters.Dto;
using Charismatic.DoctorSpecialties.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charismatic.DoctorCenters
{
  public  interface IDoctorCentersAppService : ICharismaticAsyncCrudAppService<DoctorCenterDto, int, CharismaticBaseListInputDto, CreateDoctorCenterDto, EditDoctorCenterDto>
    {                                                                             
          Task<PagedResultDto<DoctorCenterDto>> GetAllDoctorCentersAsync(CharismaticBaseListInputDto input);
    }
}
