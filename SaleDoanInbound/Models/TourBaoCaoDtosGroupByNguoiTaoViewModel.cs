using Data.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SaleDoanInbound.Models
{
    public class TourBaoCaoDtosGroupByNguoiTaoViewModel
    {
        public IEnumerable<TourBaoCaoDto> TourBaoCaoDtos { get; set; }
        public string NguoiTao { get; set; }
    }
}
