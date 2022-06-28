using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Charismatic.Addresses.Dto.CountryDto;
using Charismatic.CrudAppServiceBase;
using Charismatic.Domain.Address;
using Charismatic.Models.Address;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charismatic.Addresses.CountriesService
{
    public class CountriesAppService : CharismaticAppServiceBase,ICountriesAppService
    {
        private readonly IRepository<Country> _countriesRepository;
        private readonly IAddressManager _addressManager;
            
        public CountriesAppService(IRepository<Country> countriesRepository, IAddressManager addressManager)
        {
            this._countriesRepository = countriesRepository;
            this._addressManager = addressManager;
        }

        public async Task<ListResultDto<CountryListDto>> GetAllAsync(GetAllCountriesInput input)
        {
            var countries = await _countriesRepository.GetAllListAsync();
            return new ListResultDto<CountryListDto>(
                ObjectMapper.Map<List<CountryListDto>>(countries)
                );
        }
    }
}
