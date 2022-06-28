using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.IdentityFramework;
using Abp.Runtime.Session;
using Abp.UI;
using Charismatic.Authorization.Users;
using Charismatic.CrudAppServiceBase;
using Charismatic.Admins.Dto;
using Charismatic.Localization.SourceFiles;
using Charismatic.Models;
using ITLand.CMMS.Libs.DevExtreme;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charismatic.Admins
{
   public class AdminAppService : CharismaticAsyncCrudAppService<Admin, AdminDto, int, CharismaticBaseListInputDto, CreateAdminDto, EditAdminDto>, IAdminAppService
    {
        private readonly IRepository<Admin> _Repository;
        private readonly UserManager _userManager;

        public AdminAppService(IRepository<Admin> Repository, UserManager userManager) : base(Repository)
        {
            _Repository = Repository;
            _userManager = userManager;

        }

        /// <summary>
        /// filtering list params
        /// </summary>
        /// <param name="input">search-filter</param>
        /// <returns></returns>
        /// 
        protected override IQueryable<Admin> CreateFilteredQuery(CharismaticBaseListInputDto input)
        {
            var data = base.CreateFilteredQuery(input);
            //data = data.WhereIf(input.ReasonRelatedTo.HasValue, i => i.RelatedTo == input.ReasonRelatedTo.Value);
            if (input.HasFilter)
            {
                data = new DataSourceLoaderImpl<Admin>(data, input, default, true).LoadAsync().Result;
            }
            return data;
        }
        public async Task<PagedResultDto<AdminListDto>> GetAllAdminsAsync(CharismaticBaseListInputDto input)
        {
            try
            {
                var data = CreateFilteredQuery(input);
                var totalCount = await AsyncQueryableExecuter.CountAsync(data);
                data = ApplySorting(data, input);
                data = ApplyPaging(data, input);
                var list = await AsyncQueryableExecuter.ToListAsync(data);
                var listDto = ObjectMapper.Map<List<AdminListDto>>(list);
                foreach (var item in listDto)
                {
                    if (_userManager.Users.SingleOrDefault(a => a.Id == item.UserId)!=null)
                    {
                        item.EmailAddress = _userManager.Users.SingleOrDefault(a => a.Id == item.UserId).EmailAddress;
                        item.PhoneNumber = _userManager.Users.SingleOrDefault(a => a.Id == item.UserId).PhoneNumber;
                        item.Surname = _userManager.Users.SingleOrDefault(a => a.Id == item.UserId).Surname;
                        item.Name = _userManager.Users.SingleOrDefault(a => a.Id == item.UserId).Name;
                        item.UserName = _userManager.Users.SingleOrDefault(a => a.Id == item.UserId).UserName;
                        item.IsActive = _userManager.Users.SingleOrDefault(a => a.Id == item.UserId).IsActive;
                    }
                    var user = await _userManager.FindByIdAsync(AbpSession.GetUserId().ToString());
                    item.CreatorUserName = (await _userManager.GetUserByIdAsync(user.Id)).UserName;
                }
                return new PagedResultDto<AdminListDto>(totalCount, listDto);
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
        public override async Task<AdminDto> CreateAsync(CreateAdminDto input)
        {
            try
            {
                    var user = ObjectMapper.Map<User>(input);
                    user.TenantId = AbpSession.TenantId;
                    user.IsEmailConfirmed = true;
                    await _userManager.InitializeOptionsAsync(AbpSession.TenantId);
                    CheckErrors(await _userManager.CreateAsync(user, input.Password));
                    var Admin = MapToEntity(input);
                    var existAdmin = await _Repository.FirstOrDefaultAsync(a => a.UserId == user.Id);
                    if (existAdmin == null)
                    {
                        Admin.UserId = user.Id;
                        Admin.Id = await Repository.InsertAndGetIdAsync(Admin);
                    }
                    else
                    {
                        throw new UserFriendlyException(string.Format((Exceptions.ObjectAlreadyExisted), input));
                    }
                    var AdminDto = MapToEntityDto(Admin);
                    return AdminDto;
            }
            catch (NullReferenceException e)
            {
                throw;
            }
        }
        public async override Task<AdminDto> UpdateAsync(EditAdminDto input)
        {

            var Admin = Repository.GetAsync(input.Id).Result;
            if (Admin == null)
                throw new UserFriendlyException(string.Format((Exceptions.ObjectWasNotFound), input.Id));

            var user = _userManager.Users.FirstOrDefault(a => a.Id == Admin.UserId);
            // user.IsActive = input.IsActive;
            user.Name = input.Name;
            user.UserName = input.UserName;
            user.Surname = input.Surname;
            user.EmailAddress = input.EmailAddress;
            user.PhoneNumber = input.PhoneNumber;
            //user.Password = new PasswordHasher<User>(new OptionsWrapper<PasswordHasherOptions>(new PasswordHasherOptions())).HashPassword(user, input.Password);
            user.LastModifierUserId = AbpSession.UserId;
            CheckErrors(await _userManager.UpdateAsync(user));

            CurrentUnitOfWork.SaveChanges();
            return await base.UpdateAsync(input);

        }
        public async Task ActivateDeactivate(EntityDto<int> input)
        {
            var Admin = await Repository.GetAsync(input.Id);
            if (Admin == null)
                throw new UserFriendlyException(string.Format((Exceptions.ObjectWasNotFound), input.Id));
            var user = _userManager.Users.FirstOrDefault(a => a.Id == Admin.UserId);
            user.IsActive = !user.IsActive;
            CheckErrors(await _userManager.UpdateAsync(user));
            CurrentUnitOfWork.SaveChanges();
            await Repository.UpdateAsync(Admin);

        }

        public override async Task<AdminDto> GetAsync(EntityDto<int> input)
        {
            var Admin = await Repository.FirstOrDefaultAsync(input.Id);
            if (Admin == null)
                throw new UserFriendlyException(string.Format((Exceptions.ObjectWasNotFound), Tokens.Admin));

            var AdminDto = ObjectMapper.Map<AdminDto>(Admin);
            var user = await _userManager.Users.FirstOrDefaultAsync(a => a.Id == Admin.UserId);
            if (user == null)
                throw new UserFriendlyException(string.Format((Exceptions.ObjectWasNotFound), Tokens.Users));
            AdminDto.UserName = user.UserName;
            AdminDto.Name = user.Name;
            AdminDto.Surname = user.Surname;
            AdminDto.IsActive = user.IsActive;
            AdminDto.EmailAddress = user.EmailAddress;
            AdminDto.CreatorUserName = (await _userManager.GetUserByIdAsync(AdminDto.CreatorUserId.Value)).Name;

            return AdminDto;
        }
        public override async Task DeleteAsync(EntityDto<int> input)
        {
            var Admin = await Repository.FirstOrDefaultAsync(input.Id);
            if (Admin == null)
                throw new UserFriendlyException(string.Format((Exceptions.ObjectWasNotFound), Tokens.Admin));
            await Repository.DeleteAsync(Admin);
            var user = await _userManager.Users.FirstOrDefaultAsync(a => a.Id == Admin.UserId);
            if (user == null)
                throw new UserFriendlyException(string.Format((Exceptions.ObjectWasNotFound), Tokens.Users));
            await _userManager.DeleteAsync(user);
            MapToEntityDto(Admin);

        }

    }
}
