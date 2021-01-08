using Data.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Services
{
    public interface IDSKhachHangService
    {

    }
    public class DSKhachHangService : IDSKhachHangService
    {
        private readonly IUnitOfWork _unitOfWork;

        public DSKhachHangService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
    }
}
