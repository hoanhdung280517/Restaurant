using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSSolution.Data.Entities
{
    public class Province
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public virtual ICollection<AppUser> CUsers { get; set; }
    }
}
