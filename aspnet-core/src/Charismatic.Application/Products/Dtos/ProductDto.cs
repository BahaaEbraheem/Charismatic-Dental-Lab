using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Charismatic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charismatic.Products.Dtos
{
    [AutoMap(typeof(Product))]
    public class ProductDto: FullAuditedEntityDto
    {
        public string Name { get; set; }
        public double? Price { get; set; }
        public string Image { get; set; }
    }
}
