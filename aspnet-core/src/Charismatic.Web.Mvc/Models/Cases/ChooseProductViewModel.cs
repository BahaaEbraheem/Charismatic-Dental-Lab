using Charismatic.Products.Dtos;
using System.Collections.Generic;

namespace Charismatic.Web.Models.Cases
{
    public class ChooseProductViewModel
    {
        public int CaseId { get; set; }
        public List<ProductListDto> Products { get; set; }
    }
}