using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using Dunfoss.Data;

namespace Dunfoss.Charts
{
    public class ChartFormat67
    {
        ICurrentFile currentFile = new EfCurrentFile();
        List<DateTime> EndMonth = new List<DateTime>();
        List<DateTime> StartMonth = new List<DateTime>();
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
        DataSet ds = new DataSet();
        

        public ChartFormat67()
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
            string path = HostingEnvironment.ApplicationPhysicalPath + currentFile.GetCurrentFile().Path1;
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

        public int[] return_good_values_for_reasons_in_month_range(int RangA, int RangB, string[] Division)
        {
            DataRow[] foundRows;
            int Sum = 0;

            int[] good = new int[Division.Length];
            int index = 0;

            foundRows = ds.Tables[1].Select("[column18] IN ('0day','1day', '2day', '3day')");
            foreach (var elem in Division)
            {
                Sum = 0;
                for (int i = RangA; i <= RangB; i++)
                {
                    foreach (var item in foundRows)
                    {
                        if ((item.ItemArray[0].ToString().TrimEnd(' ') == elem) && (item.ItemArray[39].ToString() != "") && ((DateTime)item.ItemArray[13] >= StartMonth[i - 1]) && ((DateTime)item.ItemArray[13]) < EndMonth[i - 1]) { Sum += Convert.ToInt32(item.ItemArray[39]); }
                    }
                }
                good[index] = Sum;
                index++;
            }
            return good;
        }

        public int[] return_all_values_for_reasons_in_month_range(int RangA, int RangB, string[] Division)
        {
            DataRow[] foundRows;
            int Sum = 0;

            int[] all = new int[Division.Length];
            int index = 0;

            foundRows = ds.Tables[1].Select();
            foreach (var elem in Division)
            {
                Sum = 0;
                for (int i = RangA; i <= RangB; i++)
                {
                    foreach (var item in foundRows)
                    {
                        if ((item.ItemArray[0].ToString().TrimEnd(' ') == elem) && (item.ItemArray[39].ToString() != "") && ((DateTime)item.ItemArray[13] >= StartMonth[i - 1]) && ((DateTime)item.ItemArray[13]) < EndMonth[i - 1]) { Sum += Convert.ToInt32(item.ItemArray[39]); }
                    }
                }
                all[index] = Sum;
                index++;
            }
            return all;
        }
    }
}