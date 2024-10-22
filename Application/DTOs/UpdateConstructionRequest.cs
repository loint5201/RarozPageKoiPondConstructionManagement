using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class UpdateConstructionRequest
    {
        public int? Status { get; set; }
        public string? RejectReason { get; set; }
    }
}
