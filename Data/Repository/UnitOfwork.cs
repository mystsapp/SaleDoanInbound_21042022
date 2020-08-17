
using Data.Models_IB;
using Data.Models_QLT;
using Data.Models_QLTaiKhoan;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        IKhachHangRepository khachHangRepository { get; }
        IQuanRepository quanRepository { get; }
        IDMNganhNgheRepository dMNganhNgheRepository { get; }
        IKhuVucTGRepository khuVucTGRepository { get; }
        ICacNoiDungHuyTourRepository cacNoiDungHuyTourRepository { get; }
        ILoaiTourRepository loaiTourRepository { get; }
        //IChiNhanhRepository chiNhanhRepository { get; }
        IDmChiNhanhRepository dmChiNhanhRepository { get; }
        IPhongBanRepository phongBanRepository { get; }
        IThanhPhoRepository thanhPhoRepository { get; }
        IQuocGiaRepository quocGiaRepository { get; }
        ITourRepository tourRepository { get; }
        IUserRepository userRepository { get; }
        IThanhPhoForTuyenTQRepository thanhPhoForTuyenTQRepository { get; }
        ITourKindRepository tourKindRepository { get; }
        ITourInfRepository tourInfRepository { get; }

        // Mr.Son Db
        ITourIBRepository tourIBRepository { get; }
        ICTVATRepository cTVATRepository { get; }
        IInvoiceRepository invoiceRepository { get; }
        IChiTietBNRepository chiTietBNRepository { get; }
        IBienNhanRepository bienNhanRepository { get; }
        Task<int> Complete();
    }
    public class UnitOfwork : IUnitOfWork
    {
        private readonly SaleDoanIBDbContext _context;
        private readonly qltourContext _qltourContext;
        private readonly qltaikhoanContext _qltaikhoanContext;

        public UnitOfwork(SaleDoanIBDbContext context, qltourContext qltourContext, qltaikhoanContext qltaikhoanContext)
        {
            _context = context;
            _qltourContext = qltourContext;
            _qltaikhoanContext = qltaikhoanContext;

            khachHangRepository = new KhachHangRepository(qltourContext);
            quocGiaRepository = new QuocGiaRepository(_qltaikhoanContext);
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
            thanhPhoForTuyenTQRepository = new ThanhPhoForTuyenTQRepository(_qltaikhoanContext);
            tourKindRepository = new TourKindRepository(_qltourContext);
            tourInfRepository = new TourInfRepository(_qltourContext);

            // Mr.Son
            tourIBRepository = new TourIBRepository(_context);
            cTVATRepository = new CTVATRepository(_context);
            invoiceRepository = new InvoiceRepository(_context);
            chiTietBNRepository = new ChiTietBNRepository(_context);
            bienNhanRepository = new BienNhanRepository(_context);
        }

        public IKhachHangRepository khachHangRepository { get; }

        public IQuocGiaRepository quocGiaRepository { get; }

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

        public IThanhPhoForTuyenTQRepository thanhPhoForTuyenTQRepository { get; }

        public ITourKindRepository tourKindRepository { get; }

        public IDmChiNhanhRepository dmChiNhanhRepository { get; }

        public IPhongBanRepository phongBanRepository { get; }

        public ITourInfRepository tourInfRepository { get; }

        public IUserRepository userRepository { get; }

        public async Task<int> Complete()
        {
            await _context.SaveChangesAsync();
            await _qltourContext.SaveChangesAsync();
            await _qltaikhoanContext.SaveChangesAsync();
            return 1;
        }

        public void Dispose()
        {
            _context.Dispose();
            GC.Collect();
        }
    }
}
