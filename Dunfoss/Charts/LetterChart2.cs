using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Hosting;
using Dunfoss.Data;

namespace Dunfoss.Charts
{
    class LetterChart2
    {
        ICurrentFile currentFile = new EfCurrentFile();
        DataSet ds = new DataSet();
        Dictionary<int, int> WeeK_GoodsuM = new Dictionary<int, int>();
        List<DateTime> StartMonth = new List<DateTime>();
        List<DateTime> EndMonth = new List<DateTime>();
        DateTime January = new DateTime(2017, 01, 31, 23, 59, 00);
        DateTime February = new DateTime(2017, 02, 28, 23, 59, 00);
        DateTime March = new DateTime(2017, 03, 31, 23, 59, 00);
        DateTime April = new DateTime(2017, 04, 30, 23, 59, 00);
        DateTime May = new DateTime(2017, 05, 31, 23, 59, 00);
        DateTime June = new DateTime(2017, 06, 30, 23, 59, 00);
        DateTime July = new DateTime(2017, 07, 31, 23, 59, 00);
        DateTime August = new DateTime(2017, 08, 31, 23, 59, 00);
        DateTime September = new DateTime(2017, 09, 30, 23, 59, 00);
        DateTime Oktober = new DateTime(2017, 10, 31, 23, 59, 00);
        DateTime November = new DateTime(2017, 11, 30, 23, 59, 00);
        DateTime December = new DateTime(2017, 12, 31, 23, 59, 00);
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
        public LetterChart2()
        {
            EndMonth.Add(January);
            EndMonth.Add(February);
            EndMonth.Add(March);
            EndMonth.Add(April);
            EndMonth.Add(May);
            EndMonth.Add(June);
            EndMonth.Add(July);
            EndMonth.Add(August);
            EndMonth.Add(September);
            EndMonth.Add(Oktober);
            EndMonth.Add(November);
            EndMonth.Add(December);
            string path = HostingEnvironment.ApplicationPhysicalPath + currentFile.GetCurrentFile().Path2;
            FileStream stream = File.Open(path, FileMode.Open, FileAccess.Read);

            IExcelDataReader dataReader = null;

            if (path.EndsWith(".xls"))
            {
                dataReader = ExcelReaderFactory.CreateBinaryReader(stream);
            }
            else if (path.EndsWith(".xlsx"))
            {
                dataReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
            }

            var conf = new ExcelDataSetConfiguration
            {
                ConfigureDataTable = _ => new ExcelDataTableConfiguration
                {
                    UseHeaderRow = true
                }
            };

            for (int i = 1; i < 13; i++)
            {
                var temp = new DateTime(2017, i, 1, 00, 00, 00);
                StartMonth.Add(temp);
            }

            ds = dataReader.AsDataSet(conf);
            stream.Close();
            stream.Dispose();
            dataReader.Close();
            
        }

        public int[] CreateFirstTableValues(int MonthNumber, string[] Division)
        {
            DataRow[] foundRows;
            foundRows = ds.Tables[1].Select();
            int Sum11 = 0;
            int Sum21 = 0;
            int Sum12 = 0;
            int Sum22 = 0;
            foreach (var item in foundRows)
            {
                if (((item.ItemArray[12] is DateTime) && ((DateTime)item.ItemArray[12] >= StartMonth[MonthNumber - 1]) && ((DateTime)item.ItemArray[12]) < EndMonth[MonthNumber - 1])) { Sum11++; }
            }
            //////////
            foreach (var elem in Division)
            {
                foreach (var item in foundRows)
                {
                    if (((item.ItemArray[12] is DateTime) && (item.ItemArray[0].ToString().TrimEnd(' ') == elem) && ((DateTime)item.ItemArray[12] >= StartMonth[MonthNumber - 1]) && ((DateTime)item.ItemArray[12]) < EndMonth[MonthNumber - 1])) { Sum21++; }
                }
            }
            //////////
            foreach (var item in foundRows)
            {
                if (((item.ItemArray[12] is DateTime) && ((DateTime)item.ItemArray[12] >= StartMonth[MonthNumber - 1]) && ((DateTime)item.ItemArray[12]) < EndMonth[MonthNumber - 1])) { Sum12 += Convert.ToInt32(item.ItemArray[17]); }
            }
            //////////
            foreach (var elem in Division)
            {
                foreach (var item in foundRows)
                {
                    if (((item.ItemArray[13] is DateTime) && (item.ItemArray[0].ToString().TrimEnd(' ') == elem) && ((DateTime)item.ItemArray[13] >= StartMonth[MonthNumber - 1]) && ((DateTime)item.ItemArray[13]) < EndMonth[MonthNumber - 1])) { Sum22 += Convert.ToInt32(item.ItemArray[17]); }
                }
            }
            int[] valuesforfirsttable = new int[4];
            valuesforfirsttable[0] = Sum11;
            valuesforfirsttable[1] = Sum12;
            valuesforfirsttable[2] = Sum21;
            valuesforfirsttable[3] = Sum22;
            return valuesforfirsttable;
        }

        public void CreateFirstGraph(int MonthNumber, string[] Division, out int[] Allint, out int[] Goodint)
        {
            List<int> All = return_all_values_per_month_COUNT(MonthNumber, MonthNumber, Division);
            List<int> Good = return_good_values_per_month_COUNT(MonthNumber, MonthNumber, Division);
            Allint = new int[All.Count];
            Goodint = new int[Good.Count];
            All.CopyTo(Allint, 0);
            Good.CopyTo(Goodint, 0);
        }

        public float[] CreateSecondTableValues(int MonthNumber, string[] Division)
        {
            int Value11 = return_good_values_in_month_range(MonthNumber, MonthNumber, Division);

            int Value21 = return_bad_values_in_month_range(MonthNumber, MonthNumber, Division);

            float Value12 = 0;
            float Value22 = 0;
            if (Value11 + Value21 != 0)
            {
                Value12 = (Value11 / (float)(Value11 + Value21)) * 100;
                Value22 = (Value21 / (float)(Value11 + Value21)) * 100;
            }

            float[] valuesforfirsttable = new float[4];
            valuesforfirsttable[0] = Value11;
            valuesforfirsttable[1] = Value12;
            valuesforfirsttable[2] = Value21;
            valuesforfirsttable[3] = Value22;
            return valuesforfirsttable;
        }
        #region
        private int return_bad_values_in_month_range(int RangA, int RangB, string[] Division)
        {
            DataRow[] foundRows;
            int Sum = 0;

            foundRows = ds.Tables[1].Select("[column16] NOT IN ('время закрытия задачи','1day', '2day', '3day')");
            foreach (var elem in Division)
            {
                for (int i = RangA; i <= RangB; i++)
                {
                    foreach (var item in foundRows)
                    {
                        if ((item.ItemArray[0].ToString().TrimEnd(' ') == elem) && ((DateTime)item.ItemArray[12] >= StartMonth[i - 1]) && ((DateTime)item.ItemArray[12]) < EndMonth[i - 1])
                        {
                            Sum++;
                        }

                    }
                }
            }
            return Sum;
        }


        private int return_good_values_in_month_range(int RangA, int RangB, string[] Division)
        {
            DataRow[] foundRows;
            int Sum = 0;
            foundRows = ds.Tables[1].Select("[column16] IN ('1day', '2day', '3day')");
            foreach (var elem in Division)
            {
                for (int i = RangA; i <= RangB; i++)
                {
                    foreach (var item in foundRows)
                    {
                        if ((item.ItemArray[0].ToString().TrimEnd(' ') == elem) && ((DateTime)item.ItemArray[12] >= StartMonth[i - 1]) && ((DateTime)item.ItemArray[12]) < EndMonth[i - 1])
                        {
                            Sum++;
                        }
                    }
                }
            }
            return Sum;
        }
        #endregion

        #region
        private List<int> return_good_values_per_month_COUNT(int RangA, int RangB, string[] Divisions)
        {
            DataRow[] foundRows;
            int Sum = 0;
            foundRows = ds.Tables[1].Select("[column19] = 'нет'");
            List<int> lol = new List<int>();
            foreach (var elem in Divisions)
            {
                Sum = 0;//15
                for (int i = RangA; i <= RangB; i++)
                {

                    foreach (var item in foundRows)
                    {
                        if ((item.ItemArray[0].ToString().TrimEnd(' ') == elem) && (item.ItemArray[12] is DateTime) && ((DateTime)item.ItemArray[12] >= StartMonth[i - 1]) && ((DateTime)item.ItemArray[12]) < EndMonth[i - 1]) { Sum++; }
                    }

                }
                lol.Add(Sum);
            }
            return lol;
        }

        private List<int> return_all_values_per_month_COUNT(int RangA, int RangB, string[] Divisions)
        {
            DataRow[] foundRows;
            int Sum = 0;
            foundRows = ds.Tables[1].Select();
            List<int> lol = new List<int>();
            foreach (var elem in Divisions)
            {
                Sum = 0;//15
                for (int i = RangA; i <= RangB; i++)
                {

                    foreach (var item in foundRows)
                    {
                        if ((item.ItemArray[0].ToString().TrimEnd(' ') == elem) && (item.ItemArray[12] is DateTime) && ((DateTime)item.ItemArray[12] >= StartMonth[i - 1]) && ((DateTime)item.ItemArray[12]) < EndMonth[i - 1]) { Sum++; }
                    }

                }
                lol.Add(Sum);
            }
            return lol;
        }
        #endregion
    }
}
