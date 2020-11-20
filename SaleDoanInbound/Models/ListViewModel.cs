using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SaleDoanInbound.Models
{
    public class ListViewModel
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string DienGiais { get; set; }
        public string SoTiens { get; set; }

        // company view model
        public string CompanyId { get; set; }
        public string CompanyName { get; set; }
    }
}
