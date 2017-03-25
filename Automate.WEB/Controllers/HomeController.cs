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

        public HomeController(IDrinkService drinkServ, ICoinService coinServ)
        {
            drinkService = drinkServ;
            coinService = coinServ;
        }

        public ActionResult Index()
        {
            var drinks = drinkService.GetDrinks();
            Mapper.Initialize(cfg => cfg.CreateMap<DrinkDTO, DrinkViewModel>());

            return View(Mapper.Map<IEnumerable<DrinkDTO>, List<DrinkViewModel>>(drinks));
        }

        protected override void Dispose(bool disposing)
        {
            drinkService.Dispose();
            coinService.Dispose();
            base.Dispose(disposing);
        }

    }
}