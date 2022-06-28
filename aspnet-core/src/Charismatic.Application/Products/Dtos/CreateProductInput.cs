using Abp.AutoMapper;
using Charismatic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charismatic.Products.Dtos
{
    [AutoMapTo(typeof(Product))]
    public class CreateProductInput
    {
        public string Name { get; set; }
        public double? Price { get; set; }
        public string Image { get; set; }
    }
}
