using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Data.Dtos
{
    public class SgtcodeString
    {
        [Key]
        public string Sgtcode { get; set; }
    }
}