using Data.Models_IB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SaleDoanInbound.Models
{
    public class BienNhanViewModel
    {
        public IEnumerable<BienNhan> BienNhans { get; set; }
        public IEnumerable<CacNoiDungHuyTour> CacNoiDungHuyTours { get; set; }
        public BienNhan BienNhan { get; set; }
        public TourIB TourIB { get; set; }
        public string StrUrl { get; set; }
    }
}
