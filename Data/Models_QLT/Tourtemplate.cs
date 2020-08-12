using System;
using System.Collections.Generic;

namespace Data.Models_QLT
{
    public partial class Tourtemplate
    {
        public Tourtemplate()
        {
            Tourprogtemp = new HashSet<Tourprogtemp>();
        }

        public string Code { get; set; }
        public string Tourkind { get; set; }
        public string Tentour { get; set; }
        public string Tuyentq { get; set; }
        public string Chudetour { get; set; }
        public int Songay { get; set; }
        public string Chinhanh { get; set; }
        public string Nguoitao { get; set; }

        public virtual ICollection<Tourprogtemp> Tourprogtemp { get; set; }
    }
}
