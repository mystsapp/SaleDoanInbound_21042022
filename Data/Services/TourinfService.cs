using Data.Models_Tourlewi;
using Data.Repository;
using Data.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.Services
{

    public interface ITourinfService
    {
    }
    public class TourinfService : ITourinfService
    {
        private IUnitOfWork _unitOfWork;

        public TourinfService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

    }
}
