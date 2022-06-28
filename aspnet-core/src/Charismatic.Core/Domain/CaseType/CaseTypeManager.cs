using Abp.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charismatic.Domain.CaseType
{
    public class CaseTypeManager: ICaseTypeManager
    {
        private readonly IRepository<Charismatic.Models.CaseType> _caseTypeRepository;

        public CaseTypeManager(IRepository<Charismatic.Models.CaseType> caseTypeRepository)
        {
            _caseTypeRepository = caseTypeRepository;
        }
    }
}
