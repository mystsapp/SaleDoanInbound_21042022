using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Data.Dtos
{
    public class BienNhanDto
    {
        public long Id { get; set; }

        [DisplayName("Số biên nhận")]
        public string SoBN { get; set; } // biennhan in Mr.Son db

        [DisplayName("Tour")]
        public long TourId { get; set; }// relation --> foreignkey to tourIB
        public string Sgtcode { get; set; }

        [DisplayName("Ngày BN")]
        [Required(ErrorMessage = "Ngày không được để trống")]
        public DateTime NgayBN { get; set; }

        [DisplayName("Tên khách hàng")]
        public string TenKhach { get; set; }

        public int SK { get; set; }

        [DisplayName("Ghi chú")]
        public string GhiChu { get; set; }

        [DisplayName("Loại tiền")]
        [Required(ErrorMessage = "Loại tiền không được để trống")]
        public string LoaiTien { get; set; }

        [DisplayName("Tỷ giá")]
        [Required(ErrorMessage = "Tỷ giá không được để trống")]
        public decimal TyGia { get; set; }

        [DisplayName("Ngày hủy")]
        public DateTime? NgayHuy { get; set; }

        [DisplayName("Hủy BN")]
        public bool? HuyBN { get; set; }

        [DisplayName("Nội dung hủy")]
        public string NoiDungHuy { get; set; }

        [DisplayName("Khách lẽ")]
        public bool KhachLe { get; set; }

        [DisplayName("Địa chỉ")]
        public string DiaChi { get; set; }

        [DisplayName("Điện thoại")]
        public string DienThoai { get; set; }

        [DisplayName("Nội dung")]
        public string NoiDung { get; set; }

        [DisplayName("Số tiền")]
        public decimal SoTien { get; set; }

        [DisplayName("Ngày tạo")]
        public DateTime NgayTao { get; set; }

        [DisplayName("Người tạo")]
        public string NguoiTao { get; set; }

        public DateTime NgaySua { get; set; }

        [DisplayName("Người sửa")]
        public string NguoiSua { get; set; }

        public string LogFile { get; set; }

        public string TrangThai { get; set; }
    }
}
