using Abp.Application.Services.Dto;
using Charismatic.CrudAppServiceBase;
using Charismatic.Admins.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charismatic.Admins
{
    public interface IAdminAppService : ICharismaticAsyncCrudAppService<AdminDto, int, CharismaticBaseListInputDto, CreateAdminDto, EditAdminDto>
    {
        Task<PagedResultDto<AdminListDto>> GetAllAdminsAsync(CharismaticBaseListInputDto input);

    }
}
