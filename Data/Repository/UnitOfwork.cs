
using Data.Models_HDDT;
using Data.Models_HDVATOB;
using Data.Models_IB;
using Data.Models_QLT;
using Data.Models_QLTaiKhoan;
using Data.Models_Tourlewi;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        IKhachHangRepository khachHangRepository { get; }
        IDSKhachHangRepository dSKhachHangRepository { get; }
        IQuanRepository quanRepository { get; }
        IDMNganhNgheRepository dMNganhNgheRepository { get; }
        IKhuVucTGRepository khuVucTGRepository { get; }
        ICacNoiDungHuyTourRepository cacNoiDungHuyTourRepository { get; }
        ILoaiTourRepository loaiTourRepository { get; }
        //IChiNhanhRepository chiNhanhRepository { get; }
        IDmChiNhanhRepository dmChiNhanhRepository { get; }
        IPhongBanRepository phongBanRepository { get; }
        IThanhPhoRepository thanhPhoRepository { get; }
        ITourRepository tourRepository { get; }
        IUserRepository userRepository { get; }
        IRoleRepository roleRepository { get; }
        ILoaiIVRepository loaiIVRepository { get; }
        IPhanKhuCNRepository phanKhuCNRepository { get; }

        // qltour
        IThanhPhoForTuyenTQRepository thanhPhoForTuyenTQRepository { get; }
        ITourKindRepository tourKindRepository { get; }
        ITourInfRepository tourInfRepository { get; }
        INgoaiTeRepository ngoaiTeRepository { get; }
        IKhachTourRepository khachTourRepository { get; }
        ITourprogRepository tourproRepository { get; }
        ISupplierRepository supplierRepository { get; }
        IHotelRepository hotelRepository { get; }
        ISightseeingRepository sightseeingRepository { get; }
        IDMDiemTQRepository dMDiemTQRepository { get; }
        ITournoteRepository tournoteRepository { get; }
        IChiPhiKhacRepository chiPhiKhacRepository { get; }
        IDichVuRepository dichVuRepository { get; }
        IDieuXeRepository dieuXeRepository { get; }
        IHuongDanRepository huongDanRepository { get; }
        IUserQLTourRepository userQLTourRepository { get; }
        ITraHauCanRepository traHauCanRepository { get; }
        IThanhPho_QLT_Repository thanhPho_QLT_Repository { get; }

        // qltaikhoan
        IUserQLTaiKhoanRepository userQLTaiKhoanRepository { get; }
        IQuocGiaRepository quocGiaRepository { get; }
        IApplicationUserQLTaiKhoanRepository applicationUserQLTaiKhoanRepository { get; }
        IApplicationQLTaiKhoanRepository applicationQLTaiKhoanRepository { get; }

        // Mr.Son Db
        ITourIBRepository tourIBRepository { get; }
        ICTVATRepository cTVATRepository { get; }
        IInvoiceRepository invoiceRepository { get; }
        IChiTietBNRepository chiTietBNRepository { get; }
        IBienNhanRepository bienNhanRepository { get; }

        // HDDT
        IDSDangKyHDRepository dSDangKyHDRepository { get; }
        IdsChiNhanh_HDDTRepository dsChiNhanh_HDDTRepository { get; }

        // HDVATOB
        IUserHDVATOBRepository userHDVATOBRepository { get; }

        // TourleWI
        ITourWIRepository tourWIRepository { get; }

        Task<int> Complete();
    }
    public class UnitOfwork : IUnitOfWork
    {
        private readonly SaleDoanIBDbContext _context;
        private readonly qltourContext _qltourContext;
        private readonly qltaikhoanContext _qltaikhoanContext;
        private readonly hoadondientuContext _hoadondientuContext;
        private readonly tourlewiContext _tourlewiContext;
        private readonly hdvatobContext _hdvatobContext;

        public UnitOfwork(SaleDoanIBDbContext context, 
                          qltourContext qltourContext, 
                          qltaikhoanContext qltaikhoanContext, 
                          hoadondientuContext hoadondientuContext,
                          tourlewiContext tourlewiContext,
                          hdvatobContext hdvatobContext)
        {
            _context = context;
            _qltourContext = qltourContext;
            _qltaikhoanContext = qltaikhoanContext;
            _hoadondientuContext = hoadondientuContext;
            _tourlewiContext = tourlewiContext;
            _hdvatobContext = hdvatobContext;

            khachHangRepository = new KhachHangRepository(qltourContext);
            quanRepository = new QuanRepository(_context);
            dMNganhNgheRepository = new DMNganhNgheRepository(_context);
            khuVucTGRepository = new KhuVucTGRepository(_context);
            cacNoiDungHuyTourRepository = new CacNoiDungHuyTourRepository(_context);
            loaiTourRepository = new LoaiTourRepository(_context);
            //chiNhanhRepository = new ChiNhanhRepository(_qltourContext);
            dmChiNhanhRepository = new DmChiNhanhRepository(_qltourContext);
            phongBanRepository = new PhongBanRepository(_qltourContext);
            thanhPhoRepository = new ThanhPhoRepository(_context);
            tourRepository = new TourRepository(_context);
            userRepository = new UserRepository(_context);
            roleRepository = new RoleRepository(_context);
            dSKhachHangRepository = new DSKhachHangRepository(_context);
            loaiIVRepository = new LoaiIVRepository(_context);
            phanKhuCNRepository = new PhanKhuCNRepository(_context);

            // qltour
            thanhPhoForTuyenTQRepository = new ThanhPhoForTuyenTQRepository(_qltaikhoanContext);
            tourKindRepository = new TourKindRepository(_qltourContext);
            tourInfRepository = new TourInfRepository(_qltourContext);
            ngoaiTeRepository = new NgoaiTeRepository(_qltourContext);
            khachTourRepository = new KhachTourRepository(_qltourContext);
            tourproRepository = new TourprogRepository(_qltourContext);
            supplierRepository = new SupplierRepository(_qltourContext);
            hotelRepository = new HotelRepository(_qltourContext);
            sightseeingRepository = new SightseeingRepository(_qltourContext);
            dMDiemTQRepository = new DMDiemTQRepository(_qltourContext);
            tournoteRepository = new TournoteRepository(_qltourContext);
            chiPhiKhacRepository = new ChiPhiKhacRepository(_qltourContext);
            dichVuRepository = new DichVuRepository(_qltourContext);
            dieuXeRepository = new DieuXeRepository(_qltourContext);
            huongDanRepository = new HuongDanRepository(_qltourContext);
            quocGiaRepository = new QuocGiaRepository(_qltourContext);
            userQLTourRepository = new UserQLTourRepository(_qltourContext);
            traHauCanRepository = new TraHauCanRepository(_qltourContext);
            thanhPho_QLT_Repository = new ThanhPho_QLT_Repository(_qltourContext);

            // qltaikhoan

            userQLTaiKhoanRepository = new UserQLTaiKhoanRepository(_qltaikhoanContext);
            applicationUserQLTaiKhoanRepository = new ApplicationUserQLTaiKhoanRepository(_qltaikhoanContext);
            applicationQLTaiKhoanRepository = new ApplicationQLTaiKhoanRepository(_qltaikhoanContext);

            // Mr.Son
            tourIBRepository = new TourIBRepository(_context);
            cTVATRepository = new CTVATRepository(_context);
            invoiceRepository = new InvoiceRepository(_context);
            chiTietBNRepository = new ChiTietBNRepository(_context);
            bienNhanRepository = new BienNhanRepository(_context);

            // HDDT
            dSDangKyHDRepository = new DSDangKyHDRepository(_hoadondientuContext);
            dsChiNhanh_HDDTRepository = new dsChiNhanh_HDDTRepository(_hoadondientuContext);

            // HDVATOB
            userHDVATOBRepository = new UserHDVATOBRepository(_hdvatobContext);

            // TourleWI
            tourWIRepository = new TourWIRepository(_tourlewiContext);
        }

        public IKhachHangRepository khachHangRepository { get; }

        public IQuanRepository quanRepository { get; }

        public IDMNganhNgheRepository dMNganhNgheRepository { get; }

        public IKhuVucTGRepository khuVucTGRepository { get; }

        public ICacNoiDungHuyTourRepository cacNoiDungHuyTourRepository { get; }

        public ILoaiTourRepository loaiTourRepository { get; }

        //public IChiNhanhRepository chiNhanhRepository { get; }


        public IThanhPhoRepository thanhPhoRepository { get; }

        public ITourIBRepository tourIBRepository { get; }

        public ICTVATRepository cTVATRepository { get; }

        public IInvoiceRepository invoiceRepository { get; }

        public IChiTietBNRepository chiTietBNRepository { get; }

        public IBienNhanRepository bienNhanRepository { get; }

        public ITourRepository tourRepository { get; }


        public ITourKindRepository tourKindRepository { get; }

        public IDmChiNhanhRepository dmChiNhanhRepository { get; }

        public IPhongBanRepository phongBanRepository { get; }


        public IUserRepository userRepository { get; }

        public IRoleRepository roleRepository { get; }

        public IDSKhachHangRepository dSKhachHangRepository { get; }
        public ILoaiIVRepository loaiIVRepository { get; }
        public IPhanKhuCNRepository phanKhuCNRepository { get; }

        // qltour
        public IThanhPhoForTuyenTQRepository thanhPhoForTuyenTQRepository { get; }
        public ITourInfRepository tourInfRepository { get; }
        public INgoaiTeRepository ngoaiTeRepository { get; }
        public IKhachTourRepository khachTourRepository { get; }
        public ITourprogRepository tourproRepository { get; }
        public ISupplierRepository supplierRepository { get; }
        public IHotelRepository hotelRepository { get; }
        public ISightseeingRepository sightseeingRepository { get; }
        public IDMDiemTQRepository dMDiemTQRepository { get; }
        public ITournoteRepository tournoteRepository { get; }
        public IChiPhiKhacRepository chiPhiKhacRepository { get; }
        public IDichVuRepository dichVuRepository { get; }
        public IDieuXeRepository dieuXeRepository { get; }
        public IHuongDanRepository huongDanRepository { get; }
        public IUserQLTourRepository userQLTourRepository { get; }
        public ITraHauCanRepository traHauCanRepository { get; }
        public IThanhPho_QLT_Repository thanhPho_QLT_Repository { get; }
        // qltaikhoan
        public IUserQLTaiKhoanRepository userQLTaiKhoanRepository { get; }
        public IQuocGiaRepository quocGiaRepository { get; }
        public IApplicationUserQLTaiKhoanRepository applicationUserQLTaiKhoanRepository { get; }

        public IApplicationQLTaiKhoanRepository applicationQLTaiKhoanRepository { get; }

        // HDDT
        public IDSDangKyHDRepository dSDangKyHDRepository { get; }
        public IdsChiNhanh_HDDTRepository dsChiNhanh_HDDTRepository { get; }

        // TourleWI
        public ITourWIRepository tourWIRepository { get; }

        // HDVATOB
        public IUserHDVATOBRepository userHDVATOBRepository { get; }

        public async Task<int> Complete()
        {
            await _context.SaveChangesAsync();
            await _qltourContext.SaveChangesAsync();
            await _qltaikhoanContext.SaveChangesAsync();
            await _hoadondientuContext.SaveChangesAsync();
            await _tourlewiContext.SaveChangesAsync();
            return 1;
        }

        public void Dispose()
        {
            _context.Dispose();
            GC.Collect();
        }
    }
}
