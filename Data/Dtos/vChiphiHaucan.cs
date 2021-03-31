using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Dtos
{
    [Table("vChiphiHaucan")]
    public class vChiphiHaucan
    {
        [Key]
        public decimal Id { get; set; } // Id Hậu cần
        public decimal IdTrahc { get; set; } // Id trả hậu cần
        public string Mahh { get; set; }
        public string Tenhh { get; set; }
        public string Sgtcode { get; set; }
        public string Maphieudx { get; set; }
        public string Maphieutra { get; set; }
        public int Soluong { get; set; }
        public int Soluongtra { get; set; }
        public string Donvitinh { get; set; }
        public decimal Dongia { get; set; }
        public DateTime? Ngayyeucau { get; set; }
        public string Nguoiyeucau { get; set; }
        public string Ghichutra { get; set; }
        public bool Daxuat { get; set; }
        public bool Danhap { get; set; }
        public string Chinhanh { get; set; }

    }
}
