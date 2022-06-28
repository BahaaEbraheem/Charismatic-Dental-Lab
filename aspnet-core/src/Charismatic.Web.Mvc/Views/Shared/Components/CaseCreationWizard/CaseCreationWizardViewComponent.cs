using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Charismatic.Enums;

namespace Charismatic.Web.Views.Shared.Components.CaseCreationWizard
{
    public class CaseCreationWizardViewComponent : CharismaticViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(CaseCreationStep currentStep)
        {
            var model = new CaseCreationWizardViewModel {
                Steps = Enum.GetValues(typeof(CaseCreationStep)).Cast<CaseCreationStep>().ToList(),
                CurrentStep= currentStep
            };
            return View(model);
        }
    }
}
