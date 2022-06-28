using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Charismatic.Centers.Dto;
using Charismatic.Countries.Dto;
using Charismatic.CrudAppServiceBase;
using Charismatic.Doctors.Dto;
using Charismatic.Specialties.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charismatic.Countries
{
    public interface ICountryAppService : ICharismaticAsyncCrudAppService<CountryDto, int, CharismaticBaseListInputDto, CreateCountryDto, EditCountryDto>
    {
        Task<PagedResultDto<CountryListDto>> GetAllCountriesAsync(CharismaticBaseListInputDto input);
        Task<List<StateListSelect>> GetCities(int countryId);

    }
}
