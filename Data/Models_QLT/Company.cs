using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Data.Models_QLT
{
    public partial class Company
    {
        [DisplayName("ID")]
        public string CompanyId { get; set; }

        [DisplayName("Tên công ty")]
        [Required(ErrorMessage = "Tên không được để trống")]
        public string Name { get; set; }

        [DisplayName("Ten đầy đủ")]
        public string Fullname { get; set; }

        [DisplayName("Quốc gia")]
        public string Nation { get; set; }

        public string Fax { get; set; }
        public string Tel { get; set; }

        [DisplayName("Địa chỉ")]
        public string Address { get; set; }

        public DateTime? Contact { get; set; }

        [DisplayName("Tên tiếng anh")]
        public string Natione { get; set; }

        [DisplayName("Head office")]
        public string Headoffice { get; set; }

        [DisplayName("MST")]
        public string Msthue { get; set; }

        [DisplayName("Chi nhánh")]
        [Required(ErrorMessage = "Chi nhánh không được để trống")]
        public string Chinhanh { get; set; }
    }
}