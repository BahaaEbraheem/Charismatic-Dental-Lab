using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.UI;
using Charismatic.Authorization.Users;
using Charismatic.CaseTypes.Dto;
using Charismatic.CrudAppServiceBase;
using Charismatic.Domain.CaseType;
using Charismatic.Localization.SourceFiles;
using Charismatic.Models;
using Charismatic.Roles.Dto;
using ITLand.CMMS.Libs.DevExtreme;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charismatic.CaseTypes
{
    public class CaseTypesAppService : CharismaticAsyncCrudAppService<CaseType, CaseTypeDto, int, CharismaticBaseListInputDto, CreateCaseTypeDto, EditCaseTypeDto>, ICaseTypesAppService
    {
        private readonly ICaseTypeManager _caseTypeManager;
        private readonly UserManager _userManager;

        public CaseTypesAppService(IRepository<CaseType> repository, ICaseTypeManager caseTypeManager, UserManager userManager)
            : base(repository)
        {
            _caseTypeManager = caseTypeManager;
            _userManager = userManager;
        }

        /// <summary>
        /// filtering list params
        /// </summary>
        /// <param name="">search-filter</param>
        /// <returns></returns>
        /// 
        protected IQueryable<CaseType> CreateFilteredQuery(CharismaticBaseListInputDto input)
        {
            var data = base.CreateFilteredQuery(input);
            //data = data.WhereIf(input.ReasonRelatedTo.HasValue, i => i.RelatedTo == input.ReasonRelatedTo.Value);

            if (input.HasFilter)
            {
                data = new DataSourceLoaderImpl<CaseType>(data, input, default, true).LoadAsync().Result;
            }

            return data;
        }



        public async Task<PagedResultDto<CaseTypeListDto>> GetAllCaseTypesAsync(CharismaticBaseListInputDto input)
        {
            var data = CreateFilteredQuery(input);
            var totalCount = await AsyncQueryableExecuter.CountAsync(data);
            data = ApplySorting(data, input);
            data = ApplyPaging(data, input);
            var list = await AsyncQueryableExecuter.ToListAsync(data);
            var listDto = ObjectMapper.Map<List<CaseTypeListDto>>(list);
            foreach (var item in listDto)
            {
                if (item.CreatorUserId.HasValue)
                    item.CreatorUserName = (await _userManager.GetUserByIdAsync(item.CreatorUserId.Value)).UserName;
            }
            return new PagedResultDto<CaseTypeListDto>(totalCount, listDto);
        }

        public override async Task<CaseTypeDto> CreateAsync(CreateCaseTypeDto input)
        {
            var caseType = MapToEntity(input);
            //  caseType.is = true;
            caseType.Id = await Repository.InsertAndGetIdAsync(caseType);

            var caseTypeDto = MapToEntityDto(caseType);

            //if (caseTypeDto.CreatorUserId.HasValue)
            //    caseTypeDto.CreatorUserName = (await _userManager.GetUserByIdAsync(caseTypeDto.CreatorUserId.Value)).Name;
            //caseTypeDto.LockedStatus = false;

            return caseTypeDto;
        }

        public override Task<CaseTypeDto> UpdateAsync(EditCaseTypeDto input)
        {
            return base.UpdateAsync(input);

        }


        public override async Task<CaseTypeDto> GetAsync(EntityDto<int> input)
        {
            var caseType = await Repository.FirstOrDefaultAsync(input.Id);
            if (caseType == null)
                throw new UserFriendlyException(string.Format((Exceptions.ObjectWasNotFound), Tokens.CaseType));

            var caseTypeDto = ObjectMapper.Map<CaseTypeDto>(caseType);
            if (caseTypeDto.CreatorUserId.HasValue)
                caseTypeDto.CreatorUserName = (await _userManager.GetUserByIdAsync(caseTypeDto.CreatorUserId.Value)).Name;

            return caseTypeDto;
            ;
        }
        public override async Task DeleteAsync(EntityDto<int> input)
        {

            var caseType = await Repository.FirstOrDefaultAsync(input.Id);
            if (caseType == null)
                throw new UserFriendlyException(string.Format((Exceptions.ObjectWasNotFound), Tokens.CaseType));

            await Repository.DeleteAsync(caseType);

            MapToEntityDto(caseType);

        }


        public async Task<ListResultDto<CaseTypeListDto>> GetAllCaseTypesForCurrentUserAsync()
        {

            var list = await Repository.GetAllListAsync();
            var filteredList = ObjectMapper.Map<List<CaseTypeListDto>>(list);

            return new ListResultDto<CaseTypeListDto>(items: filteredList);
        }
    }

}
