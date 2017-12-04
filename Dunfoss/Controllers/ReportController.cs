using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Dunfoss.Charts;
using System.IO;
using Dunfoss.Models;
using Dunfoss.Data;

namespace Dunfoss.Controllers
{
    public class ReportController : Controller
    {
        string[] Cities = new string[]{ "Краснодар - HE", "Ростов-на-Дону - HE", "Волгоград - HE", "Саратов - HE",
            "Владивосток - HE", "Хабаровск - HE", "Иркутск - HE", "Красноярск - HE",
            "Новосибирск - HE", "Омск - HE", "Санкт-Петербург - HE", "Екатеринбург - HE", "Ижевск - HE", "Пермь - HE",
            "Тюмень - HE", "Челябинск - HE", "Москва - HE", "Казань - HE", "Самара - HE", "Уфа - HE", "Н.Новгород - HE",
            "Воронеж - HE", "Ярославль - HE"
        , "Тула - HE"};
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
        private IReportRepository reportReporsitory;
        public ReportController()
        {
            reportReporsitory = new EfReportRepository();
        }
        public ReportController(IReportRepository rep)
        {
            reportReporsitory = rep;
        }
        // GET: Report
        public ActionResult Index()
        {
            
            return View();
        }

        [HttpPost]
        public PartialViewResult Formatting(int min, int max, string division)
        {
            int[] ALLWeekvalues = null;
            int[] GoodWeekvalues = null;
            string[] titles = null;
            string[] titlesM = { "Январь", "Февраль", "Март", "Апрель", "Май", "Июнь", "Июль", "Август", "Сентябрь", "Октябрь", "Ноябрь", "Декабрь" };
            titles = new string[max - min + 1];
            for (int i = 0; i < max - min + 1; i++)
            {
                titles[i] = "W" + (min + i).ToString();
            }
            ViewBag.Titles = titles;

            // --- 1 ----
            FirstTypeChart chart = new FirstTypeChart(37);
            chart.return_all_values_weekly(min, max, out ALLWeekvalues);
            chart.return_good_values_weekly(min, max, out GoodWeekvalues);
            ViewBag.all1 = ALLWeekvalues;
            ViewBag.good1 = GoodWeekvalues;
            // ---- 2 -------
            chart = new FirstTypeChart(38);
            chart.return_all_values_weekly(min, max, out ALLWeekvalues);
            chart.return_good_values_weekly(min, max, out GoodWeekvalues);
            ViewBag.all2 = ALLWeekvalues;
            ViewBag.good2 = GoodWeekvalues;
            // ---- 3--------
            chart = new FirstTypeChart(39);
            chart.return_all_values_weekly(min, max, out ALLWeekvalues);
            chart.return_good_values_weekly(min, max, out GoodWeekvalues);
            ViewBag.all3 = ALLWeekvalues;
            ViewBag.good3 = GoodWeekvalues;
            // -----4--------
            chart.return_good_values_per_month(1, 12, out GoodWeekvalues);
            chart.return_all_values_per_month(1, 12, out ALLWeekvalues);
            ViewBag.all4 = ALLWeekvalues;
            ViewBag.good4 = GoodWeekvalues;
            ViewBag.titlesM = titlesM;
            // ------ 5--------
            ReasonsChart reasonChart = new ReasonsChart();
            IEnumerable<string> reasons = null;
            int local_min = max - 1, local_max = max;
            if (max <= 1)
                local_min = 1;
            reasons = reasonChart.Return_all_reasons4(local_min, local_max);
            string[] r = reasons.ToArray();
            reasonChart.Values_per_reasons_for4graph(local_min, local_max, reasons.ToArray(), out ALLWeekvalues);

            for (int i = 0; i < ALLWeekvalues.Length - 1; i++)
            {
                for (int j = 0; j < ALLWeekvalues.Length - 1; j++)
                {
                    if (ALLWeekvalues[j] < ALLWeekvalues[j + 1])
                    {
                        int temp = ALLWeekvalues[j];
                        ALLWeekvalues[j] = ALLWeekvalues[j + 1];
                        ALLWeekvalues[j + 1] = temp;
                        string t = r[j];
                        r[j] = r[j + 1];
                        r[j + 1] = t;
                    }
                }
            }

            ViewBag.all5 = ALLWeekvalues;
            ViewBag.titlesR = r;
            // ------ 6 ----------
            reasons = reasonChart.Return_all_reasons5(local_min, local_max);
            reasonChart.Values_per_reasons_for5graph(local_min, local_max, reasons.ToArray(), out ALLWeekvalues);

            r = reasons.ToArray();

            for (int i = 0; i < ALLWeekvalues.Length - 1; i++)
            {
                for (int j = 0; j < ALLWeekvalues.Length - 1; j++)
                {
                    if (ALLWeekvalues[j] < ALLWeekvalues[j + 1])
                    {
                        int temp = ALLWeekvalues[j];
                        ALLWeekvalues[j] = ALLWeekvalues[j + 1];
                        ALLWeekvalues[j + 1] = temp;
                        string t = r[j];
                        r[j] = r[j + 1];
                        r[j + 1] = t;
                    }
                }
            }


            ViewBag.all6 = ALLWeekvalues;
            ViewBag.titlesR2 = reasons.ToArray();
            // ------- 7 ----------
            string[] cities = null;
            if (division == divisions[0])
            {
                cities = Cities;
            }
            if (division == divisions[1])
            {
                cities = DivisionJug;
            }
            else if (division == divisions[2])
            {
                cities = DivisionDalniiVostok;
            }
            else if (division == divisions[3])
            {
                cities = DivisionZapadnayaSibir;
            }
            else if (division == divisions[4])
            {
                cities = DivisionSeveroZapad;
            }
            else if (division == divisions[5])
            {
                cities = DivisionUral;
            }
            else if (division == divisions[6])
            {
                cities = DivisionMoskva;
            }
            else if (division == divisions[7])
            {
                cities = DivisionPovolje;
            }
            else if (division == divisions[8])
            {
                cities = DivisionCentr;
            }
            List<int> all = null;
            List<int> good = null;
            ChartFormat7 chart7 = new ChartFormat7();
            all = chart7.return_all_values_per_week_COUNT(min, max, cities);
            good = chart7.return_good_values_per_week_COUNT(min, max, cities);

            ALLWeekvalues = new int[all.Count];
            GoodWeekvalues = new int[good.Count];

            for (int i = 0; i < ALLWeekvalues.Length; i++)
            {
                ALLWeekvalues[i] = all[i];
                GoodWeekvalues[i] = good[i];
            }

            ViewBag.all7 = ALLWeekvalues;
            ViewBag.good7 = GoodWeekvalues;
            ViewBag.TitlesC = cities;

            return PartialView();
        }

        [HttpPost]
        public JsonResult Export(string imageData, string number)
        {
            string name = System.DateTime.Now.ToString("ddMMyyyyhhmmss") + "-" + number + ".png";
            string fileName = Path.Combine(Server.MapPath("~/ChartImages/"), name);
            using (FileStream fs = new FileStream(fileName, FileMode.Create))
            {
                using (BinaryWriter bw = new BinaryWriter(fs))
                {
                    byte[] data = Convert.FromBase64String(imageData);
                    bw.Write(data);
                    bw.Close();
                }
            }
            name = "/ChartImages/" + name;
            return Json(name);
        }

        [HttpPost]
        public JsonResult GetImages(string image1, string image2, string image3, string image4, string image5, string image6, string image7, Report report)
        {
            Report model = new Report();
            model.Date = DateTime.Now;
            model.image1 = image1;
            model.image2 = image2;
            model.image3 = image3;
            model.image4 = image4;
            model.image5 = image5;
            model.image6 = image6;
            model.image7 = image7;
            Report rep = reportReporsitory.CreateReport(model);

            return Json(rep.Id, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetReport(int id)
        {
            Report report = reportReporsitory.GetReportById(id);

            return Json(report.Id, JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult GetReportById(int id)
        {
            Report report = reportReporsitory.GetReportById(id);
            return PartialView("ShowImages", report);
        }

        public PartialViewResult GetReportList()
        {
            IQueryable<Report> list = reportReporsitory.Reports;
            list = list.OrderByDescending(x => x.Date);
            return PartialView("ReportList", list);
        }
    }
}