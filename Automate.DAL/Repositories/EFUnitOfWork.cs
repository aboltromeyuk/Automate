﻿using Automate.DAL.EF;
using Automate.DAL.Entities;
using Automate.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automate.DAL.Repositories
{
    /// <summary>
    /// Class for access to db 
    /// (have constructor who take string of name connect to db)
    /// </summary>
    public class EFUnitOfWork : IUnitOfWork
    {
        private AutomateContext db;
        private DrinkRepository drinkRepository;
        private CoinRepository coinRepository;
        private PictureRepository pictureRepository;

        public EFUnitOfWork(string conectionString)
        {
            db = new AutomateContext(conectionString);
        }

        public IRepository<Coin> Coins
        {
            get
            {
                if (coinRepository == null)
                    coinRepository = new CoinRepository(db);
                return coinRepository;
            }
        }

        public IRepository<Drink> Drinks
        {
            get
            {
                if (drinkRepository == null)
                    drinkRepository = new DrinkRepository(db);
                return drinkRepository;
            }
        }

        public IRepository<Picture> Pictures
        {
            get
            {
                if (pictureRepository == null)
                    pictureRepository = new PictureRepository(db);
                return pictureRepository;
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }

        private bool disposed = false;

        public void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
