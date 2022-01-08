using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSSolution.ViewModels.Catalog.Contacts
{
    public class ContactTimeRequest
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public int PhoneNumber { get; set; }
        public string Message { get; set; }
        public DateTime ContactDate { get; set; }
    }
}
