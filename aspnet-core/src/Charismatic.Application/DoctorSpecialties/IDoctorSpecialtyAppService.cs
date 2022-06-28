using Abp.Application.Services.Dto;
using Charismatic.CrudAppServiceBase;
using Charismatic.DoctorSpecialties.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charismatic.DoctorSpecialties
{
  public  interface IDoctorSpecialtyAppService: ICharismaticAsyncCrudAppService<DoctorSpecialtyDto, int, CharismaticBaseListInputDto, CreateDoctorSpecialtyDto, EditDoctorSpecialtyDto>
    {
        Task<PagedResultDto<DoctorSpecialtyDto>> GetAllDoctorSpecialtiesAsync(CharismaticBaseListInputDto input);

    }
}
