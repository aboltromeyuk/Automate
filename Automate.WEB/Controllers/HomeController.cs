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
        public HtmlString InputMoney(int nominal)
        {
            if (Session["sum"] == null)
                Session["sum"] = 0;
            
            Session["sum"] = (int)Session["sum"] + nominal;

            if (nominal > 0 )
            {
                var coin = coinService.GetCoinByNominal(nominal);
                coin.Number += 1;

                coinService.Update(coin);
            }

            HtmlString result = new HtmlString(Session["sum"].ToString());

            return result;
        }

        public void SelectDrink(int id)
        {
            var selectedDrink = drinkService.GetDrink(id);
                                   
            var cartObjects = (Session["CartObjects"] as List<DrinkViewModel>) ?? new List<DrinkViewModel>();
            Mapper.Initialize(cfg => cfg.CreateMap<DrinkDTO, DrinkViewModel>());
            cartObjects.Add(Mapper.Map<DrinkDTO, DrinkViewModel>(selectedDrink));
            Session["CartObjects"] = cartObjects;
        }

        public ActionResult TakeDrinks()
        {
            var cartDrinks = (Session["CartObjects"] as List<DrinkViewModel>) ?? new List<DrinkViewModel>();
            int change = 0;
            Mapper.Initialize(cfg => cfg.CreateMap<DrinkViewModel, DrinkDTO>());
            drinkService.TakeDrinks(Mapper.Map<List<DrinkViewModel>, IEnumerable<DrinkDTO>>(cartDrinks));

            Session["CartObjects"] = "";

            var coinsInChange = coinService.ReturnChange(Convert.ToInt32(Session["sum"]));

            if (coinsInChange.Count() != 0)
            {
                change = (int)Session["sum"] - coinService.GetSumOfCoins(coinsInChange);
                Session["sum"] = change;
            }

            var allChange = true;

            if (change != 0) { allChange = false; }

            Mapper.Initialize(cfg => cfg.CreateMap<DrinkViewModel, DrinkWithImgViewModel>());
            Mapper.Initialize(cfg => cfg.CreateMap<CoinDTO, CoinViewModel>());

            var drinksOfUser = new DrinksWithChange
            {
                AllChange = allChange,
                SumOfChange = change,
                Drinks = cartDrinks,
                Change = Mapper.Map<IEnumerable<CoinDTO>, List<CoinViewModel>>(coinsInChange)
            };

            return PartialView(drinksOfUser);
        }

        protected override void Dispose(bool disposing)
        {
            drinkService.Dispose();
            coinService.Dispose();
            base.Dispose(disposing);
        }

    }
}