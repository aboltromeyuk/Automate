using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automate.BLL.DTO
{
    public class DrinkDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int PictureId { get; set; }
        public int Number { get; set; }
        public int Price { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            DrinkDTO drink = obj as DrinkDTO;
            if (drink as DrinkDTO == null)
                return false;
            return drink.Id == this.Id;
        }

        public bool Equals(DrinkDTO obj)
        {
            if (obj == null)
                return false;
            return obj.Id == this.Id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
