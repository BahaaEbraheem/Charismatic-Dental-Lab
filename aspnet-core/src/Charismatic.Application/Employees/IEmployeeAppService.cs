using Abp.Application.Services.Dto;
using Charismatic.CrudAppServiceBase;
using Charismatic.Employees.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charismatic.Employees
{
    public interface IEmployeeAppService : ICharismaticAsyncCrudAppService<EmployeeDto, int, CharismaticBaseListInputDto, CreateEmployeeDto, EditEmployeeDto>
    {
        Task<PagedResultDto<EmployeeListDto>> GetAllEmployeesAsync(CharismaticBaseListInputDto input);

    }
}
