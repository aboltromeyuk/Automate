﻿using AutoMapper;
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
        public HtmlString InputMoney(int nominal)
        {
            if (Session["sum"] == null)
            {
                Session["sum"] = 0;
            }

            Session["sum"] = (int)Session["sum"] + nominal;

            if (nominal != 0)
            {
                var coin = coinService.GetCoinByNominal(nominal);
                coin.Number += 1;
                coinService.Update(coin);
            }

            HtmlString result = new HtmlString(Session["sum"].ToString());

            return result;
        }

        protected override void Dispose(bool disposing)
        {
            drinkService.Dispose();
            coinService.Dispose();
            base.Dispose(disposing);
        }

    }
}