using Data.Dtos;
using Data.Models_IB;
using Data.Models_QLT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace SaleDoanInbound.Models
{
    public class InvoiceTourViewModel
    {
        public IPagedList<TourIBDto> TourIBs { get; set; }
        public TourIB TourIB { get; set; }
        public Invoice Invoice { get; set; }
        public TourIBDto TourIBDto { get; set; }
        public IEnumerable<Invoice> Invoices { get; set; }
        public IEnumerable<CTVAT> CTVATs { get; set; }

        public IEnumerable<CacNoiDungHuyTour> CacNoiDungHuyTours { get; set; }
        public IEnumerable<Company> Companies { get; set; }
        public string StrUrl { get; set; }
        public string tabActive { get; set; }
    }
}
