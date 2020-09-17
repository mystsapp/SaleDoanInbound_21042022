using Data.Models_IB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SaleDoanInbound.Models
{
    public class ChiTietBNViewModel
    {
        public BienNhan BienNhan { get; set; }
        public ChiTietBN ChiTietBN { get; set; }
        public IEnumerable<ChiTietBN> ChiTietBNs { get; set; }
        public string StrUrl { get; set; }
    }
}
