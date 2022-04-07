using Data.Interfaces;
using Data.Models_Tourlewi;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.Repository
{
    public interface ITourWIRepository : IRepository<Models_Tourlewi.Tour>
    {
        string lastCode(DateTime batdau, string chinhanh, string macode);
    }

    public class TourWIRepository : Repository_TourleWI<Models_Tourlewi.Tour>, ITourWIRepository
    {
        public TourWIRepository(tourlewiContext context) : base(context)
        {
        }

        public string lastCode(DateTime batdau, string chinhanh, string macode)
        {
            try
            {
                //switch (chinhanh)
                //{
                //    case "STS":
                //        chinhanh = "SGT";
                //        break;
                //    default:
                //        chinhanh = "SGT";
                //        break;
                //}
                //var tourInfs = _context.Tourinf.Where(x => x.Sgtcode.Substring(0, 12) == chinhanh + macode + "-" + batdau.Year.ToString() + "-");

                var newSgtcode = chinhanh + macode + "-" + batdau.Year.ToString() + "-";
                var parammeter = new SqlParameter[]
                  {
                      new SqlParameter("@code",newSgtcode)
                  };
                //var tourInfs = _context.Tourinf.FromSqlRaw("newSgtcode @code", parammeter).FirstOrDefault();
                string code = _context.SgtcodeStrings.FromSqlRaw("newSgtcode @code", parammeter).AsEnumerable().FirstOrDefault().Sgtcode;// FirstOrDefault().Sgtcode;

                return code;

                //if (tourInfs.Count() != 0)
                //{
                //    var code = tourInfs.Take(1).OrderByDescending(x => x.Sgtcode).FirstOrDefault().Sgtcode;
                //    return code;
                //}
                //return "";
            }
            catch
            {
                return "";
            }
        }
    }
}