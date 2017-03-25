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
    public class CoinRepository : IRepository<Coin>
    {
        private AutomateContext db;

        public CoinRepository(AutomateContext context)
        {
            this.db = context;
        }

        public void Create(Coin coin)
        {
            db.Coins.Add(coin);
        }

        public void Delete(int id)
        {
            Coin coin = db.Coins.Find(id);
            if (coin != null)
                db.Coins.Remove(coin);
        }

        public IEnumerable<Coin> Find(Func<Coin, bool> predicate)
        {
            return db.Coins.Where(predicate).ToList();
        }

        public Coin Get(int id)
        {
            return db.Coins.Find(id);
        }

        public IEnumerable<Coin> GetAll()
        {
            return db.Coins;
        }

        public void Update(Coin coin)
        {
            db.Entry(coin).State = EntityState.Modified;
        }
    }
}
