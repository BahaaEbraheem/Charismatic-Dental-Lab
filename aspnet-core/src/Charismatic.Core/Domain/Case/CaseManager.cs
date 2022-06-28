using Abp.Domain.Repositories;
using Abp.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charismatic.Domain.Case
{
    public class CaseManager: DomainService,ICaseManager
    {
        private readonly IRepository<Charismatic.Models.Case> _caseRepository;
        public CaseManager(IRepository<Charismatic.Models.Case> caseRepository)
        {
            _caseRepository = caseRepository;
        }
    }
}
