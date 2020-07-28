using Data.Models;
using Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace SaleDoanInbound.Models
{
    public class DiemTQViewModel
    {
        public IPagedList<Dmdiemtq> Dmdiemtqs { get; set; }
        public Dmdiemtq Dmdiemtq { get; set; }
        public string StrUrl { get; set; }
    }
}
