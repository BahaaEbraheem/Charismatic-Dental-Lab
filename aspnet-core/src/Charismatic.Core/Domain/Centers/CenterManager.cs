using Abp.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Charismatic.Models;
namespace Charismatic.Domain.Centers
{
    public class CenterManager : ICenterManager
    {
        private readonly IRepository<Center> _centerRepository;

        public CenterManager(IRepository<Center> centerRepository)
        {
            _centerRepository = centerRepository;
        }
    }
}
