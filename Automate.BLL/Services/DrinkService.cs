using AutoMapper;
using Automate.BLL.DTO;
using Automate.BLL.Interfaces;
using Automate.DAL.Entities;
using Automate.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automate.BLL.Services
{
    public class DrinkService : IDrinkService
    {
        IUnitOfWork Database { get; set; }

        public DrinkService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public void Create(DrinkDTO drink)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<DrinkDTO, Drink>());
            Database.Drinks.Create(Mapper.Map<DrinkDTO, Drink>(drink));
        }

        public void Delete(int id)
        {
            Database.Drinks.Delete(id);
        }

        public void Dispose()
        {
            Database.Dispose();
        }

        public DrinkDTO GetDrink(int id)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<Drink, DrinkDTO>());
            return Mapper.Map<Drink, DrinkDTO>(Database.Drinks.Get(id));
        }

        public IEnumerable<DrinkDTO> GetDrinks()
        {
            Mapper.Initialize(cfg => cfg.CreateMap<Drink, DrinkDTO>());
            return Mapper.Map<IEnumerable<Drink>, List<DrinkDTO>>(Database.Drinks.GetAll());
        }

        public void Update(DrinkDTO drink)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<DrinkDTO, Drink>());
            Database.Drinks.Update(Mapper.Map<DrinkDTO, Drink>(drink));
        }

        public void TakeDrinks(IEnumerable<DrinkDTO> drinks)
        {
            var userDrinks = new List<DrinkDTO>();

            foreach(var drink in drinks.Distinct())
            {
                drink.Number -= drinks.Where(x => x.Equals(drink)).Count();
                userDrinks.Add(drink);
            }

            foreach (var drink in userDrinks)
            {
                this.Update(drink);
            }
        }
    }
}
