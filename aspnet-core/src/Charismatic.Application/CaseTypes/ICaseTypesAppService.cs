using Abp.Application.Services.Dto;
using Charismatic.CaseTypes.Dto;
using Charismatic.Centers.Dto;
using Charismatic.CrudAppServiceBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charismatic.CaseTypes
{
    public interface ICaseTypesAppService : ICharismaticAsyncCrudAppService<CaseTypeDto, int, CharismaticBaseListInputDto, CreateCaseTypeDto, EditCaseTypeDto>
    {
        public Task<PagedResultDto<CaseTypeListDto>> GetAllCaseTypesAsync(CharismaticBaseListInputDto input);
        public Task<ListResultDto<CaseTypeListDto>> GetAllCaseTypesForCurrentUserAsync();
    }
}
