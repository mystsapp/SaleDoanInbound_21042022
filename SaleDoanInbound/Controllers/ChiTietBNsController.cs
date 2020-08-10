using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Repository;
using Microsoft.AspNetCore.Mvc;
using SaleDoanInbound.Models;

namespace SaleDoanInbound.Controllers
{
    public class ChiTietBNsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        [BindProperty]
        public ChiTietBNViewModel ChiTietBNVM { get; set; }

        public ChiTietBNsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            ChiTietBNVM = new ChiTietBNViewModel()
            {
                ChiTietBN = new Data.Models_IB.ChiTietBN()
            };
        }
        public IActionResult ChiTietBNFind(string strUrl, string tabActive, string chiTietBNSearchString)
        {
            ChiTietBNVM.StrUrl = strUrl + "&tabActive=" + tabActive; // for redirect tab
            ChiTietBNVM.StrUrl = strUrl + "&chiTietBNSearchString=" + chiTietBNSearchString; // and searchtring
            return Redirect(strUrl);
        }
    }
}