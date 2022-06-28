using Abp.Application.Services.Dto;
using Charismatic.CrudAppServiceBase;
using Charismatic.Products.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charismatic.Products
{
    public interface IProductAppService: ICharismaticAsyncCrudAppService<ProductDto, int, CharismaticBaseListInputDto, CreateProductInput, EditProductInput>
    {
        Task<PagedResultDto<ProductListDto>> GetAllProductsAsync(CharismaticBaseListInputDto input);
        Task<ListResultDto<ProductListDto>> GetAllForChoose();
    }
}
