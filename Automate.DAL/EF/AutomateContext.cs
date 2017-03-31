using Automate.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Hosting;

namespace Automate.DAL.EF
{
    public class AutomateContext : DbContext
    {
        static AutomateContext()
        {
            Database.SetInitializer<AutomateContext>(new StoreDbInitializer());
        }

        public AutomateContext(string connectionString)
            : base(connectionString)
        {
        }

        /// <summary>
        /// Initialization of db if db does not exist
        /// and adding nophoto.png for drinks without img
        /// </summary>
        public class StoreDbInitializer : DropCreateDatabaseIfModelChanges<AutomateContext>
        {
            protected override void Seed(AutomateContext db)
            {
                db.Pictures.Add(new Picture {  Name="Нет изображения",
                    Image =Image.GetImg(HostingEnvironment.MapPath("~\\Files")+"\\nophoto.png") });   //adding nophoto.png (id=1) to table "Pictures"                 
            }
        }

        public DbSet<Drink> Drinks { get; set; }
        public DbSet<Coin> Coins { get; set; }
        public DbSet<Picture> Pictures { get; set; }
    }
}
