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
    public class AdminController : Controller
    {
        IDrinkService drinkService;
        ICoinService coinService;

        public AdminController(IDrinkService drinkServ, ICoinService coinServ)
        {
            drinkService = drinkServ;
            coinService = coinServ;
        }
        
        public ActionResult Index(string key)
        {
            if (key == "automate" || (string)Session["access"] == "true")
            {
                Session["access"] = "true";
                return RedirectToAction("Drinks");
            }
            else return HttpNotFound();
        }

        public ActionResult Drinks()
        {
            if ((string)Session["access"] == "true")
            {
                var allDrinks = drinkService.GetDrinks();

                Mapper.Initialize(cfg => cfg.CreateMap<DrinkDTO, DrinkViewModel>());
                
                return View(Mapper.Map<IEnumerable<DrinkDTO>, List<DrinkViewModel>>(allDrinks));
            }
            else return HttpNotFound();
        }

        public ActionResult CreateDrink()
        {
            if ((string)Session["access"] == "true")
            {
                return View();
            }
            else return HttpNotFound();
        }

        [HttpPost]
        public ActionResult CreateDrink(DrinkViewModel drink)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<DrinkViewModel, DrinkDTO>());
            drinkService.Create(Mapper.Map<DrinkViewModel, DrinkDTO>(drink));

            return View();
        }

        public ActionResult EditDrink(int id)
        {
            if ((string)Session["access"] == "true")
            {
                var drinkEditable = drinkService.GetDrink(id);

                Mapper.Initialize(cfg => cfg.CreateMap<DrinkDTO, DrinkViewModel>());

                return View(Mapper.Map<DrinkDTO, DrinkViewModel>(drinkEditable));
            }
            else return HttpNotFound();
        }

        [HttpPost]
        public ActionResult EditDrink(DrinkViewModel drink)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<DrinkViewModel, DrinkDTO>());

            var drinkEditable = Mapper.Map<DrinkViewModel, DrinkDTO>(drink);

            drinkService.Update(drinkEditable);

            return View();
        }

        public ActionResult DeleteDrink(int id)
        {
            if ((string)Session["access"] == "true")
            {
                drinkService.Delete(id);
                return RedirectToAction("Drinks");
            }
            else return HttpNotFound();
        }

        public ActionResult Coins()
        {
            if ((string)Session["access"] == "true")
            {
                var allCoins = coinService.GetCoins();

                Mapper.Initialize(cfg => cfg.CreateMap<CoinDTO, CoinViewModel>());

                return View(Mapper.Map<IEnumerable<CoinDTO>, List<CoinViewModel>>(allCoins));
            }
            else return HttpNotFound();
        }

        public ActionResult CreateCoin()
        {
            if ((string)Session["access"] == "true")
            {
                return View();
            }
            else return HttpNotFound();
        }

        [HttpPost]
        public ActionResult CreateCoin(CoinViewModel coin)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<CoinViewModel, CoinDTO>());
            coinService.Create(Mapper.Map<CoinViewModel, CoinDTO>(coin));

            return View();
        }

        public ActionResult EditCoin(int id)
        {
            if ((string)Session["access"] == "true")
            {
                var coinEditable = coinService.GetCoin(id);

                Mapper.Initialize(cfg => cfg.CreateMap<CoinDTO, CoinViewModel>());

                return View(Mapper.Map<CoinDTO, CoinViewModel>(coinEditable));
            }
            else return HttpNotFound();
        }

        [HttpPost]
        public ActionResult EditCoin(CoinViewModel coin)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<CoinViewModel, CoinDTO>());

            var coinEditable = Mapper.Map<CoinViewModel, CoinDTO>(coin);

            coinService.Update(coinEditable);

            return View();
        }

        public ActionResult DeleteCoin(int id)
        {
            if ((string)Session["access"] == "true")
            {
                coinService.Delete(id);
                return RedirectToAction("Coins");
            }
            else return HttpNotFound();
        }

        protected override void Dispose(bool disposing)
        {
            drinkService.Dispose();
            coinService.Dispose();
            base.Dispose(disposing);
        }
    }
}