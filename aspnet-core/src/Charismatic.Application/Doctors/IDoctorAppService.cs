using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Charismatic.CrudAppServiceBase;
using Charismatic.Doctors.Dto;
using Charismatic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charismatic.Doctors
{
    public interface IDoctorAppService : ICharismaticAsyncCrudAppService<DoctorDto, int, CharismaticBaseListInputDto, CreateDoctorDto, EditDoctorDto>
    {
        //    Task CreateDoctor(CreateDoctorDto input);
        Task<PagedResultDto<DoctorListDto>> GetAllDoctorsAsync(CharismaticBaseListInputDto input);
        //Task<PagedResultDto<DoctorListDto>> GetAllAsync(CharismaticBaseListInputDto input);

    }
}
