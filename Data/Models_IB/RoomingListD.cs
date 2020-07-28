using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models_IB
{
    public class RoomingListD
    {
        public long Id { get; set; }

        [DisplayName("Số phòng")]
        [MaxLength(50), Column(TypeName = "nvarchar(50)")]
        public string SoPhong { get; set; }

        [DisplayName("Tên khách")]
        [MaxLength(50), Column(TypeName = "nvarchar(50)")]
        public string KhachTour { get; set; }

        [DisplayName("Loại phòng")]
        [MaxLength(100), Column(TypeName = "nvarchar(100)")]
        public string LoaiPhong { get; set; }

        [DisplayName("RoomingList")]
        public long IdRoomingList { get; set; }

        [ForeignKey("IdRoomingList")]
        public virtual RoomingList RoomingList { get; set; }
    }
}