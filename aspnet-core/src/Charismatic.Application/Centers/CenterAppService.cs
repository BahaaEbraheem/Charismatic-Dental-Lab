using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.UI;
using Charismatic.Authorization.Users;
using Charismatic.Centers.Dto;
using Charismatic.CrudAppServiceBase;
using Charismatic.DoctorCenters;
using Charismatic.Domain.Centers;
using Charismatic.Localization.SourceFiles;
using Charismatic.Models;
using Charismatic.Models.Address;
using Charismatic.Roles.Dto;
using ITLand.CMMS.Libs.DevExtreme;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charismatic.Centers
{
    public class CenterAppService : CharismaticAsyncCrudAppService<Center, CenterDto, int, CharismaticBaseListInputDto, CreateCenterDto, EditCenterDto>, ICenterAppService
    {
        private readonly UserManager _userManager;
        private readonly IDoctorCentersAppService _doctorcenterAppService;
        private readonly IRepository<State> _stateRepository;

        public CenterAppService(IDoctorCentersAppService doctorcenterAppService,IRepository<Center> Repository, UserManager userManager, IRepository<State> stateRepository)
            : base(Repository)
        {

            _userManager = userManager;
            _doctorcenterAppService = doctorcenterAppService;
            _stateRepository = stateRepository;
        }

        /// <summary>
        /// filtering list params
        /// </summary>
        /// <param name="input">search-filter</param>
        /// <returns></returns>
        /// 
        protected override IQueryable<Center> CreateFilteredQuery(CharismaticBaseListInputDto input)
        {
            var data = base.CreateFilteredQuery(input);
            //data = data.WhereIf(input.ReasonRelatedTo.HasValue, i => i.RelatedTo == input.ReasonRelatedTo.Value);

            if (input.HasFilter)
            {
                data = new DataSourceLoaderImpl<Center>(data, input, default, true).LoadAsync().Result;
            }

            return data;
        }
        public async Task<PagedResultDto<CenterListDto>> GetAllCentersAsync(CharismaticBaseListInputDto input)
        {
            try
            {
                var data = CreateFilteredQuery(input);
                var totalCount = await AsyncQueryableExecuter.CountAsync(data);
                data = ApplySorting(data, input);
                data = ApplyPaging(data, input);
                var list = await AsyncQueryableExecuter.ToListAsync(data);
                var listDto = ObjectMapper.Map<List<CenterListDto>>(list);
                foreach (var item in listDto)
                {
                    if (item.StateId != 0)
                    {
                        var state = await _stateRepository.GetAsync(item.StateId);
                        if (item.CreatorUserId.HasValue)
                            item.CityName = state.Name;
                        item.CountryId = state.CountryId;
                    }
         
                  
                    item.CreatorUserName = (await _userManager.GetUserByIdAsync(item.CreatorUserId.Value)).UserName;
                }
                return new PagedResultDto<CenterListDto>(totalCount, listDto);
            }
            catch (Exception)
            {

                throw;
            }

        }
        public override async Task<CenterDto> CreateAsync(CreateCenterDto input)
        {
            try
            {

                var center = MapToEntity(input);
                //  center.is = true;
                center.Id= await Repository.InsertAndGetIdAsync(center);

                var countryDto = MapToEntityDto(center);

                if (countryDto.CreatorUserId.HasValue)
                {
                    countryDto.CreatorUserName = (await _userManager.GetUserByIdAsync(countryDto.CreatorUserId.Value)).Name;
                  
                }
                //countryDto. = false;

                return countryDto;
                //   // var center = null;// MapToEntity(input);
                //   CenterDto centerDto = null;
                //   var existCenter = Repository.FirstOrDefault(cen => cen.Name == input.Name);
                //   if (existCenter == null)
                //   {

                //     var  center = MapToEntity(input);
                //        centerDto = ObjectMapper.Map<CenterDto>(center);
                //       if (centerDto.CreatorUserId.HasValue)
                //           centerDto.CreatorUserName = (await _userManager.GetUserByIdAsync(centerDto.CreatorUserId.Value)).Name;
                //       //     centerDto.LockedStatus = false;
                //       return centerDto;
                //       //if (center.Country == null)
                //       //{
                //       //    center.CountryId = null;
                //       //}
                //       //else
                //       //{
                //       //    center.CountryId = center.Country.Id;
                //       //}
                //       //if (center.StateId == 0)
                //       //{
                //       //    center.StateId = null;
                //       //}
                //       //center.Id = await Repository.InsertAndGetIdAsync(center);
                //       ;
                //   }
                //   else
                //   {
                //      // throw new UserFriendlyException(string.Format((Exceptions.ObjectAlreadyExisted), Tokens.Center));
                //       center.Id = Repository.FirstOrDefaultAsync(cen => cen.Name == center.Name).Result.Id;
                //   }
                ////   await Repository.InsertAndGetIdAsync(center);
                //   var centerDto = MapToEntityDto(center);
                //   if (centerDto.CreatorUserId.HasValue)
                //       centerDto.CreatorUserName = (await _userManager.GetUserByIdAsync(centerDto.CreatorUserId.Value)).Name;
                //   //     centerDto.LockedStatus = false;
                //   return centerDto;

            }
            catch (NullReferenceException e)
            {

                throw;
            }
            

            
        
        }

        public override Task<CenterDto> UpdateAsync(EditCenterDto input)
        {
            return base.UpdateAsync(input);

        }
        public override async Task<CenterDto> GetAsync(EntityDto<int> input)
        {
            var center = await Repository.FirstOrDefaultAsync(input.Id);
            if (center == null)
                throw new UserFriendlyException(string.Format((Exceptions.ObjectWasNotFound), Tokens.Center));
            var centerDto = ObjectMapper.Map<CenterDto>(center);
            centerDto.CityName = (await _stateRepository.GetAsync(centerDto.StateId)).Name;
            if (centerDto.CreatorUserId.HasValue)
                centerDto.CreatorUserName = (await _userManager.GetUserByIdAsync(centerDto.CreatorUserId.Value)).Name;
            return centerDto;

        }
        public override async Task DeleteAsync(EntityDto<int> input)
        {

            var center = await Repository.FirstOrDefaultAsync(input.Id);
            if (center == null)
                throw new UserFriendlyException(string.Format((Exceptions.ObjectWasNotFound), Tokens.Center));

            await Repository.DeleteAsync(center);

            MapToEntityDto(center);

        }


        //public async Task CreateCentsers(List<CreateCenterDto> lists)
        //{

        //    var centers = ObjectMapper.Map<List<Center>>(lists);
        //    foreach (var item in centers)
        //    {
        //        await Repository.InsertAsync(item);
        //    }

        //}

        public async Task<List<CenterListDto>> GetAll()
        {
            var centers = await Repository.GetAllListAsync();
            return ObjectMapper.Map<List<CenterListDto>>(centers);
        }

        public async Task<ListResultDto<CenterListDto>> GetAllCentersForCurrentUserAsync()
        {
            var list = await Repository.GetAllListAsync();
            var filteredList = ObjectMapper.Map<List<CenterListDto>>(list);
            return new ListResultDto<CenterListDto>(items: filteredList);
        }

        public async Task<List<CenterListDto>> GetAllCentersForCurrentUserAsync(long? id)
        {
            var centers = await Repository.GetAllListAsync();
            return ObjectMapper.Map<List<CenterListDto>>(centers); ;
        }
    }
}
