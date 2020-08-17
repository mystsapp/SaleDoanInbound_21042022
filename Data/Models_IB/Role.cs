using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models_IB
{
    public class Role
    {
        public int Id { get; set; }

        [DisplayName("Role name")]
        [MaxLength(50), Column(TypeName = "nvarchar(50)")]
        public string RoleName { get; set; }

        [DisplayName("Miêu tả")]
        [MaxLength(250), Column(TypeName = "nvarchar(250)")]
        public string Description { get; set; }

    }
}