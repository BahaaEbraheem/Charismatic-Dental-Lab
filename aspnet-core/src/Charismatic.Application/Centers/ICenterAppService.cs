using Abp.Application.Services.Dto;
using Charismatic.Centers.Dto;
using Charismatic.CrudAppServiceBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charismatic.Centers
{
    public interface ICenterAppService : ICharismaticAsyncCrudAppService<CenterDto, int, CharismaticBaseListInputDto, CreateCenterDto, EditCenterDto>
    {
        Task<PagedResultDto<CenterListDto>> GetAllCentersAsync(CharismaticBaseListInputDto input);
        Task<List<CenterListDto>> GetAll();
        Task<CenterDto> CreateAsync(CreateCenterDto input);
      //  Task<List<CenterListDto>> GetAllCentersForCurrentUserAsync(long? id);
        //Task CreateCentsers(List<CreateCenterDto> lists);
        public Task<ListResultDto<CenterListDto>> GetAllCentersForCurrentUserAsync();
    }
}
