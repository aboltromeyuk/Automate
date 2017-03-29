using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Automate.WEB.Models
{
    public class DrinksWithChange
    {
        public List<DrinkViewModel> Drinks { get; set; }
        public int SumOfChange { get; set; }
        public bool AllChange { get; set; }
        public List<CoinViewModel> Change { get; set; }
    }
}