using Data.Models_IB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SaleDoanInbound.Models
{
    public class RoleViewModel
    {
        public IEnumerable<Role> Roles { get; set; }
        public Role Role { get; set; }
        public string StrUrl { get; set; }
    }
}
