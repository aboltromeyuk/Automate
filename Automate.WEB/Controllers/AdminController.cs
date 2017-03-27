using AutoMapper;
using Automate.BLL.DTO;
using Automate.BLL.Interfaces;
using Automate.WEB.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Automate.WEB.Controllers
{
    public class AdminController : Controller
    {
        IDrinkService drinkService;
        ICoinService coinService;
        IPictureService pictureService;

        public AdminController(IDrinkService drinkServ, ICoinService coinServ, IPictureService pictureServ)
        {
            drinkService = drinkServ;
            coinService = coinServ;
            pictureService = pictureServ;
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
                byte[] imageOfDrink = pictureService.GetPicture(drinkEditable.PictureId).Image;

                Mapper.Initialize(cfg => cfg.CreateMap<DrinkDTO, DrinkWithImgViewModel>());
                var resultDrink = Mapper.Map<DrinkDTO, DrinkWithImgViewModel>(drinkEditable);
                resultDrink.Image = imageOfDrink;
                return View(resultDrink);
            }
            else return HttpNotFound();
        }

        [HttpPost]
        public ActionResult EditDrink(DrinkWithImgViewModel drink)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<DrinkWithImgViewModel, DrinkDTO>());

            var drinkEditable = Mapper.Map<DrinkWithImgViewModel, DrinkDTO>(drink);

            drinkService.Update(drinkEditable);
            
            drink.Image= pictureService.GetPicture(drink.PictureId).Image;

            return View(drink);
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

        public ActionResult Pictures()
        {
            if ((string)Session["access"] == "true")
            {
                var allPictures = pictureService.GetPictures();

                Mapper.Initialize(cfg => cfg.CreateMap<PictureDTO, PictureViewModel>());

                return PartialView(Mapper.Map<IEnumerable<PictureDTO>, List<PictureViewModel>>(allPictures));
            }

            return HttpNotFound();
        }

        public ActionResult CreatePicture()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreatePicture(PictureViewModel picture, HttpPostedFileBase uploadImage)
        {
            if (ModelState.IsValid && uploadImage != null)
            {
                byte[] imageData = null;

                using (var binaryReader = new BinaryReader(uploadImage.InputStream))
                {
                    imageData = binaryReader.ReadBytes(uploadImage.ContentLength);
                }

                picture.Image = imageData;

                Mapper.Initialize(cfg => cfg.CreateMap<PictureViewModel, PictureDTO>());

                pictureService.Create(Mapper.Map<PictureViewModel, PictureDTO>(picture));

                return View();
            }
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            drinkService.Dispose();
            coinService.Dispose();
            pictureService.Dispose();
            base.Dispose(disposing);
        }
    }
}