using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Charismatic.Centers.Dto;
using Charismatic.CrudAppServiceBase;
using Charismatic.Doctors.Dto;
using Charismatic.Specialties.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charismatic.Specialties
{
    public interface ISpecialtyAppService : ICharismaticAsyncCrudAppService<SpecialtyDto, int, CharismaticBaseListInputDto, CreateSpecialtyDto, EditSpecialtyDto>
    {
        Task<PagedResultDto<SpecialtyListDto>> GetAllSpecialtiesAsync(CharismaticBaseListInputDto input);
        Task<List<SpecialitySelecltList>> GetAllList();

        //void CreateDoctorSpecialty(int Specialtyid , int doctorId);
        //System.Threading.Tasks.Task CreateSpecialty(string SpecialtyName);
        //Task<ListResultDto<SpecialtyDto>> GetAll();
        //System.Threading.Tasks.Task Create(List<int> input, CreateDoctorDto inputdoctor);

    }
}
