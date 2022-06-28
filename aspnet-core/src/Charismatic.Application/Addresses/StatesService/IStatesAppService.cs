using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Charismatic.Addresses.Dto.SteteDto;
using Charismatic.CrudAppServiceBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charismatic.Addresses.StatesService
{
    public interface IStatesAppService :IApplicationService
    {
        public Task<ListResultDto<StateListDto>> GetAllAsync(GetAllStatesInput input);
    }
}
