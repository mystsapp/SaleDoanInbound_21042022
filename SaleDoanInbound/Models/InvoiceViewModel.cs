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
        public IPagedList<Invoice> Invoices { get; set; }
        public IEnumerable<LoaiIV> LoaiIVs { get; set; }
        public IEnumerable<Ngoaite> Ngoaites { get; set; } // qltour
        public Invoice Invoice { get; set; }
        public TourIB TourIB { get; set; }
        public Tour Tour { get; set; }
        public string StrUrl { get; set; }
    }
}
