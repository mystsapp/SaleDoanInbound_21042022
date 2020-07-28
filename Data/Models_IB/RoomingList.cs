using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models_IB
{
    public class RoomingList
    {
        public long Id { get; set; }

        [DisplayName("Tour")]
        public long IdTour { get; set; }

        [ForeignKey("IdTour")]
        public virtual Tour Tour { get; set; }

        [DisplayName("Tên khách sạn")]
        [MaxLength(100), Column(TypeName = "nvarchar(100)")]
        public string TenKhachSan { get; set; }

        [DisplayName("Ngày checkin")]
        public DateTime NgayCheckIn { get; set; }

        [DisplayName("Ngày checkout")]
        public DateTime NgayCheckOut { get; set; }
    }
}