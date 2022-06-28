using Abp.Application.Services.Dto;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charismatic
{
    public class CharismaticBaseListInputDto : PagedAndSortedResultRequestDto
    {
        public string Search { get; set; }
        public string Filter { get; set; }
        public IList FilterExpr { get; set; }

        public int? ReasonRelatedTo { get; set; }

        public bool HasFilter => string.IsNullOrEmpty(Filter) ? false : true;
        public byte? Stage { get; set; }
        public bool? HasPositions { get; set; }
        public bool? IsActive { get; set; }

        public string Keyword { get; set; }
    }
  
}
