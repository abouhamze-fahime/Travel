using System;
using System.Collections.Generic;

namespace Travel.Models.Travel
{
    public partial class CustomerTb
    {
        public int CustomerId { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? Country { get; set; }
        public string? City { get; set; }
        public string? Phoneno { get; set; }
    }
}
