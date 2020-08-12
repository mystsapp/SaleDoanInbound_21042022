using System;
using System.Collections.Generic;

namespace Data.Models_QLT
{
    public partial class Booked
    {
        public decimal Idbooking { get; set; }
        public string Sgtcode { get; set; }
        public int Times { get; set; }
        public string SupplierId { get; set; }
        public string Booking { get; set; }
        public DateTime? Date { get; set; }
        public string Note { get; set; }
        public string Profile { get; set; }
        public DateTime? ConfirmDate { get; set; }
        public string Name { get; set; }
        public string Logfile { get; set; }
    }
}
