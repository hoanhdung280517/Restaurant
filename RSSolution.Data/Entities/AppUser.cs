using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RSSolution.Data.Entities
{
    public class AppUser : IdentityUser<Guid>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime Dob { get; set; }

        public List<Cart> Carts { get; set; }

        public List<Order> Orders { get; set; }

        public List<Transaction> Transactions { get; set; }
        public int ProvinceId { get; set; }
        public  Province Province { get; set; }
        public int DistrictId { get; set; }
        public  District District { get; set; }
        public List<BookTable> bookTables { get; set; }
    }
}
