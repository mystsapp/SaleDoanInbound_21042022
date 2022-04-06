using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Data.Dtos
{
    public class UserInfo
    {
        [Key]
        public string username { get; set; }

        public string hoten { get; set; }
        public string dienthoai { get; set; }
        public string email { get; set; }
        public string tenphong { get; set; }
        public string macn { get; set; }
        public string roleId { get; set; }
    }
}