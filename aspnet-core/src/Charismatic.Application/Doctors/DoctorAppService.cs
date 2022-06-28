using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization.Users;
using Abp.Domain.Repositories;
using Abp.IdentityFramework;
using Abp.Runtime.Session;
using Abp.UI;
using Charismatic.Authorization.Roles;
using Charismatic.Authorization.Users;
using Charismatic.CrudAppServiceBase;
using Charismatic.Doctors.Dto;
using Charismatic.Localization.SourceFiles;
using Charismatic.Models;
using Charismatic.Users.Dto;
using ITLand.CMMS.Libs.DevExtreme;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Charismatic.Enums;

namespace Charismatic.Doctors
{

    public class DoctorAppService : CharismaticAsyncCrudAppService<Doctor, DoctorDto, int, CharismaticBaseListInputDto, CreateDoctorDto, EditDoctorDto>, IDoctorAppService

    {
        private readonly IRepository<Doctor> _doctorRepository;
        private readonly UserManager _userManager;

        public DoctorAppService(IRepository<Doctor> Repository, UserManager userManager) : base(Repository)
        {
            _doctorRepository = Repository;
            _userManager = userManager;

        }

        /// <summary>
        /// filtering list params
        /// </summary>
        /// <param name="input">search-filter</param>
        /// <returns></returns>
        /// 
        protected override IQueryable<Doctor> CreateFilteredQuery(CharismaticBaseListInputDto input)
        {
            var data = base.CreateFilteredQuery(input);
            //data = data.WhereIf(input.ReasonRelatedTo.HasValue, i => i.RelatedTo == input.ReasonRelatedTo.Value);
            if (input.HasFilter)
            {
                data = new DataSourceLoaderImpl<Doctor>(data, input, default, true).LoadAsync().Result;
            }
            return data;
        }
        public async Task<PagedResultDto<DoctorListDto>> GetAllDoctorsAsync(CharismaticBaseListInputDto input)
        {
            try
            {
                var data = CreateFilteredQuery(input);
                var totalCount = await AsyncQueryableExecuter.CountAsync(data);
                data = ApplySorting(data, input);
                data = ApplyPaging(data, input);
                var list = await AsyncQueryableExecuter.ToListAsync(data);
                var listDto = ObjectMapper.Map<List<DoctorListDto>>(list);

                foreach (var item in listDto)
                {

                    item.EmailAddress = _userManager.Users.SingleOrDefault(a => a.Id == item.UserId).EmailAddress;
                    item.Surname = _userManager.Users.SingleOrDefault(a => a.Id == item.UserId).Surname;
                    item.Name = _userManager.Users.SingleOrDefault(a => a.Id == item.UserId).Name;
                    item.UserName = _userManager.Users.SingleOrDefault(a => a.Id == item.UserId).UserName;
                    item.IsActive = _userManager.Users.SingleOrDefault(a => a.Id == item.UserId).IsActive;
                    //item.StateName = Enum.GetName(typeof(Status), item.DoctorStatus);
                    var user = await _userManager.FindByIdAsync(AbpSession.GetUserId().ToString());
                    item.CreatorUserName = (await _userManager.GetUserByIdAsync(user.Id)).UserName;
                }
                return new PagedResultDto<DoctorListDto>(totalCount, listDto);
            }
            catch (Exception)
            {

                throw;
            }

        }
        protected virtual void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
        public override async Task<DoctorDto> CreateAsync(CreateDoctorDto input)
        {
            try
            {
                if (input.UserId == 0)
                {
                    CheckCreatePermission();
                    var user = ObjectMapper.Map<User>(input);
                    user.TenantId = AbpSession.TenantId;
                    user.IsEmailConfirmed = true;

                    await _userManager.InitializeOptionsAsync(AbpSession.TenantId);

                    CheckErrors(await _userManager.CreateAsync(user, input.Password));
                    var doctor = MapToEntity(input);
                    var existDoctor =await  _doctorRepository.FirstOrDefaultAsync(a => a.UserId == user.Id);
                    if (existDoctor == null)
                    {
                        doctor.UserId = user.Id;
                        if (user.IsActive)
                        {
                            doctor.DoctorStatus = Status.Accepted;
                        }
                        doctor.Id = await Repository.InsertAndGetIdAsync(doctor);

                        //Add doctor Role to this Doctor
                        var doctorRole = _userManager._roleRepository.FirstOrDefault(r => r.TenantId == user.TenantId && r.Name == StaticRoleNames.Tenants.Doctor);
                     await _userManager._userRoleRepository.InsertAsync( new UserRole(user.TenantId, user.Id, doctorRole.Id));
                    }
                    var doctorDto = MapToEntityDto(doctor);
                  //  CurrentUnitOfWork.SaveChanges();
                    return doctorDto;
                }
                else
                {
                    var doctor = MapToEntity(input);
                    var existDoctor =await  _doctorRepository.FirstOrDefaultAsync(a => a.UserId == doctor.UserId);
                    if (existDoctor==null)
                    {
                        doctor.Id = await Repository.InsertAndGetIdAsync(doctor);
                    }
                    var doctorDto = MapToEntityDto(doctor);
                    return doctorDto;
                }

            }
            catch (NullReferenceException)
            {
                throw;
            }
        }
        public async override Task<DoctorDto> UpdateAsync(EditDoctorDto input)
        {

            var doctor = Repository.GetAsync(input.Id).Result;
            if (doctor == null)
                throw new UserFriendlyException(string.Format((Exceptions.ObjectWasNotFound), input.Id));

            var user = _userManager.Users.FirstOrDefault(a => a.Id == doctor.UserId);
            // user.IsActive = input.IsActive;
            user.Name = input.Name;
            user.UserName = input.UserName;
            user.Surname = input.Surname;
            user.EmailAddress = input.EmailAddress;
            user.PhoneNumber = input.PhoneNumber;
            //user.Password = new PasswordHasher<User>(new OptionsWrapper<PasswordHasherOptions>(new PasswordHasherOptions())).HashPassword(user, input.Password);
            user.LastModifierUserId = AbpSession.UserId;
            CheckErrors(await _userManager.UpdateAsync(user));
            //if (user.IsActive)
            //{
            //    input.DoctorStatus = Status.Accepted;
            //}
            //else
            //{
            //    input.DoctorStatus = Status.Suspended;
            //}
            CurrentUnitOfWork.SaveChanges();
            return await base.UpdateAsync(input);

        }
        public async Task ActivateDeactivate(EntityDto<int> input)
        {
            var doctor = await Repository.GetAsync(input.Id);
            if (doctor == null)
                throw new UserFriendlyException(string.Format((Exceptions.ObjectWasNotFound), input.Id));
            var user = _userManager.Users.FirstOrDefault(a => a.Id == doctor.UserId);
            user.IsActive = !user.IsActive;
            CheckErrors(await _userManager.UpdateAsync(user));
            if (user.IsActive)
            {
                doctor.DoctorStatus = Status.Accepted;
            }
            else
            {
                doctor.DoctorStatus = Status.Suspended;
            }
            CurrentUnitOfWork.SaveChanges();
            await Repository.UpdateAsync(doctor);

        }
        public override async Task<DoctorDto> GetAsync(EntityDto<int> input)
        {
            var doctor = await Repository.FirstOrDefaultAsync(input.Id);
            if (doctor == null)
                throw new UserFriendlyException(string.Format((Exceptions.ObjectWasNotFound), Tokens.Doctor));

            var doctorDto = ObjectMapper.Map<DoctorDto>(doctor);
            var user = await _userManager.Users.FirstOrDefaultAsync(a => a.Id == doctor.UserId);
            if (user == null)
                throw new UserFriendlyException(string.Format((Exceptions.ObjectWasNotFound), Tokens.Users));
            doctorDto.UserName = user.UserName;
            doctorDto.Name = user.Name;
            doctorDto.Surname = user.Surname;
            doctorDto.IsActive = user.IsActive;
            doctorDto.EmailAddress = user.EmailAddress;
            doctorDto.CreatorUserName = (await _userManager.GetUserByIdAsync(doctorDto.CreatorUserId.Value)).Name;

            return doctorDto;
        }
        public override async Task DeleteAsync(EntityDto<int> input)
        {
            var doctor = await Repository.FirstOrDefaultAsync(input.Id);
            if (doctor == null)
                throw new UserFriendlyException(string.Format((Exceptions.ObjectWasNotFound), Tokens.Doctor));
            await Repository.DeleteAsync(doctor);
            var user = await _userManager.Users.FirstOrDefaultAsync(a => a.Id == doctor.UserId);
            if (user == null)
                throw new UserFriendlyException(string.Format((Exceptions.ObjectWasNotFound), Tokens.Users));
            await _userManager.DeleteAsync(user);
            MapToEntityDto(doctor);

        }

    }
}
