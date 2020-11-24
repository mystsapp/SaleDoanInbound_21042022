using Data.Models_IB;
using Data.Models_QLT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SaleDoanInbound.Models
{
    public class PhanKhuCNViewModel
    {
        public IEnumerable<PhanKhuCN> PhanKhuCNs { get; set; }
        public IEnumerable<Dmchinhanh> Dmchinhanhs { get; set; }
        public IEnumerable<Phongban> Phongbans { get; set; }
        public IEnumerable<Role> Roles { get; set; }
        public PhanKhuCN PhanKhuCN { get; set; }
        public string StrUrl { get; set; }
    }
}
