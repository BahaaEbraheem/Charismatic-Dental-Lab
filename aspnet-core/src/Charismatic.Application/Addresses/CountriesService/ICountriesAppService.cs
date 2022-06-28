using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Charismatic.Addresses.Dto.CountryDto;
using Charismatic.CrudAppServiceBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charismatic.Addresses.CountriesService
{
    public interface ICountriesAppService: IApplicationService
    {
        public Task<ListResultDto<CountryListDto>> GetAllAsync(GetAllCountriesInput input);
    }
}
