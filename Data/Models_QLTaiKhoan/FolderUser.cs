﻿using System;
using System.Collections.Generic;

namespace Data.Models_QLTaiKhoan
{
    public partial class FolderUser
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public string Description { get; set; }
        public string UserId { get; set; }
        public bool? Upload { get; set; }
    }
}
