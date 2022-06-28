using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Runtime.Session;
using Abp.UI;
using Charismatic.Authorization.Users;
using Charismatic.CrudAppServiceBase;
using Charismatic.Departments.Dto;
using Charismatic.Localization.SourceFiles;
using Charismatic.Models;
using ITLand.CMMS.Libs.DevExtreme;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charismatic.Departments
{
    public class DepartmentAppService : CharismaticAsyncCrudAppService<Department, DepartmentDto, int, CharismaticBaseListInputDto, CreateDepartmentDto, EditDepartmentDto>, IDepartmentAppService
    {
        private readonly IRepository<Department> _Repository;
        private readonly UserManager _userManager;

        public DepartmentAppService(IRepository<Department> Repository, UserManager userManager) : base(Repository)
        {
            _Repository = Repository;
            _userManager = userManager;

        }
        /// <summary>
        /// filtering list params
        /// </summary>
        /// <param name="input">search-filter</param>
        /// <returns>IQueryable of Product</returns>
        protected override IQueryable<Department> CreateFilteredQuery(CharismaticBaseListInputDto input)
        {
            var data = base.CreateFilteredQuery(input);
            if (input.HasFilter)
            {
                data = new DataSourceLoaderImpl<Department>(data, input, default, true).LoadAsync().Result;
            }
            return data;
        }

        public async Task<PagedResultDto<DepartmentListDto>> GetAllDepartmentsAsync(CharismaticBaseListInputDto input)
        {
            try
            {
                var data = CreateFilteredQuery(input);
                var totalCount = await AsyncQueryableExecuter.CountAsync(data);
                data = ApplySorting(data, input);
                data = ApplyPaging(data, input);
                var list = await AsyncQueryableExecuter.ToListAsync(data);
                var listDto = ObjectMapper.Map<List<DepartmentListDto>>(list);
                foreach (var item in listDto)
                {
                    //item.StateName = Enum.GetName(typeof(Status), item.DepartmentStatus);
                    var user = await _userManager.FindByIdAsync(AbpSession.GetUserId().ToString());
                    item.CreatorUserName = (await _userManager.GetUserByIdAsync(user.Id)).UserName;
                }
                return new PagedResultDto<DepartmentListDto>(totalCount, listDto);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public override async Task<DepartmentDto> CreateAsync(CreateDepartmentDto input)
        {
            try
            {
                var department = MapToEntity(input);
                var existdepartment = await _Repository.FirstOrDefaultAsync(a => a.Name == department.Name);
                if (existdepartment == null)
                {
                    department.Id = await Repository.InsertAndGetIdAsync(department);

                }
                else
                {
                    throw new UserFriendlyException(string.Format((Exceptions.ObjectAlreadyExisted), input));
                }
                var departmentDto = MapToEntityDto(department);
                //  CurrentUnitOfWork.SaveChanges();
                return departmentDto;
            }
            catch (NullReferenceException e)
            {
                throw;

            }
        }
        public async override Task<DepartmentDto> UpdateAsync(EditDepartmentDto input)
        {

            var Department = Repository.GetAsync(input.Id).Result;
            if (Department == null)
                throw new UserFriendlyException(string.Format((Exceptions.ObjectWasNotFound), input.Id));
            return await base.UpdateAsync(input);

        }


        public override async Task<DepartmentDto> GetAsync(EntityDto<int> input)
        {
            var Department = await Repository.FirstOrDefaultAsync(input.Id);
            if (Department == null)
                throw new UserFriendlyException(string.Format((Exceptions.ObjectWasNotFound), Tokens.Department));
            var DepartmentDto = ObjectMapper.Map<DepartmentDto>(Department);
            DepartmentDto.CreatorUserName = (await _userManager.GetUserByIdAsync(DepartmentDto.CreatorUserId.Value)).Name;

            return DepartmentDto;
        }
        public override async Task DeleteAsync(EntityDto<int> input)
        {
            var Department = await Repository.FirstOrDefaultAsync(input.Id);
            if (Department == null)
                throw new UserFriendlyException(string.Format((Exceptions.ObjectWasNotFound), Tokens.Department));
            await Repository.DeleteAsync(Department);
            MapToEntityDto(Department);

        }

    }
}
