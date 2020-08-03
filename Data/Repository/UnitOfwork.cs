
using Data.Models;
using Data.Models_IB;
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
        IChiNhanhRepository chiNhanhRepository { get; }
        IDiemTQRepository diemTQRepository { get; }
        IThanhPhoRepository thanhPhoRepository { get; }
        IQuocGiaRepository quocGiaRepository { get; }
        ITourIBRepository tourIBRepository { get; }
        ICTVATRepository cTVATRepository { get; }
        IInvoiceRepository invoiceRepository { get; }
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
            chiNhanhRepository = new ChiNhanhRepository(_qltourContext);
            diemTQRepository = new DiemTQRepository(_qltourContext);
            thanhPhoRepository = new ThanhPhoRepository(_context);
            tourIBRepository = new TourIBRepository(_context);
            cTVATRepository = new CTVATRepository(_context);
            invoiceRepository = new InvoiceRepository(_context);
        }

        public IKhachHangRepository khachHangRepository { get; }

        public IQuocGiaRepository quocGiaRepository { get; }

        public IQuanRepository quanRepository { get; }

        public IDMNganhNgheRepository dMNganhNgheRepository { get; }

        public IKhuVucTGRepository khuVucTGRepository { get; }

        public ICacNoiDungHuyTourRepository cacNoiDungHuyTourRepository { get; }

        public ILoaiTourRepository loaiTourRepository { get; }

        public IChiNhanhRepository chiNhanhRepository { get; }

        public IDiemTQRepository diemTQRepository { get; }

        public IThanhPhoRepository thanhPhoRepository { get; }

        public ITourIBRepository tourIBRepository { get; }

        public ICTVATRepository cTVATRepository { get; }

        public IInvoiceRepository invoiceRepository { get; }

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
