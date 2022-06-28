using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Charismatic.Authorization.Users;
using Charismatic.CrudAppServiceBase;
using Charismatic.DoctorSpecialties.Dto;
using Charismatic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charismatic.DoctorSpecialties
{
    public class DoctorSpecialtyAppService : CharismaticAsyncCrudAppService<DoctorSpecialty, DoctorSpecialtyDto, int, CharismaticBaseListInputDto, CreateDoctorSpecialtyDto, EditDoctorSpecialtyDto>, IDoctorSpecialtyAppService
    {
        private readonly IRepository<Specialty> _Specialtyrepository;
        private readonly IRepository<DoctorSpecialty> _doctorSpecialtyrepository;
        private readonly UserManager _userManager;
        public DoctorSpecialtyAppService(IRepository<Specialty> Specialtyrepository, UserManager userManager, IRepository<DoctorSpecialty> doctorSpecialtyrepository)
            : base(doctorSpecialtyrepository)
        {
            _doctorSpecialtyrepository = doctorSpecialtyrepository;
            _Specialtyrepository = Specialtyrepository;
            _userManager = userManager;
        }
        public Task<PagedResultDto<DoctorSpecialtyDto>> GetAllDoctorSpecialtiesAsync(CharismaticBaseListInputDto input)
        {
            throw new NotImplementedException();
        }
        public override async Task<DoctorSpecialtyDto> CreateAsync(CreateDoctorSpecialtyDto input)
        {
            try
            {
                var doctorSpecialty = MapToEntity(input);
                var ExistdoctorSpecialtyId = Repository.FirstOrDefaultAsync(a => a.SpecialtyId == doctorSpecialty.SpecialtyId && a.DoctorId == doctorSpecialty.DoctorId);
                if (ExistdoctorSpecialtyId.Result == null)
                {
                    await Repository.InsertAsync(doctorSpecialty);
                }
                var doctorSpecialtyDto = MapToEntityDto(doctorSpecialty);
                if (doctorSpecialtyDto.CreatorUserId.HasValue)
                    doctorSpecialtyDto.CreatorUserName = (await _userManager.GetUserByIdAsync(doctorSpecialtyDto.CreatorUserId.Value)).Name;
                //doctorCenterDto.LockedStatus = false;

                return doctorSpecialtyDto;
            }
            catch (NullReferenceException e)
            {

                throw;
            }
        }
    }
}
