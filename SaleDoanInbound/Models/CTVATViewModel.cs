using Data.Models_IB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SaleDoanInbound.Models
{
    public class CTVATViewModel
    {
        public IEnumerable<CTVAT> CTVATs { get; set; }
        public IEnumerable<ListViewModel> ListTrueFalse { get; set; }
        public CTVAT CTVAT { get; set; }
        public CTVAT CTInvoice { get; set; }
        public Invoice Invoice { get; set; }
        public string StrUrl { get; set; }
    }
}
