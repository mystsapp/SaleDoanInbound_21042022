﻿using Data.Models_QLTaiKhoan;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Repository
{
    public interface IThanhPhoForTuyenTQRepository
    {
        IEnumerable<Thanhpho> GetAll();

        Task<Thanhpho> GetByIdAsync(string id);

        IEnumerable<Thanhpho> Find(Func<Thanhpho, bool> predicate);
        IEnumerable<Thanhpho1> ListThanhpho1(Func<Thanhpho1, bool> predicate);

    }

    public class ThanhPhoForTuyenTQRepository : IThanhPhoForTuyenTQRepository
    {
        private readonly qltaikhoanContext _qltaikhoanContext;

        public ThanhPhoForTuyenTQRepository(qltaikhoanContext qltaikhoanContext)
        {
            _qltaikhoanContext = qltaikhoanContext;
        }
        public IEnumerable<Thanhpho> Find(Func<Thanhpho, bool> predicate)
        {
            return _qltaikhoanContext.Thanhpho.Where(predicate);
        }
        public IEnumerable<Thanhpho1> ListThanhpho1(Func<Thanhpho1, bool> predicate)
        {
            return _qltaikhoanContext.Thanhpho1.Where(predicate);
        }

        public IEnumerable<Thanhpho> GetAll()
        {
            return _qltaikhoanContext.Thanhpho;
        }

        public async Task<Thanhpho> GetByIdAsync(string id)
        {
            return await _qltaikhoanContext.Thanhpho.FindAsync(id);
        }

    }
}