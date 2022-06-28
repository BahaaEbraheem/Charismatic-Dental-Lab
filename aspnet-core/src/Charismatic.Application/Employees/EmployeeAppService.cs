using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.IdentityFramework;
using Abp.Runtime.Session;
using Abp.UI;
using Charismatic.Authorization.Users;
using Charismatic.CrudAppServiceBase;
using Charismatic.Employees.Dto;
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

namespace Charismatic.Employees
{
   public class EmployeeAppService : CharismaticAsyncCrudAppService<Employee, EmployeeDto, int, CharismaticBaseListInputDto, CreateEmployeeDto, EditEmployeeDto>, IEmployeeAppService
    {
        private readonly IRepository<Employee> _Repository;
        private readonly UserManager _userManager;

        public EmployeeAppService(IRepository<Employee> Repository, UserManager userManager) : base(Repository)
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
        protected override IQueryable<Employee> CreateFilteredQuery(CharismaticBaseListInputDto input)
        {
            var data = base.CreateFilteredQuery(input);
            //data = data.WhereIf(input.ReasonRelatedTo.HasValue, i => i.RelatedTo == input.ReasonRelatedTo.Value);
            if (input.HasFilter)
            {
                data = new DataSourceLoaderImpl<Employee>(data, input, default, true).LoadAsync().Result;
            }
            return data;
        }
        public async Task<PagedResultDto<EmployeeListDto>> GetAllEmployeesAsync(CharismaticBaseListInputDto input)
        {
            try
            {
                var data = CreateFilteredQuery(input);
                var totalCount = await AsyncQueryableExecuter.CountAsync(data);
                data = ApplySorting(data, input);
                data = ApplyPaging(data, input);
                var list = await AsyncQueryableExecuter.ToListAsync(data);
                var listDto = ObjectMapper.Map<List<EmployeeListDto>>(list);
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
                return new PagedResultDto<EmployeeListDto>(totalCount, listDto);
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
        public override async Task<EmployeeDto> CreateAsync(CreateEmployeeDto input)
        {
            try
            {
                    var user = ObjectMapper.Map<User>(input);
                    user.TenantId = AbpSession.TenantId;
                    user.IsEmailConfirmed = true;
                    await _userManager.InitializeOptionsAsync(AbpSession.TenantId);
                    CheckErrors(await _userManager.CreateAsync(user, input.Password));
                    var Employee = MapToEntity(input);
                    var existEmployee = await _Repository.FirstOrDefaultAsync(a => a.UserId == user.Id);
                    if (existEmployee == null)
                    {
                        Employee.UserId = user.Id;
                        Employee.Id = await Repository.InsertAndGetIdAsync(Employee);
                    }
                    else
                    {
                        throw new UserFriendlyException(string.Format((Exceptions.ObjectAlreadyExisted), input));
                    }
                    var EmployeeDto = MapToEntityDto(Employee);
                    return EmployeeDto;
            }
            catch (NullReferenceException e)
            {
                throw;
            }
        }
        public async override Task<EmployeeDto> UpdateAsync(EditEmployeeDto input)
        {

            var Employee = Repository.GetAsync(input.Id).Result;
            if (Employee == null)
                throw new UserFriendlyException(string.Format((Exceptions.ObjectWasNotFound), input.Id));

            var user = _userManager.Users.FirstOrDefault(a => a.Id == Employee.UserId);
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
            var Employee = await Repository.GetAsync(input.Id);
            if (Employee == null)
                throw new UserFriendlyException(string.Format((Exceptions.ObjectWasNotFound), input.Id));
            var user = _userManager.Users.FirstOrDefault(a => a.Id == Employee.UserId);
            user.IsActive = !user.IsActive;
            CheckErrors(await _userManager.UpdateAsync(user));
            CurrentUnitOfWork.SaveChanges();
            await Repository.UpdateAsync(Employee);

        }

        public override async Task<EmployeeDto> GetAsync(EntityDto<int> input)
        {
            var Employee = await Repository.FirstOrDefaultAsync(input.Id);
            if (Employee == null)
                throw new UserFriendlyException(string.Format((Exceptions.ObjectWasNotFound), Tokens.Employee));

            var EmployeeDto = ObjectMapper.Map<EmployeeDto>(Employee);
            var user = await _userManager.Users.FirstOrDefaultAsync(a => a.Id == Employee.UserId);
            if (user == null)
                throw new UserFriendlyException(string.Format((Exceptions.ObjectWasNotFound), Tokens.Users));
            EmployeeDto.UserName = user.UserName;
            EmployeeDto.Name = user.Name;
            EmployeeDto.Surname = user.Surname;
            EmployeeDto.IsActive = user.IsActive;
            EmployeeDto.EmailAddress = user.EmailAddress;
            EmployeeDto.CreatorUserName = (await _userManager.GetUserByIdAsync(EmployeeDto.CreatorUserId.Value)).Name;

            return EmployeeDto;
        }
        public override async Task DeleteAsync(EntityDto<int> input)
        {
            var Employee = await Repository.FirstOrDefaultAsync(input.Id);
            if (Employee == null)
                throw new UserFriendlyException(string.Format((Exceptions.ObjectWasNotFound), Tokens.Employee));
            await Repository.DeleteAsync(Employee);
            var user = await _userManager.Users.FirstOrDefaultAsync(a => a.Id == Employee.UserId);
            if (user == null)
                throw new UserFriendlyException(string.Format((Exceptions.ObjectWasNotFound), Tokens.Users));
            await _userManager.DeleteAsync(user);
            MapToEntityDto(Employee);

        }

    }
}
