using Automate.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automate.BLL.Interfaces
{
    public interface IDrinkService
    {
        DrinkDTO GetDrink(int id);
        IEnumerable<DrinkDTO> GetDrinks();
        void Create(DrinkDTO drink);
        void Delete(int id);
        void Update(DrinkDTO drink);
        void TakeDrinks(IEnumerable<DrinkDTO> drinks);
        void Dispose();
    }
}
