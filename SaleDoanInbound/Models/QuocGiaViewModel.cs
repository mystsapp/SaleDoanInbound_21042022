using Data.Models_QLTaiKhoan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace SaleDoanInbound.Models
{
    public class QuocGiaViewModel
    {
        public IPagedList<Quocgia> Quocgias { get; set; }
        public Quocgia Quocgia { get; set; }
        public string StrUrl { get; set; }
    }
}
