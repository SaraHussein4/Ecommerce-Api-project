﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Core.Identity
{
    public class Address
    {
        public int id { get; set; }
        public string FirstName  { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Street { get; set; }

        [ForeignKey("User")]
        public string AppUserId { get; set; }

        public AppUser User { get; set; }
    }
}
