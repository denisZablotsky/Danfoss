using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using Dunfoss.Charts;
using System.Data.OleDb;
using System.IO;
using System;
using System.Collections.Generic;
using Dunfoss.Data;
using Dunfoss.Services;
using Dunfoss.Filters;
using Dunfoss.Resources;
using System.Threading;


// Название графиков
// Значение оценок добавлены вручную

namespace Dunfoss.Controllers
{
    [Culture]
    public class ChartController : Controller
    {
        ISecurityService _security;
        IFileRepository fileRep = new EfFileRepository();
        ICurrentFile currentFile = new EfCurrentFile();
        private string path; // Сделать ссылку у User
        private string[] chartNames = {"Новые расчеты БТП", "Корректировки расчетов", "Новые расчеты и корректировки БТП", "Причины просроченных задач", "Причины корректировок", "Расчеты БТП по дивизионам", "Заказы БТП" };
        private string filename;
        string[] months;

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
        string[] divisions = new string[] {"Все", "Юг", "Дальний Восток", "Западная Сибирь", "Северо-Запад", "Урал", "Москва", "Поволжье", "Центр"};

        public ChartController()
        {
            months = new string[] { Controllers_ChartController.Jan, Controllers_ChartController.Feb, Controllers_ChartController.Mar, Controllers_ChartController.Apr, Controllers_ChartController.May,
            Controllers_ChartController.June, Controllers_ChartController.July, Controllers_ChartController.Aug, Controllers_ChartController.Sept, Controllers_ChartController.Oct, Controllers_ChartController.Nov, Controllers_ChartController.Dec };
            

            
            _security = new SecurityService();
        }

        // GET: Chart
        public ActionResult Index()
        {
            if (!_security.IsAuthenticate())
                return RedirectToAction("Login", "Home");
            return View();
        }

        public ActionResult Search()
        {
            if (!_security.IsAuthenticate())
                return RedirectToAction("Login", "Home");
            return View();
        }

        public ActionResult GetNav()
        {
            if (!_security.IsAuthenticate())
                return RedirectToAction("Login", "Home");
            return View();
        }

        public ActionResult Info()
        {
            if (!_security.IsAuthenticate())
                return RedirectToAction("Login", "Home");
            TableInfo info = new TableInfo();
            int q = 0, d = 0, ag = 0, f = 0;
            info.Get_Total_Info(out q, out d, out ag, out f);
            string[,] ar = info.Get_More_Info_About_Total_Info();
            int len = ar.GetLength(0);

            ViewBag.q = q;
            ViewBag.d = d;
            ViewBag.ag = ag;
            ViewBag.f = f;
            ViewBag.len = len;
            ViewBag.ar = ar;

            return View();
        }

        [HttpPost]
        public PartialViewResult Search(string number)
        {
            Search s = new Charts.Search();
            List<string> list = s.SearchByNumber(number);
            return PartialView("SearchDraw", list);
        }

        [HttpPost]
        public JsonResult UploadAjax()
        {
            foreach(string file in Request.Files)
            {
                Models.File fileModel = new Models.File();

                var upload = Request.Files[file];
                if(upload != null)
                {
                    filename = HostingEnvironment.ApplicationPhysicalPath + "/xls/" + upload.FileName;
                    fileModel.Path = "/xls/" + upload.FileName;
                    fileModel.Name = upload.FileName;

                    if (file == "file1")
                    {
                        fileModel.Type = 1;
                        
                    }
                    else if (file == "file2")
                    {
                        fileModel.Type = 2;
                        
                    }
                    else
                    {
                        fileModel.Type = 3;
                        
                    }

                    Models.File f = fileRep.CreateFile(fileModel);

                    if (file == "file1")
                    {
                        fileModel.Type = 1;
                        currentFile.UpdateFile1("/xls/" + upload.FileName);
                        currentFile.UpdateFileID1(f.Id);
                    }
                    else if (file == "file2")
                    {
                        fileModel.Type = 2;
                        currentFile.UpdateFile2("/xls/" + upload.FileName);
                        currentFile.UpdateFileID2(f.Id);
                    }
                    else
                    {
                        fileModel.Type = 3;
                        currentFile.UpdateFile3("/xls/" + upload.FileName);
                        currentFile.UpdateFileID3(f.Id);
                    }

                    
                    upload.SaveAs(filename);
                }
            }
            if (Request.Files.Count > 1)
            {
                return Json("файлы успешно загружены!");
            }
            else
                return Json("файл успешно загружен!");

        }

        public ActionResult ViewForm()
        {
            if (!_security.IsAuthenticate())
                return RedirectToAction("Login", "Home");
            return View("ChartFormatting");
        }


        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase upload)
        {
            if (upload == null)
                return RedirectToAction("Index", "Home");

            HttpPostedFileBase file = (HttpPostedFileBase)upload;
            filename = HostingEnvironment.ApplicationPhysicalPath + "/xls/" + "1.xls";
            file.SaveAs(filename);
            this.path = filename;
            ViewBag.Filename = filename;
            return View("ChartFormatting"); 
        }

        public ActionResult Design()
        {
            if (!_security.IsAuthenticate())
                return RedirectToAction("Login", "Home");
            return View();
        }

        public ActionResult ChartFormatting()
        {
            if (!_security.IsAuthenticate())
                return RedirectToAction("Login", "Home");
            return View();
        }

        public ActionResult DesignersChart()
        {
            if (!_security.IsAuthenticate())
                return RedirectToAction("Login", "Home");
            return View();
        }

        [HttpPost]
        public PartialViewResult Graph3(string ChartType, string filterType, int monthNumber, int weekNumber, int min, int max, int minMonth, int maxMonth)
        {
            if (ChartType == "1")
            {
                Graph3 g = new Charts.Graph3();
                List<string> designers = new List<string>();
                List<object> gud = new List<object>();
                List<object> med = new List<object>();
                List<object> po = new List<object>();

                string[] des = null;
                float[] good = null;
                float[] medium = null;
                float[] poor = null;
                if (filterType == "1")
                {
                    g.Return_values_per_month(monthNumber, out designers, out gud, out med, out po);
                }
                else
                {
                    g.Return_values_per_week(weekNumber, out designers, out gud, out med, out po);
                }
                des = new string[designers.Count];
                good = new float[designers.Count];
                medium = new float[designers.Count];
                poor = new float[designers.Count];

                int index = 0;
                foreach (string d in designers)
                {
                    des[index] = d;
                    good[index] = (float)gud[index];
                    medium[index] = (float)med[index];
                    poor[index] = (float)po[index];
                    index++;
                }

                ViewBag.Titles = des;
                ViewBag.Good = good;
                ViewBag.Med = medium;
                ViewBag.Poor = poor;

                return PartialView();
            }
            else
            {
                Graph4 g = new Graph4();
                List<object> Designers = null;
                List<float> Results = null;
                string[] designers = null;
                float[] results = null;
                if(filterType == "1")
                {
                    g.Return_values_per_month(monthNumber, monthNumber, out Designers, out Results);
                }
                else
                {
                    g.Return_values_per_week(weekNumber, weekNumber, out Designers, out Results);
                }

                designers = new string[Designers.Count];
                results = new float[Results.Count];
                int index = 0;
                foreach(object des in Designers)
                {
                    designers[index] = (string)des;
                    results[index] = (float)Results[index];
                    index++;
                }

                ViewBag.Titles = designers;
                ViewBag.Good = results;

                return PartialView("DesignersAvg");
            }
        }

        [HttpPost]
        public PartialViewResult DesignDraw(string ChartType, string designType, string filterType, int weekMin, int weekMax, int monthMin, int monthMax)
        {
            ViewBag.FilterType = filterType;
            int[] ALLWeekvalues = new int[52];
            int[] GoodWeekvalues = new int[52];
            string[] titles = null;

            string[] titlesMonth = new string[monthMax - monthMin + 1];
            for (int i = 0; i < titlesMonth.Length; i++)
            {
                titlesMonth[i] = (i + monthMin - 1).ToString();
            }
            string MainTitle = "";
            if (ChartType == "1")
            {
                DesignChart design = new DesignChart();
                int ty = 0;
                if (filterType == "1")
                {

                    design.Return_values_per_month(monthMin, monthMax, designType, out ALLWeekvalues, out GoodWeekvalues);
                    titles = titlesMonth;
                    ty = 2;
                }

                else
                {
                    design.Return_values_per_week(weekMin, weekMax, designType, out ALLWeekvalues, out GoodWeekvalues);
                    titles = new string[weekMax - weekMin + 1];
                    for (int i = 0; i < weekMax - weekMin + 1; i++)
                    {
                        titles[i] = "W" + (weekMin + i).ToString();
                    }
                    ty = 1;
                }

                ViewBag.GoodValuesWeek = GoodWeekvalues;
                ViewBag.AllValuesWeek = ALLWeekvalues;
                ViewBag.Titles = titles;
                ViewBag.type = ty;
                ViewBag.MainTitle = 9;
                return PartialView("ChartDraw");
            }
            else
            {
                DesignCities chartDesign = new Charts.DesignCities();
                IEnumerable<string> cities = null;
                if (filterType == "2")
                {
                    cities = chartDesign.returnCitiesWeek(ChartType, weekMin, weekMax);
                    ViewBag.min = weekMin;
                    ViewBag.max = weekMax;
                    ViewBag.filterType = 2;///
                    //ViewBag.type = 1;
                }
                else
                {
                    cities = chartDesign.returnCitiesMonth(ChartType, monthMin, monthMax);
                    ViewBag.min = monthMin;
                    ViewBag.max = monthMax;
                    ViewBag.filterType = 1;

                }
                ViewBag.designType = designType;
                ViewBag.MainTitle = 9;
                return PartialView("DesignCities", cities);
            }
        }

        [HttpPost]
        public PartialViewResult ChartDraw(string filename, int min, int max, string chartNumber, int minMonth, int maxMonth, int month, string division, string tp, string filterType, string spin)
        {
            ViewBag.FilterType = filterType;
            ViewBag.MainTitle = chartNumber;
            int[] ALLWeekvalues = null;
            int[] GoodWeekvalues = null;
            string[] titles = null;

            string MainTitle = "";

            string[] titlesMonth = new string[maxMonth - minMonth + 1];
            for(int i = 0; i < titlesMonth.Length; i++)
            {
                titlesMonth[i] = (i + minMonth - 1).ToString();
                
            }

            FirstTypeChart chart = null; 


            if (chartNumber == "1")
            {               
                chart = new FirstTypeChart(37);
                //MainTitle = "Новые расчеты БТП";
            }
            else if (chartNumber == "2")
            {
                chart = new FirstTypeChart(38);
                //MainTitle = "Корректировки расчетов БТП";
            }
            else if (chartNumber == "3")
            {
                chart = new FirstTypeChart(39);
                //MainTitle = "Новые расчеты и корректировки БТП";
            }
            else if (chartNumber == "4")
            {
                ReasonsChart fourChart = new ReasonsChart();
                IEnumerable<string> reasons = null;
                reasons = fourChart.Return_all_reasons4(min, max);
                ViewBag.min = min;
                ViewBag.max = max;
                ViewBag.Function = "ChartDraw45";
                MainTitle = "Причины просроченных задач";
                ViewBag.MainTitle = MainTitle;
                return PartialView("Draw45", reasons);
                

            }
            else if (chartNumber == "5")
            {
                ReasonsChart fiveChart = new ReasonsChart();
                IEnumerable<string> reasons = null;
                reasons = fiveChart.Return_all_reasons5(min, max);
                ViewBag.min = min;
                ViewBag.max = max;
                ViewBag.Function = "ChartDraw5";
                MainTitle = "Причины корректировок";
                ViewBag.MainTitle = MainTitle;
                return PartialView("Draw45", reasons);
            }
            else if (chartNumber == "6")
            {
                ChartFormat67 chart6 = new ChartFormat67();
                string[] cities = null;
                if (division == divisions[0])
                {
                    cities = Cities;
                }
                if (division == divisions[1])
                {
                    cities = DivisionJug;
                }
                else if(division == divisions[2])
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
                ViewBag.OyTitle = 2;
                titles = cities;
                ALLWeekvalues = chart6.return_all_values_for_reasons_in_month_range(minMonth, maxMonth, cities);
                GoodWeekvalues = chart6.return_good_values_for_reasons_in_month_range(minMonth, maxMonth, cities);
                ViewBag.GoodValuesWeek = GoodWeekvalues;
                ViewBag.AllValuesWeek = ALLWeekvalues;
                ViewBag.Titles = titles;
                ViewBag.Type = 3;
                //MainTitle = "Расчеты БТП по дивизионам";
                //ViewBag.MainTitle = MainTitle;
                return PartialView("ChartDraw");
            }
            else if (chartNumber == "7")
            {
                if(spin == "with")
                {
                    ChartFormat7 chart7 = new ChartFormat7();
                    int ty = 0;
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

                    if (tp == "задачи")
                    {
                        ViewBag.OyTitle = 1;

                        if (filterType == "2")
                        {
                            good = chart7.return_good_values_per_week_SUM(min, max, cities);
                            all = chart7.return_all_values_per_week_SUM(min, max, cities);

                        }
                        //chart7.CreateGraphWithWeekFilterSumY(out ALLWeekvalues, out GoodWeekvalues, min, max);
                        else
                        {
                            all = chart7.return_all_values_per_month_SUM(minMonth, maxMonth, cities);
                            good = chart7.return_good_values_per_month_SUM(minMonth, maxMonth, cities);

                        }
                        ty = 3;
                        // chart7.CreateGraphWithMonthFilterSumY(out ALLWeekvalues, out GoodWeekvalues, minMonth, maxMonth);
                    }
                    else
                    {
                        ViewBag.OyTitle = 0;
                        if (filterType == "2")
                        {
                            all = chart7.return_all_values_per_week_COUNT(min, max, cities);
                            good = chart7.return_good_values_per_week_COUNT(min, max, cities);

                        }
                        //chart7.CreateGraphWithWeekFilterCountY(out ALLWeekvalues, out GoodWeekvalues, min, max);
                        else
                        {
                            all = chart7.return_all_values_per_month_COUNT(minMonth, maxMonth, cities);
                            good = chart7.return_good_values_per_month_COUNT(minMonth, maxMonth, cities);

                        }
                        ty = 3;
                    }
                    ALLWeekvalues = new int[all.Count];
                    GoodWeekvalues = new int[good.Count];

                    for (int i = 0; i < ALLWeekvalues.Length; i++)
                    {
                        ALLWeekvalues[i] = all[i];
                        GoodWeekvalues[i] = good[i];
                    }



                    ViewBag.GoodValuesWeek = GoodWeekvalues;
                    ViewBag.AllValuesWeek = ALLWeekvalues;
                    ViewBag.Titles = cities;
                    ViewBag.Type = ty;
                    //MainTitle = "Заказы БТП";
                    //ViewBag.MainTitle = MainTitle;
                    return PartialView("ChartDraw");
                }
                else
                {
                    Chart7WithoutDivisions chart7 = new Chart7WithoutDivisions();
                    int ty = 0;
                    string[] cities = null;
                    //if (division == divisions[0])
                    //{
                    //    cities = Cities;
                    //}
                    //if (division == divisions[1])
                    //{
                    //    cities = DivisionJug;
                    //}
                    //else if (division == divisions[2])
                    //{
                    //    cities = DivisionDalniiVostok;
                    //}
                    //else if (division == divisions[3])
                    //{
                    //    cities = DivisionZapadnayaSibir;
                    //}
                    //else if (division == divisions[4])
                    //{
                    //    cities = DivisionSeveroZapad;
                    //}
                    //else if (division == divisions[5])
                    //{
                    //    cities = DivisionUral;
                    //}
                    //else if (division == divisions[6])
                    //{
                    //    cities = DivisionMoskva;
                    //}
                    //else if (division == divisions[7])
                    //{
                    //    cities = DivisionPovolje;
                    //}
                    //else if (division == divisions[8])
                    //{
                    //    cities = DivisionCentr;
                    //}
                    List<int> all = null;
                    List<int> good = null;

                    if (tp == "задачи")
                    {
                        ViewBag.OyTitle = 1;

                        if (filterType == "2")
                        {
                            good = chart7.return_good_values_per_week_SUM(min, max, cities);
                            all = chart7.return_all_values_per_week_SUM(min, max, cities);

                        }
                        //chart7.CreateGraphWithWeekFilterSumY(out ALLWeekvalues, out GoodWeekvalues, min, max);
                        else
                        {
                            all = chart7.return_all_values_per_month_SUM(minMonth, maxMonth, cities);
                            good = chart7.return_good_values_per_month_SUM(minMonth, maxMonth, cities);

                        }
                        ty = 3;
                        // chart7.CreateGraphWithMonthFilterSumY(out ALLWeekvalues, out GoodWeekvalues, minMonth, maxMonth);
                    }
                    else
                    {
                        ViewBag.OyTitle = 0;
                        if (filterType == "2")
                        {
                            all = chart7.return_all_values_per_week_COUNT(min, max, cities);
                            good = chart7.return_good_values_per_week_COUNT(min, max, cities);

                        }
                        //chart7.CreateGraphWithWeekFilterCountY(out ALLWeekvalues, out GoodWeekvalues, min, max);
                        else
                        {
                            all = chart7.return_all_values_per_month_COUNT(minMonth, maxMonth, cities);
                            good = chart7.return_good_values_per_month_COUNT(minMonth, maxMonth, cities);

                        }
                        ty = 3;
                    }
                    ALLWeekvalues = new int[all.Count];
                    GoodWeekvalues = new int[good.Count];

                    for (int i = 0; i < ALLWeekvalues.Length; i++)
                    {
                        ALLWeekvalues[i] = all[i];
                        GoodWeekvalues[i] = good[i];
                    }

                    string[] tit = null;

                    if(filterType == "1")
                    {
                        tit = new string[maxMonth - minMonth + 1];
                        for (int i = 0; i < titlesMonth.Length; i++)
                        {
                            tit[i] = months[i + minMonth - 1];

                        }
                    }
                    else
                    {
                        tit = new string[max - min + 1];
                        for(int i = 0; i < tit.Length; i++)
                        {
                            tit[i] = "W" + (min + i).ToString();
                        }
                    }


                    ViewBag.GoodValuesWeek = GoodWeekvalues;
                    ViewBag.AllValuesWeek = ALLWeekvalues;
                    ViewBag.Titles = tit;
                    ViewBag.Type = ty;
                    //MainTitle = "Заказы БТП";
                    //ViewBag.MainTitle = MainTitle;
                    return PartialView("ChartDraw");
                }
                
            }
            else if (chartNumber == "8")
            {
                TheLastChart lastChart = new TheLastChart();
                IEnumerable<string> surnames = new List<string>();
                if(filterType == "1")
                {
                    surnames = lastChart.Get_Surnames_Per_Month(minMonth, maxMonth);
                    ViewBag.Type = 0;
                    ViewBag.min = minMonth;
                    ViewBag.max = maxMonth;
                }
                else
                {
                    surnames = lastChart.Get_Surnames_Per_Week(min, max);
                    ViewBag.Type = 2;
                    ViewBag.min = min;
                    ViewBag.max = max;
                }

                

                return PartialView("LastDraw", surnames);
            }
                int t = 0;
            // First Chart
            if (filterType == "2")
            {
                chart.return_all_values_weekly(min, max, out ALLWeekvalues);
                chart.return_good_values_weekly(min, max, out GoodWeekvalues);
                titles = new string[max - min + 1];
                for (int i = 0; i < max - min + 1; i++)
                {
                    titles[i] = "W" + (min + i).ToString();
                }
                t = 1;
            }
            else
            {
                chart.return_all_values_per_month(minMonth, maxMonth, out ALLWeekvalues);
                chart.return_good_values_per_month(minMonth, maxMonth, out GoodWeekvalues);
                titles = titlesMonth;
                t = 2;
            }
            //

            ViewBag.GoodValuesWeek = GoodWeekvalues;
            ViewBag.AllValuesWeek = ALLWeekvalues;
            ViewBag.Titles = titles;
            ViewBag.Type = t;
            //ViewBag.MainTitle = MainTitle;
            return PartialView("ChartDraw");
        }


        [HttpPost]
        public PartialViewResult LastChart(int type, int min, int max, string[] checkedValues, string graphType, string typeCountSum)
        {
            TheLastChart lastChart = new TheLastChart();
            int[] all = null, bad = null, good = null;
            
            if(graphType == "1")
            {
                ViewBag.MainTitle = "Новые расчеты";
                if (typeCountSum == "count")
                {
                    if(type == 1)
                    {
                        lastChart.First_Type_Graph_Per_Month_Count(min, max, out good, out bad, checkedValues);
                    }
                    else
                    {
                        lastChart.First_Type_Graph_Per_Week_Count(min, max, out good, out bad, checkedValues);
                    }
                }
                else
                {
                    if (type == 1)
                    {
                        lastChart.First_Type_Graph_Per_Month_Sum(min, max, out good, out bad, checkedValues);
                    }
                    else
                    {
                        lastChart.First_Type_Graph_Per_Week_Sum(min, max, out good, out bad, checkedValues);
                    }
                }
            }
            else if(graphType == "2")
            {
                ViewBag.MainTitle = "Корректировки расчетов";
                if (typeCountSum == "count")
                {
                    if (type == 1)
                    {
                        lastChart.Second_Type_Graph_Per_Month_Count(min, max, out good, out bad, checkedValues);
                    }
                    else
                    {
                        lastChart.Second_Type_Graph_Per_Week_Count(min, max, out good, out bad, checkedValues);
                    }
                }
                else
                {
                    if (type == 1)
                    {
                        lastChart.Second_Type_Graph_Per_Month_Sum(min, max, out good, out bad, checkedValues);
                    }
                    else
                    {
                        lastChart.Second_Type_Graph_Per_Week_Sum(min, max, out good, out bad, checkedValues);
                    }
                }
            }
            else
            {
                ViewBag.MainTitle = "Сумма новых и корректировок";
                if (typeCountSum == "count")
                {
                    if (type == 1)
                    {
                        lastChart.Third_Type_Graph_Per_Month_Count(min, max, out good, out bad, checkedValues);
                    }
                    else
                    {
                        lastChart.Third_Type_Graph_Per_Week_Count(min, max, out good, out bad, checkedValues);
                    }
                }
                else
                {
                    if (type == 1)
                    {
                        lastChart.Third_Type_Graph_Per_Month_Sum(min, max, out good, out bad, checkedValues);
                    }
                    else
                    {
                        lastChart.Third_Type_Graph_Per_Week_Sum(min, max, out good, out bad, checkedValues);
                    }
                }
            }
            all = new int[good.Length];
            for(int i = 0; i < all.Length; i++)
            {
                all[i] = good[i] + bad[i];
            }

            for(int i = 0; i < all.Length - 1; i++)
            {
                for(int j = 0; j < all.Length - 1; j++)
                {
                    if(all[j] < all[j + 1])
                    {
                        int temp = all[j];
                        all[j] = all[j + 1];
                        all[j + 1] = temp;
                        temp = good[j];
                        good[j] = good[j + 1];
                        good[j + 1] = temp;
                        string t = checkedValues[j];
                        checkedValues[j] = checkedValues[j + 1];
                        checkedValues[j + 1] = t;
                    }
                }
            }

            ViewBag.AllValuesWeek = all;
            ViewBag.GoodValuesWeek = good;
            ViewBag.Titles = checkedValues;
            ViewBag.Type = 6;
            if (typeCountSum == "count")
            {
                ViewBag.OyTitle = 3;
            }
            else
            {
                ViewBag.OyTitle = 0;
            } 
            return PartialView("ChartDrawLast");
        }

        [HttpPost]
        public PartialViewResult ChartDraw45(string[] checkedValues, int min, int max)
        {
            ReasonsChart cart = new ReasonsChart();
            int[] all = null;
            cart.Values_per_reasons_for4graph(min, max, checkedValues, out all);
            // <Sort>
            for(int i = 0; i < all.Length - 1; i++)
            {
                for(int j = 0; j < all.Length - 1; j++)
                {
                    if(all[j] < all[j + 1])
                    {
                        int temp = all[j];
                        all[j] = all[j + 1];
                        all[j + 1] = temp;
                        string t = checkedValues[j];
                        checkedValues[j] = checkedValues[j + 1];
                        checkedValues[j + 1] = t;
                    }
                }
            }
            // <Sort/>
            ViewBag.All = all;
            ViewBag.Titles = checkedValues;
            return PartialView("Chart45");
        }

        [HttpPost]
        public PartialViewResult DesignCities(string[] checkedValues, int min, int max, string designType, string filterType)
        {
            ViewBag.FilterType = 3;
            DesignCities design = new Charts.DesignCities();
            int[] all = null;
            int[] good = null;
            if(filterType == "2")
            {
                design.Return_values_per_week(min, max, designType, checkedValues, out all, out good);
                
            }
            else
            {
                design.Return_values_per_month(min, max, designType, checkedValues, out all, out good);
                
            }
            ViewBag.Type = 3;
            ViewBag.Titles = checkedValues;
            ViewBag.GoodValuesWeek = good;
            ViewBag.AllValuesWeek = all;
            ViewBag.MainTitle = 9;
            return PartialView("ChartDraw");
        }

        [HttpPost]
        public PartialViewResult ChartDraw5(string[] checkedValues, int min, int max)
        {
            ReasonsChart cart = new ReasonsChart();
            int[] all = null;
            cart.Values_per_reasons_for5graph(min, max, checkedValues, out all);
            // <Sort>
            for (int i = 0; i < all.Length - 1; i++)
            {
                for (int j = 0; j < all.Length - 1; j++)
                {
                    if (all[j] < all[j + 1])
                    {
                        int temp = all[j];
                        all[j] = all[j + 1];
                        all[j + 1] = temp;
                        string t = checkedValues[j];
                        checkedValues[j] = checkedValues[j + 1];
                        checkedValues[j + 1] = t;
                    }
                }
            }
            // <Sort/>
            ViewBag.All = all;
            ViewBag.Titles = checkedValues;
            return PartialView("Chart45");
        }

        
    }
}