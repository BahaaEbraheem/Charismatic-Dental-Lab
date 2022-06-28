using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charismatic.CrudAppServiceBase
{


    public interface ICharismaticAsyncCrudAppService<TEntityDto, TPrimaryKey>
 : IAsyncCrudAppService<TEntityDto, TPrimaryKey>
 where TEntityDto : IEntityDto<TPrimaryKey>
    {

    }

    public interface ICharismaticAsyncCrudAppService<TEntityDto, TPrimaryKey, in TGetAllInput>
        : ICharismaticAsyncCrudAppService<TEntityDto, TPrimaryKey, TGetAllInput, TEntityDto, TEntityDto>
        where TEntityDto : IEntityDto<TPrimaryKey>
    {
    }

    public interface ICharismaticAsyncCrudAppService<TEntityDto, TPrimaryKey, in TGetAllInput, in TCreateInput, in TUpdateInput>
        : IAsyncCrudAppService<TEntityDto, TPrimaryKey, TGetAllInput, TCreateInput, TUpdateInput>
        where TEntityDto : IEntityDto<TPrimaryKey>
        where TUpdateInput : IEntityDto<TPrimaryKey>
    {
    }

}
