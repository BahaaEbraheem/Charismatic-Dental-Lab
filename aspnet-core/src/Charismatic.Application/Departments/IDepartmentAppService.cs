using Abp.Application.Services.Dto;
using Charismatic.CrudAppServiceBase;
using Charismatic.Departments.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charismatic.Departments
{
   public interface IDepartmentAppService : ICharismaticAsyncCrudAppService<DepartmentDto, int, CharismaticBaseListInputDto, CreateDepartmentDto, EditDepartmentDto>
    {
        Task<PagedResultDto<DepartmentListDto>> GetAllDepartmentsAsync(CharismaticBaseListInputDto input);

    }
}
