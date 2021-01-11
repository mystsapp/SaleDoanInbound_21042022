using Data.Models_IB;
using Data.Models_QLT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SaleDoanInbound.Models
{
    public class DSKhachHangViewModel
    {
        public KhachHang KhachHang { get; set; }
        public IEnumerable<KhachHang> KhachHangs { get; set; }
        public Tour Tour { get; set; }
        public IEnumerable<ListViewModel> GioiTinhs { get; set; }

        public IEnumerable<Khachtour> ListDsKhach { get; set; }
    }
}
