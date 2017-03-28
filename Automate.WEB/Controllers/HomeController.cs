using AutoMapper;
using Automate.BLL.DTO;
using Automate.BLL.Interfaces;
using Automate.WEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Automate.WEB.Controllers
{
    public class HomeController : Controller
    {
        IDrinkService drinkService;
        ICoinService coinService;
        IPictureService pictureService;

        public HomeController(IDrinkService drinkServ, ICoinService coinServ, IPictureService pictureServ)
        {
            drinkService = drinkServ;
            coinService = coinServ;
            pictureService = pictureServ;
        }

        public ActionResult Index()
        {
            //var drink = new DrinkViewModel
            //{
            //    Name = "Colla",
            //    Number = 1,
            //    Price = 12
            //};
            //Mapper.Initialize(cfg => cfg.CreateMap<DrinkViewModel, DrinkDTO>());
            //drinkService.Create(Mapper.Map<DrinkViewModel, DrinkDTO>(drink));

            var allDrinks = drinkService.GetDrinks();

            Mapper.Initialize(cfg => cfg.CreateMap<DrinkDTO, DrinkWithImgViewModel>());
            var allDrinksWithImg = Mapper.Map<IEnumerable<DrinkDTO>, List<DrinkWithImgViewModel>>(allDrinks);

            foreach(var drink in allDrinksWithImg)
            {
                drink.Image = pictureService.GetPicture(drink.PictureId).Image;
            }

            return View(allDrinksWithImg);
        }

        public ActionResult InputMoney()
        {
            var allCoins = coinService.GetCoins();

            Mapper.Initialize(cfg => cfg.CreateMap<CoinDTO, CoinViewModel>());

            return PartialView(Mapper.Map<IEnumerable<CoinDTO>, List<CoinViewModel>>(allCoins));
        }

        [HttpPost]
        public HtmlString InputMoney(int id, int nominal, int number, bool blocked)
        {
            if (Session["sum"] == null)
                Session["sum"] = 0;
            
            Session["sum"] = (int)Session["sum"] + nominal;

            if (id != 0 )
            {
                var coin = new CoinViewModel
                {
                    Id = id,
                    Nominal = nominal,
                    Number = number + 1,
                    Blocked = blocked
                };

                Mapper.Initialize(cfg => cfg.CreateMap<CoinViewModel, CoinDTO>());
                coinService.Update(Mapper.Map<CoinViewModel, CoinDTO>(coin));
            }

            HtmlString result = new HtmlString(Session["sum"].ToString());

            return result;
        }

        public void SelectDrink(int id, string name, int pictureId, int number, int price)
        {
            var selectedDrink = new DrinkViewModel
            {
                 Id=id,
                 Name=name,
                 PictureId=pictureId,
                 Number=number,
                 Price=price
            };
                        
            var cartObjects = (Session["CartObjects"] as List<DrinkViewModel>) ?? new List<DrinkViewModel>();
            cartObjects.Add(selectedDrink);
            Session["CartObjects"] = cartObjects;
        }

        public void TakeDrinks()
        {
            var cartObjects = (Session["CartObjects"] as List<DrinkViewModel>) ?? new List<DrinkViewModel>();

            Mapper.Initialize(cfg => cfg.CreateMap<DrinkViewModel, DrinkDTO>());
            drinkService.TakeDrinks(Mapper.Map<List<DrinkViewModel>, IEnumerable<DrinkDTO>>(cartObjects));
            coinService.ReturnChange(Convert.ToInt32(Session["sum"]));
        }

        protected override void Dispose(bool disposing)
        {
            drinkService.Dispose();
            coinService.Dispose();
            base.Dispose(disposing);
        }

    }
}