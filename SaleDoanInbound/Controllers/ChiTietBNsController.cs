using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Models_IB;
using Data.Repository;
using Data.Utilities;
using Microsoft.AspNetCore.Mvc;
using SaleDoanInbound.Models;

namespace SaleDoanInbound.Controllers
{
    public class ChiTietBNsController : BaseController
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

        public async Task<IActionResult> Create(string bienNhanId, string strUrl)
        {
            ChiTietBNVM.StrUrl = strUrl;
            ChiTietBNVM.BienNhan = await _unitOfWork.bienNhanRepository.GetByIdAsync(bienNhanId);
            ChiTietBNVM.ChiTietBN.BienNhanId = ChiTietBNVM.BienNhan.Id;
            return View(ChiTietBNVM);
        }

        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePost(string bienNhanId, string strUrl)
        {
            // from login session
            var user = HttpContext.Session.Gets<User>("loginUser").SingleOrDefault();

            string temp = "", log = "";

            if (!ModelState.IsValid)
            {
                ChiTietBNVM.StrUrl = strUrl;
                ChiTietBNVM.BienNhan = await _unitOfWork.bienNhanRepository.GetByIdAsync(bienNhanId);
                ChiTietBNVM.ChiTietBN.BienNhanId = ChiTietBNVM.BienNhan.Id;
                return View(ChiTietBNVM);
            }
            ChiTietBNVM.ChiTietBN.NgayTao = DateTime.Now;
            ChiTietBNVM.ChiTietBN.NguoiTao = user.Username;

            if (string.IsNullOrEmpty(ChiTietBNVM.ChiTietBN.Descript))
            {
                ChiTietBNVM.ChiTietBN.Descript = "";
            }
            ChiTietBNVM.ChiTietBN.Descript = ChiTietBNVM.ChiTietBN.Descript.ToUpper();
            
            // ghi log
            ChiTietBNVM.ChiTietBN.LogFile = "-User tạo: " + user.Username + " vào lúc: " + System.DateTime.Now.ToString(); // user.Username
            // sotien --> BN
            var bienNhan = await _unitOfWork.bienNhanRepository.GetByIdAsync(bienNhanId);
            var st = bienNhan.SoTien + ChiTietBNVM.ChiTietBN.Amount;

            if (bienNhan.SoTien != st)
            {
                temp += String.Format("- Số tiền thay đổi: {0:N0} -> {1:N0}, người thay đổi: {2}, vào lúc: {3} ", bienNhan.SoTien, st, user.Username, System.DateTime.Now.ToString());
            }
            bienNhan.SoTien = st;
            // kiem tra thay doi
            if (temp.Length > 0)
            {

                log = System.Environment.NewLine;
                log += "=============";
                log += System.Environment.NewLine;
                log += temp;// + " -User cập nhật tour: " + user.Username + " vào lúc: " + System.DateTime.Now.ToString(); // username
                bienNhan.LogFile = bienNhan.LogFile + log;
                
            }

            // sotien --> BN
            try
            {
                // cap nhat sotien trong BN
                _unitOfWork.bienNhanRepository.Update(bienNhan);

                _unitOfWork.chiTietBNRepository.Create(ChiTietBNVM.ChiTietBN);
                await _unitOfWork.Complete();
                SetAlert("Thêm mới thành công.", "success");
                return Redirect(ChiTietBNVM.StrUrl);
            }
            catch (Exception ex)
            {
                SetAlert(ex.Message, "error");
                ChiTietBNVM.StrUrl = strUrl;
                ChiTietBNVM.BienNhan = await _unitOfWork.bienNhanRepository.GetByIdAsync(bienNhanId);
                ChiTietBNVM.ChiTietBN.BienNhanId = ChiTietBNVM.BienNhan.Id;
                return View(ChiTietBNVM);
            }

        }

        public async Task<IActionResult> Edit(long id, string bienNhanId/*, string tabActive*/, string strUrl)
        {
            ChiTietBNVM.StrUrl = strUrl;// + "&tabActive=" + tabActive; // for redirect tab
            ChiTietBNVM.BienNhan = await _unitOfWork.bienNhanRepository.GetByIdAsync(bienNhanId);
            ChiTietBNVM.ChiTietBN.BienNhanId = bienNhanId;

            if (id == 0)
                return NotFound();

            ChiTietBNVM.ChiTietBN = _unitOfWork.chiTietBNRepository.GetById(id);

            if (ChiTietBNVM.ChiTietBN == null)
                return NotFound();

            return View(ChiTietBNVM);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(long id, string bienNhanId, string strUrl)
        {
            // from login session
            var user = HttpContext.Session.Gets<User>("loginUser").SingleOrDefault();

            string temp = "", log = "";

            if (id != ChiTietBNVM.ChiTietBN.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                ChiTietBNVM.ChiTietBN.NgaySua = DateTime.Now;
                ChiTietBNVM.ChiTietBN.NguoiSua = user.Username;
                // kiem tra thay doi : trong getbyid() va ngoai view
                #region log file
                //var t = _unitOfWork.tourRepository.GetById(id);
                var t = _unitOfWork.chiTietBNRepository.GetByIdAsNoTracking(x => x.Id == id);
                if (t.Descript != ChiTietBNVM.ChiTietBN.Descript)
                {
                    temp += String.Format("- Descript thay đổi: {0}->{1}", t.Descript, ChiTietBNVM.ChiTietBN.Descript);
                }
                
                if (t.Amount != ChiTietBNVM.ChiTietBN.Amount)
                {
                    temp += String.Format("- Tổng tiền thay đổi: {0:N0}->{1:N0}", t.Amount, ChiTietBNVM.ChiTietBN.Amount);
                }
                
                #endregion
                // kiem tra thay doi
                if (temp.Length > 0)
                {

                    log = System.Environment.NewLine;
                    log += "=============";
                    log += System.Environment.NewLine;
                    log += temp + " -User cập nhật tour: " + user.Username + " vào lúc: " + System.DateTime.Now.ToString(); // username
                    t.LogFile = t.LogFile + log;
                    ChiTietBNVM.ChiTietBN.LogFile = t.LogFile;
                }

                try
                {
                    _unitOfWork.chiTietBNRepository.Update(ChiTietBNVM.ChiTietBN);
                    await _unitOfWork.Complete();
                    SetAlert("Cập nhật thành công", "success");
                    return Redirect(ChiTietBNVM.StrUrl);
                }
                catch (Exception ex)
                {
                    SetAlert(ex.Message, "error");
                    ChiTietBNVM.StrUrl = strUrl;// + "&tabActive=" + tabActive; // for redirect tab
                    ChiTietBNVM.BienNhan = await _unitOfWork.bienNhanRepository.GetByIdAsync(bienNhanId);
                    ChiTietBNVM.ChiTietBN.BienNhanId = bienNhanId;
                    return View(ChiTietBNVM);
                }
            }
            // for not valid
            ChiTietBNVM.StrUrl = strUrl;// + "&tabActive=" + tabActive; // for redirect tab
            ChiTietBNVM.BienNhan = await _unitOfWork.bienNhanRepository.GetByIdAsync(bienNhanId);
            ChiTietBNVM.ChiTietBN.BienNhanId = bienNhanId;
            return View(ChiTietBNVM);
        }

        public async Task<IActionResult> Details(long id, string bienNhanId/*, string tabActive*/, string strUrl)
        {
            ChiTietBNVM.StrUrl = strUrl;// + "&tabActive=" + tabActive; // for redirect tab
            ChiTietBNVM.BienNhan = await _unitOfWork.bienNhanRepository.GetByIdAsync(bienNhanId);

            if (id == 0)
                return NotFound();

            var cTVAT = _unitOfWork.chiTietBNRepository.GetById(id);

            if (cTVAT == null)
                return NotFound();

            ChiTietBNVM.ChiTietBN = cTVAT;

            return View(ChiTietBNVM);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id, string bienNhanId, string strUrl/*, string tabActive*/)
        {
            ChiTietBNVM.StrUrl = strUrl;// + "&tabActive=" + tabActive; // for redirect tab

            var cTVAT = _unitOfWork.chiTietBNRepository.GetById(id);
            if (cTVAT == null)
                return NotFound();
            try
            {
                _unitOfWork.chiTietBNRepository.Delete(cTVAT);
                await _unitOfWork.Complete();
                SetAlert("Xóa thành công.", "success");
                return Redirect(ChiTietBNVM.StrUrl);
            }
            catch (Exception ex)
            {
                SetAlert(ex.Message, "error");
                return Redirect(ChiTietBNVM.StrUrl);
            }
        }
        // CTBienNhan
        

    }
}