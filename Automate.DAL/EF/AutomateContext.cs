using Automate.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automate.DAL.EF
{
    public class AutomateContext : DbContext
    {
        public AutomateContext(string connectionString)
            : base(connectionString)
        {
        }

        public DbSet<Drink> Drinks { get; set; }
        public DbSet<Coin> Coins { get; set; }
    }

}
