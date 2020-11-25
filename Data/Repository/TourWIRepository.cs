using Data.Interfaces;
using Data.Models_Tourlewi;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repository
{
    public interface ITourWIRepository : IRepository<Models_Tourlewi.Tour>
    {

    }
    public class TourWIRepository : Repository_TourleWI<Models_Tourlewi.Tour>, ITourWIRepository
    {
        public TourWIRepository(tourlewiContext context) : base(context)
        {
        }
    }

}
