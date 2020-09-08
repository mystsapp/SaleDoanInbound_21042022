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
        public List<Data.Models_QLT.Phongban> listPhongMacode { get; set; } // qltour
        public List<Data.Models_QLT.Phongban> listPhongDH { get; set; } // qltour
        public IEnumerable<Data.Models_QLT.Tourprog> listTourProgAsync { get; set; } // qltour
        public IEnumerable<Data.Models_QLT.Khachtour> ListDsKhach { get; set; } // qltour
        public Data.Models_QLT.Tournode TourNoteAsync { get; set; } // qltour
        public List<ChiPhiKhachDto> ListCPKhac { get; set; } // qltour
        public IEnumerable<Dieuxe> ListYeucauxe { get; set; } // qltour
        public IEnumerable<Huongdan> ListHuongdan { get; set; } // qltour
        public IEnumerable<Ngoaite> Ngoaites { get; set; } // qltour
        public List<ListViewModel> NguonTours { get; set; }
        public List<ListViewModel> LoaiKhachs { get; set; }
        public Tour Tour { get; set; }
        public TourDto TourDto { get; set; }
        public IEnumerable<Invoice> Invoices { get; set; }
        public IEnumerable<Invoice> InvoicesInTour { get; set; }
        public IEnumerable<BienNhan> BienNhans { get; set; }
        public IEnumerable<CacNoiDungHuyTour> CacNoiDungHuyTours { get; set; }

        public string StrUrl { get; set; }
        public string tabActive { get; set; }
        public string huy { get; set; }
    }
}
