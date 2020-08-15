using Data.Interfaces;
using Data.Models_IB;
using Data.Models_QLT;
using Data.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.Repository
{
    public interface ITourInfRepository : IRepository<Tourinf> // qltour
    {
        string newSgtcode(System.DateTime batdau, string chinhanh, string macode);
    }
    public class TourInfRepository : Repository_QLT<Tourinf>, ITourInfRepository
    {
        public TourInfRepository(qltourContext context) : base(context)
        {
        }

        GenerateId generateId = new GenerateId();

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
            var newCode = generateId.NextId(lastCode(batdau, chinhanh, macode), chinhanh + macode + "-" + batdau.Year.ToString() + "-", "00001");
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
                var tourInfs = _context.Tourinf.Where(x => x.Sgtcode.Substring(0, 12) == chinhanh + macode + "-" + batdau.Year.ToString() + "-");
                if (tourInfs.Count() != 0)
                {
                    var code = tourInfs.Take(1).OrderByDescending(x => x.Sgtcode).FirstOrDefault().Sgtcode;
                    return code;
                }
                return "";
            }
            catch
            {
                return "";
            }
        }
    }
}
