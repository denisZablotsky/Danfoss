using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dunfoss.Charts;
using Dunfoss.Models;
using Dunfoss.Data;
using System.Web.Hosting;
using Dunfoss.Filters;

namespace Dunfoss.Controllers
{
    [Culture]
    public class LetterController : Controller
    {
        ILetterRepository letterRepository;

        LetterChart chart;
        LetterChart2 chart2 ;

        string[] Cities = new string[]{ "Краснодар - HE", "Ростов-на-Дону - HE", "Волгоград - HE", "Саратов - HE",
            "Владивосток - HE", "Хабаровск - HE", "Иркутск - HE", "Красноярск - HE",
            "Новосибирск - HE", "Омск - HE", "Санкт-Петербург - HE", "Екатеринбург - HE", "Ижевск - HE", "Пермь - HE",
            "Тюмень - HE", "Челябинск - HE", "Москва - HE", "Казань - HE", "Самара - HE", "Уфа - HE", "Н.Новгород - HE",
            "Воронеж - HE", "Ярославль - HE"
        , "Тула - HE"};

        string[] months = new string[] { "Январь", "Февраль", "Март", "Апрель", "Май", "Июнь", "Июль", "Август", "Сентябрь", "Октябрь", "Ноябрь", "Декабрь"};
        string[] DivisionJug = new string[] { "Краснодар - HE", "Ростов-на-Дону - HE", "Волгоград - HE", "Саратов - HE" };
        string[] DivisionDalniiVostok = new string[] { "Владивосток - HE", "Хабаровск - HE", "Иркутск - HE", "Красноярск - HE" };
        string[] DivisionZapadnayaSibir = new string[] { "Новосибирск - HE", "Омск - HE" };
        string[] DivisionSeveroZapad = new string[] { "Санкт-Петербург - HE" };
        string[] DivisionUral = new string[] { "Екатеринбург - HE", "Ижевск - HE", "Пермь - HE",
            "Тюмень - HE", "Челябинск - HE" };
        string[] DivisionMoskva = new string[] { "Москва - HE" };
        string[] DivisionPovolje = new string[] { "Казань - HE", "Самара - HE", "Уфа - HE", "Н.Новгород - HE" };
        string[] DivisionCentr = new string[] { "Воронеж - HE", "Ярославль - HE", "Тула - HE" };
        string[] divisions = new string[] { "Все", "Юг", "Дальний Восток", "Западная Сибирь", "Северо-Запад", "Урал", "Москва", "Поволжье", "Центр" };
        // GET: Letter
        public LetterController()
        {
            chart = new LetterChart();
            chart2 = new LetterChart2();
            letterRepository = new EfLetterRepository();
        }
        public ActionResult Index()
        {
            
            return View();
        }

        [HttpGet]
        public ActionResult GetLetterById(int id)
        {
            Letter letter = letterRepository.GetLetterById(id);
            int[] all = null;
            string[] table = letter.Table1.Split(';');
            all = new int[table.Length];
            for(int i = 0; i < all.Length; i++)
            {
                all[i] = int.Parse(table[i]);
            }
            ViewBag.Table1 = all;
            float[] f = null;
            table = letter.Table2.Split(';');
            f = new float[table.Length];
            for(int i = 0; i < f.Length; i++)
            {
                f[i] = float.Parse(table[i]);
            }
            ViewBag.Table2 = f;

            table = letter.Table3.Split(';');
            all = new int[table.Length];
            for (int i = 0; i < all.Length; i++)
            {
                all[i] = int.Parse(table[i]);
            }
            ViewBag.Table3 = all;
            f = null;
            table = letter.Table4.Split(';');
            f = new float[table.Length];
            for (int i = 0; i < f.Length; i++)
            {
                f[i] = float.Parse(table[i]);
            }
            ViewBag.Table4 = f;
            ViewBag.Chart1 = letter.Chart1;
            ViewBag.Chart2 = letter.Chart2;
            ViewBag.Division = letter.Division;
            ViewBag.Month = months[letter.month - 1];
            return View();
        }

        [HttpPost]
        public PartialViewResult Remove(int id)
        {
            Letter let = letterRepository.GetLetterById(id);
            System.IO.File.Delete(HostingEnvironment.ApplicationPhysicalPath + let.Chart1);
            System.IO.File.Delete(HostingEnvironment.ApplicationPhysicalPath + let.Chart2);
            letterRepository.RemoveLetter(id);

            IQueryable<Letter> list = letterRepository.Letter;
            list = list.OrderByDescending(x => x.Date);
            return PartialView("GetLetterList", list);
        }

        public ActionResult LetterList()
        {
            return View();
        }

        public PartialViewResult GetLetterList()
        {
            IQueryable<Letter> list = letterRepository.Letter;
            list = list.OrderByDescending(x => x.Date);
            return PartialView(list);
        }

        public PartialViewResult Formatting(int month)
        {
            int[] all = null, good = null;

            //Юг
            chart.CreateFirstGraph(month, DivisionJug, out all, out good);
            ViewBag.all1J = all;
            ViewBag.good1J = good;
            chart2.CreateFirstGraph(month, DivisionJug, out all, out good);
            ViewBag.all2J = all;
            ViewBag.good2J = good;
            ViewBag.Month = month;
            ViewBag.TitlesJ = DivisionJug;
            ViewBag.divisionJ = divisions[1];

            //Дальний Восток
            chart.CreateFirstGraph(month, DivisionDalniiVostok , out all, out good);
            ViewBag.all1V = all;
            ViewBag.good1V = good;
            chart2.CreateFirstGraph(month, DivisionDalniiVostok, out all, out good);
            ViewBag.all2V = all;
            ViewBag.good2V = good;
            ViewBag.TitlesV = DivisionDalniiVostok;
            ViewBag.divisionV = divisions[2];

            //Западная Сибирь
            chart.CreateFirstGraph(month, DivisionZapadnayaSibir, out all, out good);
            ViewBag.all1S = all;
            ViewBag.good1S = good;
            chart2.CreateFirstGraph(month, DivisionZapadnayaSibir, out all, out good);
            ViewBag.all2S = all;
            ViewBag.good2S = good;
            ViewBag.TitlesS = DivisionZapadnayaSibir;
            ViewBag.divisionS = divisions[3];

            //Северо-Запад
            chart.CreateFirstGraph(month, DivisionSeveroZapad, out all, out good);
            ViewBag.all1Z = all;
            ViewBag.good1Z = good;
            chart2.CreateFirstGraph(month, DivisionSeveroZapad, out all, out good);
            ViewBag.all2Z = all;
            ViewBag.good2Z = good;
            ViewBag.TitlesZ = DivisionSeveroZapad;
            ViewBag.divisionZ = divisions[4];

            //Урал
            chart.CreateFirstGraph(month, DivisionUral, out all, out good);
            ViewBag.all1U = all;
            ViewBag.good1U = good;
            chart2.CreateFirstGraph(month, DivisionUral, out all, out good);
            ViewBag.all2U = all;
            ViewBag.good2U = good;
            ViewBag.TitlesU = DivisionUral;
            ViewBag.divisionU = divisions[5];

            //Москва
            chart.CreateFirstGraph(month, DivisionMoskva, out all, out good);
            ViewBag.all1M = all;
            ViewBag.good1M = good;
            chart2.CreateFirstGraph(month, DivisionMoskva, out all, out good);
            ViewBag.all2M = all;
            ViewBag.good2M = good;
            ViewBag.TitlesM = DivisionMoskva;
            ViewBag.divisionM = divisions[6];

            //Поволжье
            chart.CreateFirstGraph(month, DivisionPovolje, out all, out good);
            ViewBag.all1P = all;
            ViewBag.good1P = good;
            chart2.CreateFirstGraph(month, DivisionPovolje, out all, out good);
            ViewBag.all2P = all;
            ViewBag.good2P = good;
            ViewBag.TitlesP = DivisionPovolje;
            ViewBag.divisionP = divisions[7];

            //Центр
            chart.CreateFirstGraph(month, DivisionCentr, out all, out good);
            ViewBag.all1C = all;
            ViewBag.good1C = good;
            chart2.CreateFirstGraph(month, DivisionCentr, out all, out good);
            ViewBag.all2C = all;
            ViewBag.good2C = good;
            ViewBag.TitlesC = DivisionCentr;
            ViewBag.divisionC = divisions[8];

            //Все
            chart.CreateFirstGraph(month, Cities, out all, out good);
            ViewBag.all1All = all;
            ViewBag.good1All = good;
            chart2.CreateFirstGraph(month, Cities, out all, out good);
            ViewBag.all2All = all;
            ViewBag.good2All = good;
            ViewBag.TitlesAll = Cities;
            ViewBag.divisionAll = "Все";
            
            return PartialView();
        }

        [HttpPost]
        public JsonResult GetLetter(Letter letter, string Chart1, string Chart2, string Division, int month)
        {
            Letter model = new Letter();
            model.Division = Division;
            model.Chart1 = Chart1;
            model.Chart2 = Chart2;

            string[] cities = null;
            if (Division == "Юг")
                cities = DivisionJug;
            else if (Division == "Дальний Восток")
                cities = DivisionDalniiVostok;
            else if (Division == "Западная Сибирь")
                cities = DivisionZapadnayaSibir;
            else if (Division == "Северо-Запад")
                cities = DivisionSeveroZapad;
            else if (Division == "Урал")
                cities = DivisionUral;
            else if (Division == "Москва")
                cities = DivisionMoskva;
            else if (Division == "Поволжье")
                cities = DivisionPovolje;
            else if (Division == "Центр")
                cities = DivisionCentr;
            else if (Division == "Все")
                cities = Cities;

            int[] all = null;
            float[] good = null;
            all = chart.CreateFirstTableValues(month, cities);
            string table = "";
            for(int i = 0; i < all.Length; i++)
            {
                table += all[i].ToString();
                if (i != all.Length - 1)
                    table += ";";
            }
            model.Table1 = table;

            table = "";
            good = chart.CreateSecondTableValues(month, cities);
            for (int i = 0; i < good.Length; i++)
            {
                table += good[i].ToString();
                if (i != good.Length - 1)
                    table += ";";
            }
            model.Table2 = table;
            all = chart2.CreateFirstTableValues(month, cities);
            table = "";
            for (int i = 0; i < all.Length; i++)
            {
                table += all[i].ToString();
                if (i != all.Length - 1)
                    table += ";";
            }
            model.Table3 = table;
            good = chart2.CreateSecondTableValues(month, cities);
            table = "";
            for (int i = 0; i < good.Length; i++)
            {
                table += good[i].ToString();
                if (i != good.Length - 1)
                    table += ";";
            }
            model.Table4 = table;
            model.Date = DateTime.Now;
            model.month = month;
            Letter newLet = letterRepository.CreateLetter(model);


            return Json(newLet.Id);
        }

        [HttpPost]
        public PartialViewResult GetLetters(int[] id)
        {

            return PartialView();
        }
    }
}