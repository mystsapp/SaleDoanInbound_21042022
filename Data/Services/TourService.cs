using Data.Repository;
using Data.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.Services
{
    public interface ITourService
    {
        string newSgtcode(System.DateTime batdau, string chinhanh, string macode);
        string newSgtcodeKDOB(DateTime dateTime, string maCN, string telcode);
    }
    public class TourService : ITourService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TourService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        GenerateId generateId = new GenerateId();

        public string newSgtcodeKDOB(DateTime batdau, string chinhanh, string macode)
        {

            var newCode = generateId.NextId(lastCode(batdau, chinhanh, macode), chinhanh + macode + "-" + batdau.Year.ToString() + "-", "00001");
            return newCode;
        }
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
                
                if (macode == "000")
                {
                    var tourwi = _unitOfWork.tourWIRepository.Find(x => x.Sgtcode.Substring(0, 12) == chinhanh + macode + "-" + batdau.Year.ToString() + "-").OrderByDescending(x => x.Sgtcode).FirstOrDefault();
                    if (tourwi != null)
                    {
                        var code = tourwi.Sgtcode;
                        return code;
                    }
                    else
                    {
                        return "";
                    }
                }
                else
                {
                    var tourinf = _unitOfWork.tourInfRepository.Find(x => x.Sgtcode.Substring(0, 12) == chinhanh + macode + "-" + batdau.Year.ToString() + "-").OrderByDescending(x => x.Sgtcode).FirstOrDefault();
                    if (tourinf != null)
                    {
                        var code = tourinf.Sgtcode;
                        return code;
                    }
                    else
                    {
                        return "";
                    }
                }

            }
            catch
            {
                return "";
            }
        }

    }
}
