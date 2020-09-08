using Data.Models_IB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace SaleDoanInbound.Models
{
    public class LoaiIVViewModel
    {
        public LoaiIV LoaiIV { get; set; }
        public IPagedList<LoaiIV> LoaiIVs { get; set; }
        public string StrUrl { get; set; }
        
    }
}
