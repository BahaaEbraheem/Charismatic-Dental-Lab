using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Charismatic.Enums;

namespace Charismatic.Web.Views.Shared.Components.CaseCreationWizard
{
    public class CaseCreationWizardViewModel
    {
        public List<CaseCreationStep> Steps{ get; set; }
        public CaseCreationStep CurrentStep { get; set; }
    }
}
