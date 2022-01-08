﻿using RSSolution.Data.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace RSSolution.Data.Entities
{
    public class Contact
    {
        public int Id { set; get; }
        public string Name { set; get; }
        public string Email { set; get; }
        public int PhoneNumber { set; get; }
        public string Message { set; get; }
        public DateTime ContactDate { get; set; }

    }
}
