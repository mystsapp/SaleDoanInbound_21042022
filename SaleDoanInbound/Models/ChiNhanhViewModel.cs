
using Data.Models_QLT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace SaleDoanInbound.Models
{
    public class ChiNhanhViewModel
    {
        public IPagedList<Dmchinhanh> Dmchinhanhs { get; set; }
        public Dmchinhanh Dmchinhanh { get; set; }
        public string StrUrl { get; set; }
    }
}
