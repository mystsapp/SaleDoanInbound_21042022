using Data.Dtos;
using Data.Models_IB;
using Data.Models_QLT;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SaleDoanInbound.Models
{
    public class BaoCaoViewModel
    {
        public string TuNgay { get; set; }
        public string DenNgay { get; set; }
        public IEnumerable<Dmchinhanh> Dmchinhanhs { get; set; }
        public IEnumerable<TourBaoCaoDto> TourBaoCaoDtos { get; set; }
        public IEnumerable<TourBaoCaoDtosGroupByNguoiTaoViewModel> TourBaoCaoDtosGroupByNguoiTaos { get; set; }
        public decimal? TongCong { get; set; }
        public int TongSK { get; set; }

        public IEnumerable<ListViewModel> Thangs { get; set; }

        // theo thang
        public IEnumerable<TourBaoCaoTheoThangViewModel> TourBaoCaoTheoThangs1 { get; set; }

        public IEnumerable<TourBaoCaoTheoThangViewModel> TourBaoCaoTheoThangs2 { get; set; }
        // theo thang

        // theo ngay
        public TourBaoCaoDtosTheoNgay TourBaoCaoDtosTheoNgay { get; set; }
        public IEnumerable<Tourkind> Tourkinds { get; set; } // qltour
        // theo ngay

        // theo THI TRUONNG
        public IEnumerable<Phongban> Phongbans { get; set; }
        // theo THI TRUONNG

        // Bien nhan Excel
        public IEnumerable<BienNhanDto> BienNhanDtos { get; set; }
        // Bien nhan Excel
    }
}
