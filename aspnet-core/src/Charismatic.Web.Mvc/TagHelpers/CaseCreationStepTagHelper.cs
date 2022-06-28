using Charismatic.Helpers;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Charismatic.Enums;

namespace Charismatic.Web.TagHelpers
{
    [HtmlTargetElement("CaseCreationStep")]
    public class CaseCreationStepTagHelper:TagHelper
    {
        public CaseCreationStep  Step{ get; set; }
        public bool Active { get; set; } = false;
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.Content.SetHtmlContent(
                $@"<li class=""step-item {(Active?"active":null)}"">
                        <span>{(int)Step}</span>
                        <br>
                        <span class=""font-weight-bold"">{EnumHelper<CaseCreationStep>.GetDisplayValue(Step)}</span>
                   </li>"
                );
        }
    }
}
