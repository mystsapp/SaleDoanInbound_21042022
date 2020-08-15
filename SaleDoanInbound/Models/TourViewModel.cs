using Data.Dtos;
using Data.Models_IB;
using Data.Models_QLT;
using Data.Models_QLTaiKhoan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace SaleDoanInbound.Models
{
    public class TourViewModel
    {
        public IPagedList<TourDto> TourDtos { get; set; }
        public IEnumerable<Data.Models_QLTaiKhoan.Thanhpho> Thanhphos { get; set; } // qltaikhoan
        public IEnumerable<Company> Companies { get; set; } // qltour
        public IEnumerable<Tourkind> Tourkinds { get; set; } // qltour
        public IEnumerable<Data.Models_QLT.Dmchinhanh> Dmchinhanhs { get; set; } // qltour
        public List<Data.Models_QLT.Phongban> listPhong { get; set; } // qltour
        public List<ListViewModel> NguonTours { get; set; }
        public Tour Tour { get; set; }
        public TourDto TourDto { get; set; }
        public string StrUrl { get; set; }
    }
}
