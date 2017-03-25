using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automate.DAL.Entities
{
    public class Coin
    {
        [Key]
        public int Id { get; set; }
        public int Nominal { get; set; }
        public int Number { get; set; }
        public bool Blocked { get; set; }

    }
}
