using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp3
{
    public class Adress
    {
        public int Id { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
