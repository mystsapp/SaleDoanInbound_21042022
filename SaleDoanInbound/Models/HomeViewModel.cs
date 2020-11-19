using Data.Models_IB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SaleDoanInbound.Models
{
    public class HomeViewModel
    {
        public IEnumerable<Tour> Tours { get; set; }
        public IEnumerable<TourTheoThiTruongViewModel> CurrentTourTheoThiTruongViewModels { get; set; }
        public IEnumerable<TourTheoThiTruongViewModel> PreviousTourTheoThiTruongViewModels { get; set; }
    }
}
