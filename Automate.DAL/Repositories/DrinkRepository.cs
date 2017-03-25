using Automate.DAL.EF;
using Automate.DAL.Entities;
using Automate.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automate.DAL.Repositories
{
    public class DrinkRepository : IRepository<Drink>
    {
        private AutomateContext db;

        public DrinkRepository(AutomateContext context)
        {
            this.db = context;
        }

        public void Create(Drink drink)
        {
            db.Drinks.Add(drink);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            Drink drink = db.Drinks.Find(id);

            if (drink != null)
                db.Drinks.Remove(drink);
        }

        public IEnumerable<Drink> Find(Func<Drink, bool> predicate)
        {
            return db.Drinks.Where(predicate).ToList();
        }

        public Drink Get(int id)
        {
            return db.Drinks.Find(id);
        }

        public IEnumerable<Drink> GetAll()
        {
            return db.Drinks;
        }

        public void Update(Drink drink)
        {
            db.Entry(drink).State = EntityState.Modified;
        }
    }
}
