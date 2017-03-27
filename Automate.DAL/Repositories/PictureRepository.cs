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
    public class PictureRepository : IRepository<Picture>
    {
        private AutomateContext db;

        public PictureRepository(AutomateContext context)
        {
            this.db = context;
        }

        public void Create(Picture picture)
        {
            db.Pictures.Add(picture);
        }

        public void Delete(int id)
        {
            Picture picture = db.Pictures.Find(id);

            if (picture != null)
                db.Pictures.Remove(picture);
        }

        public IEnumerable<Picture> Find(Func<Picture, bool> predicate)
        {
            return db.Pictures.Where(predicate).ToList();
        }

        public Picture Get(int id)
        {
            return db.Pictures.Find(id);
        }

        public IEnumerable<Picture> GetAll()
        {
            return db.Pictures;
        }

        public void Update(Picture picture)
        {
            db.Entry(picture).State = EntityState.Modified;
        }
    }
}
