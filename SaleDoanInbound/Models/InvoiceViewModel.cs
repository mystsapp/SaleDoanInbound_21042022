using Data.Models_IB;
using Data.Models_QLT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace SaleDoanInbound.Models
{
    public class InvoiceViewModel
    {
        public IEnumerable<Invoice> Invoices { get; set; }
        public IEnumerable<CTVAT> CTVATs { get; set; }
        public CTVAT CTVAT { get; set; }
        public CTVAT CTInvoice { get; set; }
        public IEnumerable<CTVAT> CTInvoices { get; set; }
        public IEnumerable<LoaiIV> LoaiIVs { get; set; }
        public IEnumerable<CacNoiDungHuyTour> CacNoiDungHuyTours { get; set; }
        public IEnumerable<Ngoaite> Ngoaites { get; set; } // qltour
        public Invoice Invoice { get; set; }
        public TourIB TourIB { get; set; }
        public Tour Tour { get; set; }
        public string StrUrl { get; set; }
        public string SoTienBangChu { get; set; }
    }
}
