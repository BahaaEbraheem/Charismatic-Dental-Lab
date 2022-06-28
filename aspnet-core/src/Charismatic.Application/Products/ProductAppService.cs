using Charismatic.Products.Dtos;
using Charismatic.CrudAppServiceBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Charismatic.Models;
using Charismatic.Domain.Product;
using Charismatic.Authorization.Users;
using Abp.Domain.Repositories;
using ITLand.CMMS.Libs.DevExtreme;
using Abp.Application.Services.Dto;

namespace Charismatic.Products
{
    public class ProductAppService : CharismaticAsyncCrudAppService<Product, ProductDto, int, CharismaticBaseListInputDto, CreateProductInput, EditProductInput>, IProductAppService
    {
        private readonly IProductManager _productManager;
        private readonly UserManager _userManager;
        public ProductAppService(IRepository<Product> repository, IProductManager productManager, UserManager userManager)
            : base(repository)
        {
            _productManager = productManager;
            _userManager = userManager;
        }
        /// <summary>
        /// filtering list params
        /// </summary>
        /// <param name="input">search-filter</param>
        /// <returns>IQueryable of Product</returns>
        protected override IQueryable<Product> CreateFilteredQuery(CharismaticBaseListInputDto input)
        {
            var data = base.CreateFilteredQuery(input);
            if (input.HasFilter)
            {
                data = new DataSourceLoaderImpl<Product>(data, input, default, true).LoadAsync().Result;
            }

            return data;
        }
        public async Task<PagedResultDto<ProductListDto>> GetAllProductsAsync(CharismaticBaseListInputDto input)
        {
            var data = CreateFilteredQuery(input);
            var totalCount = await AsyncQueryableExecuter.CountAsync(data);
            data = ApplySorting(data, input);
            data = ApplyPaging(data, input);
            var list = await AsyncQueryableExecuter.ToListAsync(data);
            var listDto = ObjectMapper.Map<List<ProductListDto>>(list);
            foreach (var item in listDto)
            {
                if (item.CreatorUserId.HasValue)
                    item.CreatorUserName = (await _userManager.GetUserByIdAsync(item.CreatorUserId.Value)).UserName;
            }
            return new PagedResultDto<ProductListDto>(totalCount, listDto);
        }

        override public async Task<ProductDto> CreateAsync(CreateProductInput input)
        {
            return await base.CreateAsync(input);
        }

        public async Task<ListResultDto<ProductListDto>> GetAllForChoose()
        {
            var products = await Repository.GetAllListAsync();
            return new ListResultDto<ProductListDto>(
                ObjectMapper.Map<List<ProductListDto>>(products)
                );
        }
    }
}
