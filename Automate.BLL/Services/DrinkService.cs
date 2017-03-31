using AutoMapper;
using Automate.BLL.DTO;
using Automate.BLL.Interfaces;
using Automate.DAL.Entities;
using Automate.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using Excel = Microsoft.Office.Interop.Excel;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Automate.BLL.Services
{
    /// <summary>
    /// Service for coin
    /// and CRUD + busines
    /// </summary>
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
            Database.Save();
        }

        public void Delete(int id)
        {
            Database.Drinks.Delete(id);
            Database.Save();
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
            Database.Save();
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
                Database.Save();
            }
        }

        /// <summary>
        /// Method for import drinks from excel file
        /// Reading data from file and crean drinks to bd
        /// </summary>

        public void ImportDrinks(string pathExcel)
        {
            //Read data
            Excel.Application application = new Excel.Application();
            Excel.Workbook workbook = application.Workbooks.Open(pathExcel);
            Excel.Worksheet worksheet = workbook.ActiveSheet;
            Excel.Range range = worksheet.UsedRange;


            //Write data
            for (int row = 3; row <= range.Rows.Count; row++)
            {
                var drink = new DrinkDTO();
                drink.Name = ((Excel.Range)range.Cells[row, 1]).Text;
                drink.Number = Convert.ToInt32(((Excel.Range)range.Cells[row, 2]).Text);
                drink.PictureId = 1;
                drink.Price = Convert.ToInt32(((Excel.Range)range.Cells[row, 3]).Text);

                this.Create(drink);
            }

            //Kill EXCEL processes
            application.Quit();
            application = null;
            workbook = null;
            worksheet = null;
            range = null;

            Process[] List;
            List = Process.GetProcessesByName("EXCEL");
            foreach (Process proc in List)
            {
                proc.Kill();
            }
        }
    }
}
