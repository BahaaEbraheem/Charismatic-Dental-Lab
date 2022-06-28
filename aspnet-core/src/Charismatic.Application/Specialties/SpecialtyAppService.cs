using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.UI;
using Charismatic.Authorization.Users;
using Charismatic.Centers.Dto;
using Charismatic.CrudAppServiceBase;
using Charismatic.Doctors.Dto;
using Charismatic.DoctorSpecialties.Dto;
using Charismatic.Domain.Centers;
using Charismatic.Localization.SourceFiles;
using Charismatic.Models;
using Charismatic.Roles.Dto;
using Charismatic.Specialties.Dto;
using ITLand.CMMS.Libs.DevExtreme;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charismatic.Specialties
{
    public class SpecialtyAppService : CharismaticAsyncCrudAppService<Specialty, SpecialtyDto, int, CharismaticBaseListInputDto, CreateSpecialtyDto, EditSpecialtyDto>, ISpecialtyAppService
    {
        private readonly IRepository<Specialty> _Specialtyrepository;
        private readonly IRepository<DoctorSpecialty> _doctorSpecialtyrepository;
        private readonly UserManager _userManager;
        public SpecialtyAppService(IRepository<Specialty> Specialtyrepository, UserManager userManager, IRepository<DoctorSpecialty> doctorSpecialtyrepository)
            : base(Specialtyrepository)
        {
            _doctorSpecialtyrepository = doctorSpecialtyrepository;
            _Specialtyrepository = Specialtyrepository;
            _userManager = userManager;
        }
        protected IQueryable<Specialty> SpecialtyFilteredQuery(CharismaticBaseListInputDto input)
        {
            var data = base.CreateFilteredQuery(input);
            //data = data.WhereIf(input.ReasonRelatedTo.HasValue, i => i.RelatedTo == input.ReasonRelatedTo.Value);

            if (input.HasFilter)
            {
                data = new DataSourceLoaderImpl<Specialty>(data, input, default, true).LoadAsync().Result;
            }

            return data;
        }
        public async Task<PagedResultDto<SpecialtyListDto>> GetAllSpecialtiesAsync(CharismaticBaseListInputDto input)
        {
            var data = CreateFilteredQuery(input);
            var totalCount = await AsyncQueryableExecuter.CountAsync(data);
            data = ApplySorting(data, input);
            data = ApplyPaging(data, input);
            var list = await AsyncQueryableExecuter.ToListAsync(data);
            var listDto = ObjectMapper.Map<List<SpecialtyListDto>>(list);
            foreach (var item in listDto)
            {
                if (item.CreatorUserId.HasValue)
                    item.CreatorUserName = (await _userManager.GetUserByIdAsync(item.CreatorUserId.Value)).UserName;
            }
            return new PagedResultDto<SpecialtyListDto>(totalCount, listDto);

        }
        public override async Task<SpecialtyDto> CreateAsync(CreateSpecialtyDto input)
        {
           
            try
            {
                var specialty = MapToEntity(input);
                var Existspecialty = Repository.GetAll().FirstOrDefaultAsync(cen => cen.Name == specialty.Name);
                if (Existspecialty.Result == null)
                {
                    specialty.Id = await Repository.InsertAndGetIdAsync(specialty);
                }
                var specialtyDto = MapToEntityDto(specialty);
                //if (centerDto.CreatorUserId.HasValue)
                //    centerDto.CreatorUserName = (await _userManager.GetUserByIdAsync(centerDto.CreatorUserId.Value)).Name;
                //centerDto.LockedStatus = false;
                return specialtyDto;

            }
            catch (NullReferenceException e)
            {

                throw;
            }
        }


        public override Task<SpecialtyDto> UpdateAsync(EditSpecialtyDto input)
        {
            return base.UpdateAsync(input);

        }
        public override async Task<SpecialtyDto> GetAsync(EntityDto<int> input)
        {
            var specialty = await Repository.FirstOrDefaultAsync(input.Id);
            if (specialty == null)
                throw new UserFriendlyException(string.Format((Exceptions.ObjectWasNotFound), Tokens.Specialty));

            var specialtyDto = ObjectMapper.Map<SpecialtyDto>(specialty);
            if (specialtyDto.CreatorUserId.HasValue)
                specialtyDto.CreatorUserName = (await _userManager.GetUserByIdAsync(specialtyDto.CreatorUserId.Value)).Name;

            return specialtyDto;
            
        }

        public override async Task DeleteAsync(EntityDto<int> input)
        {

            var specialty = await Repository.FirstOrDefaultAsync(input.Id);
            if (specialty == null)
                throw new UserFriendlyException(string.Format((Exceptions.ObjectWasNotFound), Tokens.Specialty));

            await Repository.DeleteAsync(specialty);

            MapToEntityDto(specialty);

        }


        public async Task<List<SpecialitySelecltList>> GetAllList()
        {
            var specialties = await _Specialtyrepository.GetAllListAsync();
            return ObjectMapper.Map<List<SpecialitySelecltList>>(specialties);
        }

        //public void CreateDoctorSpecialty(int Specialtyid, int doctorId)
        //{
        //    CreateDoctorSpecialtyDto doctorSpecialty = new CreateDoctorSpecialtyDto()
        //    {
        //        DoctorId=doctorId,
        //        SpecialtyId=Specialtyid,
        //    };
        //    var doctorDto = MapToEntityDto(doctorSpecialty);
        //    _doctorSpecialtyrepository.Insert(ObjectMapper.Map<DoctorSpecialty>(doctorSpecialty));
            
        //}


    }
}
