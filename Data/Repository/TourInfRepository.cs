using Data.Interfaces;
using Data.Models_IB;
using Data.Models_QLT;
using Data.Utilities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.Repository
{
    public interface ITourInfRepository : IRepository<Tourinf> // qltour
    {
        string newSgtcode(System.DateTime batdau, string chinhanh, string macode);

        string lastCode(DateTime batdau, string chinhanh, string macode);
    }

    public class TourInfRepository : Repository_QLT<Tourinf>, ITourInfRepository
    {
        public TourInfRepository(qltourContext context) : base(context)
        {
        }

        private GenerateId generateId = new GenerateId();

        public string newSgtcode(DateTime batdau, string chinhanh, string macode)
        {
            switch (chinhanh)
            {
                case "STS":
                    chinhanh = "SGT";
                    break;

                default:
                    break;
            }
            //var newCode = generateId.NextId(lastCode(batdau, chinhanh, macode), chinhanh + macode + "-" + batdau.Year.ToString() + "-", "00001");
            var newCode = lastCode(batdau, chinhanh, macode);// generateId.NextId(lastCode(batdau, chinhanh, macode), chinhanh + macode + "-" + batdau.Year.ToString() + "-", "00001");
            return newCode;
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
                string code = _context.SgtcodeString.FromSqlRaw("newSgtcode @code", parammeter).AsEnumerable().FirstOrDefault().Sgtcode;// FirstOrDefault().Sgtcode;

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