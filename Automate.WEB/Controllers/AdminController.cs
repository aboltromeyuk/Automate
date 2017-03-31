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
            if (ModelState.IsValid)
            {
                Mapper.Initialize(cfg => cfg.CreateMap<DrinkViewModel, DrinkDTO>());
                drinkService.Create(Mapper.Map<DrinkViewModel, DrinkDTO>(drink));
                return View();
            }

            return View(drink);
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
            drink.Image = pictureService.GetPicture(drink.PictureId).Image;

            if (ModelState.IsValid)
            {
                Mapper.Initialize(cfg => cfg.CreateMap<DrinkWithImgViewModel, DrinkDTO>());

                var drinkEditable = Mapper.Map<DrinkWithImgViewModel, DrinkDTO>(drink);

                drinkService.Update(drinkEditable);

                return RedirectToAction("Drinks");
            }

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
            if (ModelState.IsValid)
            {
                Mapper.Initialize(cfg => cfg.CreateMap<CoinViewModel, CoinDTO>());
                coinService.Create(Mapper.Map<CoinViewModel, CoinDTO>(coin));

                return View();
            }

            return View(coin);
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
            if (ModelState.IsValid)
            {
                Mapper.Initialize(cfg => cfg.CreateMap<CoinViewModel, CoinDTO>());

                var coinEditable = Mapper.Map<CoinViewModel, CoinDTO>(coin);

                coinService.Update(coinEditable);

                return RedirectToAction("Coins");
            }

            return View(coin);
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
            if ((string)Session["access"] == "true")
            {
                return PartialView();
            }

            return HttpNotFound(); 
        }

        [HttpPost]
        public ActionResult CreatePicture(PictureViewModel picture, HttpPostedFileBase uploadImage)
        {
            if (ModelState.IsValid && uploadImage != null)
            {
                byte[] imageData = null;

                using (var binaryReader = new BinaryReader(uploadImage.InputStream))        // reading uploadImage
                {
                    imageData = binaryReader.ReadBytes(uploadImage.ContentLength);          //uploadImage data to byte[]
                }

                picture.Image = imageData;

                Mapper.Initialize(cfg => cfg.CreateMap<PictureViewModel, PictureDTO>());

                pictureService.Create(Mapper.Map<PictureViewModel, PictureDTO>(picture));

                return View();
            }
            return View();
        }

        public ActionResult ImportDrinks()
        {
            if ((string)Session["access"] == "true")
            {
                return View();
            }

            return HttpNotFound();
        }

        [HttpPost]
        public ActionResult ImportDrinks(HttpPostedFileBase excelfile)
        {
            if(excelfile==null || excelfile.ContentLength == 0)  
            {
                ViewBag.Error = "Пожалуйста, выберите excel файл<br/>";
                return View();
            }
            else
            {
                if(excelfile.FileName.EndsWith("xls") || excelfile.FileName.EndsWith("xlsx"))
                {
                    string path = Server.MapPath("~/Files/" + excelfile.FileName);
                    if (System.IO.File.Exists(path))
                        System.IO.File.Delete(path);
                    excelfile.SaveAs(path);

                    drinkService.ImportDrinks(path);

                    return RedirectToAction("Drinks");
                }
                else
                {
                    ViewBag.Error = "Неверный формат файла<br/>";
                    return View();
                }
            }

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