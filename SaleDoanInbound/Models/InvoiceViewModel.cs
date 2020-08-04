﻿using Data.Models_IB;
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
        public Invoice Invoice { get; set; }
        public TourIB TourIB { get; set; }
        public string StrUrl { get; set; }
    }
}
