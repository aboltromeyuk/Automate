using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Automate.WEB.Models
{
    public class CoinViewModel
    {
        public int Id { get; set; }
        public int Nominal { get; set; }
        public int Number { get; set; }
        public bool Blocked { get; set; }
    }
}