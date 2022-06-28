using Abp.Domain.Repositories;
using Abp.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Charismatic.Models;
namespace Charismatic.Domain.Product
{
    public class ProductManager:DomainService,IProductManager
    {
        private readonly IRepository<Charismatic.Models.Product> _productRepository;
        public ProductManager(IRepository<Charismatic.Models.Product> productRepository)
        {
            _productRepository = productRepository;
        }

    }
}
