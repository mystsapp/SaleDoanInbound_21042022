using Data.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Services
{
    public interface ITourleWIService
    {

    }
    public class TourleWIService : ITourleWIService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TourleWIService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
    }
}
