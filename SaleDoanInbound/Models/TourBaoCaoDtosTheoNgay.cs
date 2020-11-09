using Data.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SaleDoanInbound.Models
{
    public class TourBaoCaoDtosTheoNgay
    {
        
        public IEnumerable<TourBaoCaoDto> TourBaoCaoDtos { get; set; }

        public int TongSK { get; set; }
        public decimal TongDS { get; set; }
        
        public int TongSKCacDoanDaThanhLy { get; set; }
        public decimal TongDSCacDoanDaThanhLy { get; set; }

        public int TongSKCacDoanChuaThanhLy { get; set; }
        public decimal TongDSCacDoanChuaThanhLy { get; set; }

        public int TongSKCacDoanChuaKyHD { get; set; }
        public decimal TongDSCacDoanChuaKyHD { get; set; }
    }
}
