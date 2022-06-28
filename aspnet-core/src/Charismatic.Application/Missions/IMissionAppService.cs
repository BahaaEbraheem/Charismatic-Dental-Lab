using Abp.Application.Services.Dto;
using Charismatic.CrudAppServiceBase;
using Charismatic.Missions.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charismatic.Missions
{
   public interface IMissionAppService:ICharismaticAsyncCrudAppService<MissionDto, int, CharismaticBaseListInputDto, CreateMissionDto, EditMissionDto>
    {
        Task<PagedResultDto<MissionListDto>> GetAllMissionsAsync(CharismaticBaseListInputDto input);
        Task AcceptTask(EntityDto<int> input);
        Task CompleteTask(EntityDto<int> input);
        Task RejectTask(EntityDto<int> input);


    }
}
