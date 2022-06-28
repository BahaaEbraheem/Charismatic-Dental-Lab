using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Charismatic.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charismatic.Products.Dtos
{
    [AutoMap(typeof(Product))]
    public class ProductListDto: EntityDto<int>
    {
        [Required]
        [StringLength(600)]
        public string Name { get; set; }
        public double? Price { get; set; }
        public string Image { get; set; }
        public long? CreatorUserId { get; set; }

        public string CreatorUserName { get; set; }

        public DateTime CreationTime { get; set; }
    }
}
