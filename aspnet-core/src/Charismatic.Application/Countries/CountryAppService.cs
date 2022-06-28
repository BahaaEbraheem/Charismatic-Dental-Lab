using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.UI;
using Charismatic.Authorization.Users;
using Charismatic.Centers.Dto;
using Charismatic.Countries.Dto;
using Charismatic.CrudAppServiceBase;
using Charismatic.Doctors.Dto;
using Charismatic.Domain.Centers;
using Charismatic.Localization.SourceFiles;
using Charismatic.Models;
using Charismatic.Models.Address;
using Charismatic.Roles.Dto;
using Charismatic.Specialties.Dto;
using ITLand.CMMS.Libs.DevExtreme;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charismatic.Countries
{
    public class CountryAppService : CharismaticAsyncCrudAppService<Country, CountryDto, int, CharismaticBaseListInputDto, CreateCountryDto, EditCountryDto>, ICountryAppService
    {
        private readonly IRepository<Country> _repository;
        private readonly IRepository<State> _stateRepository;
        private readonly UserManager _userManager;
        public CountryAppService(IRepository<Country> repository, UserManager userManager, IRepository<State> stateRepository)
            : base(repository)
        {
            _stateRepository = stateRepository;
            _repository = repository;
            _userManager = userManager;
        }
        protected IQueryable<Country> CountryFilteredQuery(CharismaticBaseListInputDto input)
        {
            var data = base.CreateFilteredQuery(input);
            //data = data.WhereIf(input.ReasonRelatedTo.HasValue, i => i.RelatedTo == input.ReasonRelatedTo.Value);

            if (input.HasFilter)
            {
                data = new DataSourceLoaderImpl<Country>(data, input, default, true).LoadAsync().Result;
            }

            return data;
        }
        public async Task<PagedResultDto<CountryListDto>> GetAllCountriesAsync(CharismaticBaseListInputDto input)
        {
            var data = CreateFilteredQuery(input);
            var totalCount = await AsyncQueryableExecuter.CountAsync(data);
            data = ApplySorting(data, input);
            data = ApplyPaging(data, input);
            var list = await AsyncQueryableExecuter.ToListAsync(data);
            var listDto = ObjectMapper.Map<List<CountryListDto>>(list);
            foreach (var item in listDto)
            {
                if (item.CreatorUserId.HasValue)
                    item.CreatorUserName = (await _userManager.GetUserByIdAsync(item.CreatorUserId.Value)).UserName;
            }
            return new PagedResultDto<CountryListDto>(totalCount, listDto);

        }
        public override async Task<CountryDto> CreateAsync(CreateCountryDto input)
        {
            var country = MapToEntity(input);
            //  center.is = true;
            country.Id = await Repository.InsertAndGetIdAsync(country);

            var countryDto = MapToEntityDto(country);

            //if (centerDto.CreatorUserId.HasValue)
            //    centerDto.CreatorUserName = (await _userManager.GetUserByIdAsync(centerDto.CreatorUserId.Value)).Name;
            //centerDto.LockedStatus = false;

            return countryDto;
        }


        public override Task<CountryDto> UpdateAsync(EditCountryDto input)
        {
            return base.UpdateAsync(input);

        }
        public override async Task<CountryDto> GetAsync(EntityDto<int> input)
        {
            var country = await Repository.FirstOrDefaultAsync(input.Id);
            if (country == null)
                throw new UserFriendlyException(string.Format((Exceptions.ObjectWasNotFound), Tokens.Country));

            var countryDto = ObjectMapper.Map<CountryDto>(country);
            if (countryDto.CreatorUserId.HasValue)
                countryDto.CreatorUserName = (await _userManager.GetUserByIdAsync(countryDto.CreatorUserId.Value)).Name;

            return countryDto;
            
        }

        public override async Task DeleteAsync(EntityDto<int> input)
        {

            var country = await Repository.FirstOrDefaultAsync(input.Id);
            if (country == null)
                throw new UserFriendlyException(string.Format((Exceptions.ObjectWasNotFound), Tokens.Country));

            await Repository.DeleteAsync(country);

            MapToEntityDto(country);

        }

        public async Task<List<StateListSelect>> GetCities(int countryId)
        {
           return  ObjectMapper.Map<List<StateListSelect>>(await _stateRepository.GetAllListAsync(c=>c.CountryId==countryId));
        }


    }
}
