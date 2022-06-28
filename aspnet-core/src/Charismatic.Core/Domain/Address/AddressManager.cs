using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Charismatic.Models.Address;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charismatic.Domain.Address
{
    public class AddressManager: DomainService,IAddressManager
    {
        private readonly IRepository<Country> _countriesRepository;
        private readonly IRepository<State> _statesRepository;

        public AddressManager(IRepository<Country> countriesRepository, IRepository<State> statesRepository)
        {
            this._countriesRepository = countriesRepository;
            _statesRepository = statesRepository;
        }
    }
}
