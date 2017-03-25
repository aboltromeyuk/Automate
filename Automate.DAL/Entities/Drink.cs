using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automate.DAL.Entities
{
    public class Drink
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string PictureUrl { get; set; }
        public int Number { get; set; }
        public int Price { get; set; }

    }
}
