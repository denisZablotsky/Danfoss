using Dunfoss.Data;
using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;

namespace Dunfoss.Charts
{
    public class Chart7WithoutDivisions
    {

        DataSet ds = new DataSet();

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

        public Chart7WithoutDivisions()
        {
            ICurrentFile currentFile = new EfCurrentFile();
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


            for (int i = 1; i < 13; i++)
            {
                var temp = new DateTime(2017, i, 1, 00, 00, 00);
                StartMonth.Add(temp);
            }

            ds = dataReader.AsDataSet();
            dataReader.Close();
        }


        //COUNT
        #region
        public List<int> return_good_values_per_week_COUNT(int RangA, int RangB, string[] Divisions)
        {
            DataRow[] foundRows;
            int Sum = 0;
            foundRows = ds.Tables[1].Select("[column19] = 'нет'");
            List<int> lol = new List<int>();
            
                for (int i = RangA; i <= RangB; i++)
                {
                    Sum = 0;//15
                    foreach (var item in foundRows)
                    {
                        if ((item.ItemArray[15].ToString() == "W" + i)) { Sum++; }
                    }
                    lol.Add(Sum);
                }
            return lol;
        }

        public List<int> return_all_values_per_week_COUNT(int RangA, int RangB, string[] Divisions)
        {
            DataRow[] foundRows;
            int Sum = 0;
            foundRows = ds.Tables[1].Select();
            List<int> lol = new List<int>();
            for (int i = RangA; i <= RangB; i++)
            {
                Sum = 0;//15
                foreach (var item in foundRows)
                {
                    if ((item.ItemArray[15].ToString() == "W" + i)) { Sum++; }
                }
                lol.Add(Sum);
            }
            return lol;
        }

        public List<int> return_good_values_per_month_COUNT(int RangA, int RangB, string[] Divisions)
        {
            DataRow[] foundRows;
            int Sum = 0;
            foundRows = ds.Tables[1].Select("[column19] = 'нет'");
            List<int> lol = new List<int>();
            
                
                for (int i = RangA; i <= RangB; i++)
                {
                    Sum = 0;//15
                    foreach (var item in foundRows)
                    {
                        if ((item.ItemArray[12] is DateTime) && ((DateTime)item.ItemArray[12] >= StartMonth[i - 1]) && ((DateTime)item.ItemArray[12]) < EndMonth[i - 1]) { Sum++; }
                    }
                    lol.Add(Sum);
                }
                
            
            return lol;
        }

        public List<int> return_all_values_per_month_COUNT(int RangA, int RangB, string[] Divisions)
        {
            DataRow[] foundRows;
            int Sum = 0;
            foundRows = ds.Tables[1].Select();
            List<int> lol = new List<int>();
            for (int i = RangA; i <= RangB; i++)
            {
                Sum = 0;//15
                foreach (var item in foundRows)
                {
                    if ((item.ItemArray[12] is DateTime) && ((DateTime)item.ItemArray[12] >= StartMonth[i - 1]) && ((DateTime)item.ItemArray[12]) < EndMonth[i - 1]) { Sum++; }
                }
                lol.Add(Sum);
            }
            return lol;
        }
        #endregion //COUNT//        //
        //SUM
        #region
        public List<int> return_good_values_per_week_SUM(int RangA, int RangB, string[] Divisions)
        {
            DataRow[] foundRows;
            int Sum = 0;
            foundRows = ds.Tables[1].Select("[column19] = 'нет'");
            List<int> lol = new List<int>();

            
                
                for (int i = RangA; i <= RangB; i++)
                {
                    Sum = 0;//15
                    foreach (var item in foundRows)
                    {
                        if ((item.ItemArray[15].ToString() == "W" + i)) { Sum += Convert.ToInt32(item.ItemArray[17]); }
                    }
                    lol.Add(Sum);
                }
                
            
            return lol;
        }

        public List<int> return_all_values_per_week_SUM(int RangA, int RangB, string[] Divisions)
        {
            DataRow[] foundRows;
            int Sum = 0;
            foundRows = ds.Tables[1].Select();
            List<int> lol = new List<int>();

            for (int i = RangA; i <= RangB; i++)
            {
                Sum = 0;//15
                foreach (var item in foundRows)
                {
                    if ((item.ItemArray[15].ToString() == "W" + i)) { Sum += Convert.ToInt32(item.ItemArray[17]); }
                }
                lol.Add(Sum);
            }
            return lol;
        }

        public List<int> return_good_values_per_month_SUM(int RangA, int RangB, string[] Divisions)
        {
            DataRow[] foundRows;
            int Sum = 0;
            foundRows = ds.Tables[1].Select("[column19] = 'нет'");
            List<int> lol = new List<int>();

            
                
                for (int i = RangA; i <= RangB; i++)
                {
                    Sum = 0;//15
                    foreach (var item in foundRows)
                    {
                        if ((item.ItemArray[12] is DateTime) && ((DateTime)item.ItemArray[12] >= StartMonth[i - 1]) && ((DateTime)item.ItemArray[12]) < EndMonth[i - 1]) { Sum += Convert.ToInt32(item.ItemArray[17]); }
                    }
                    lol.Add(Sum);
                }
                
            
            return lol;
        }

        public List<int> return_all_values_per_month_SUM(int RangA, int RangB, string[] Divisions)
        {
            DataRow[] foundRows;
            int Sum = 0;
            foundRows = ds.Tables[1].Select();
            List<int> lol = new List<int>();

            for (int i = RangA; i <= RangB; i++)
            {
                Sum = 0;//15
                foreach (var item in foundRows)
                {
                    if ((item.ItemArray[12] is DateTime) && ((DateTime)item.ItemArray[12] >= StartMonth[i - 1]) && ((DateTime)item.ItemArray[12]) < EndMonth[i - 1]) { Sum += Convert.ToInt32(item.ItemArray[17]); }
                }
                lol.Add(Sum);
            }
            return lol;
        }
        #endregion
    }
}
