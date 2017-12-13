using System;
using System.IO;
using System.Data;
using ExcelDataReader;
using System.Web.Hosting;
using Dunfoss.Data;
using System.Collections.Generic;

// 2017 в коде
// 12 месяц никогда не включается

namespace Dunfoss.Charts
{
    public class FirstTypeChart
    {
        DataSet ds = new DataSet();

        List<DateTime> EndMonth = new List<DateTime>();
        List<DateTime> StartMonth = new List<DateTime>();
        ICurrentFile currentFile;

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
        //Dictionary<int, int> WeeK_GoodsuM = new Dictionary<int, int>();
        int type = 0;

        public FirstTypeChart(int type)
        {
            currentFile = new EfCurrentFile();

            this.type = type;

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

            for (int i = 1; i < 13; i++)
            {
                var temp = new DateTime(2017, i, 1, 00, 00, 00);
                StartMonth.Add(temp);
            }


            ds = dataReader.AsDataSet();
            dataReader.Close();
        }

        public void return_good_values_weekly(int RangA, int RangB, out int[] good)
        {

            good = new int[RangB - RangA + 1];
            int index = 0;

            DataRow[] a;
            List<object> obj = new List<object>();
            int Sum = 0;

            for (int i = RangA; i <= RangB; i++)
            {
                obj.Clear();
                Sum = 0;
                a = ds.Tables[1].Select("[column18] IN ('0day', '1day', '2day', '3day') and [column17] = 'W" + i + "'");
                for (int j = 0; j < a.Length; j++)
                {
                    obj.Add(a[j].ItemArray[type]);
                }
                foreach (var item in obj)
                {
                    if (item.ToString() != "")
                        Sum += Convert.ToInt32(item);
                }
                good[index] = Sum;
                index++;
            }
        }


        public void return_all_values_weekly(int RangA, int RangB, out int[] all)
        {
            all = new int[RangB - RangA + 1];
            int index = 0;

            DataRow[] a;
            List<object> obj = new List<object>();
            int Sum = 0;
            for (int i = RangA; i <= RangB; i++)
            {
                obj.Clear();
                Sum = 0;
                a = ds.Tables[1].Select("[column17] = 'W" + i + "'");
                for (int j = 0; j < a.Length; j++)
                {
                    obj.Add(a[j].ItemArray[type]);
                }
                foreach (var item in obj)
                {
                    if (item.ToString() != "")
                        Sum += Convert.ToInt32(item);
                }
                all[index] = Sum;
                index++;
            }

        }


        public void return_all_values_per_month(int RangA, int RangB, out int[] all)
        {
            all = new int[RangB - RangA + 1];
            int index = 0;
            
            DataRow[] foundRows;
            int Sum = 0;
            List<DataRow> lol = new List<DataRow>();
            Dictionary<int, int> MontH_CounT = new Dictionary<int, int>();
            List<object> obj = new List<object>();
            for (int i = RangA; i <= RangB && i <= 12; i++)
            {
                obj.Clear();
                lol.Clear();
                Sum = 0;//13
                foundRows = ds.Tables[1].Select();
                foreach (var item in foundRows)
                {
                    if ((item.ItemArray[13] is DateTime) && ((DateTime)item.ItemArray[13] >= StartMonth[i - 1]) && ((DateTime)item.ItemArray[13]) < EndMonth[i - 1])
                    {
                        lol.Add(item);
                    }
                }
                foundRows = lol.ToArray();
                for (int j = 0; j < foundRows.Length; j++)
                {
                    obj.Add(foundRows[j].ItemArray[type]);
                }
                foreach (var item in obj)
                {
                    if (item.ToString() != "")
                        Sum += Convert.ToInt32(item);
                }
                all[index] = Sum;
                index++;
            }
        }


        public void return_good_values_per_month(int RangA, int RangB, out int[] good)
        {
            good = new int[RangB - RangA + 1];
            int index = 0;
            
            DataRow[] foundRows;
            int Sum = 0;
            List<DataRow> lol = new List<DataRow>();
            Dictionary<int, int> MontH_CounT = new Dictionary<int, int>();
            List<object> obj = new List<object>();
            for (int i = RangA; i <= RangB && i <= 12; i++)
            {
                obj.Clear();
                lol.Clear();
                Sum = 0;//13
                foundRows = ds.Tables[1].Select("[column18] IN ('1day', '2day', '3day')");
                foreach (var item in foundRows)
                {
                    if ((item.ItemArray[13] is DateTime) && ((DateTime)item.ItemArray[13] >= StartMonth[i - 1]) && ((DateTime)item.ItemArray[13]) < EndMonth[i - 1])
                    {
                        lol.Add(item);
                    }
                }
                foundRows = lol.ToArray();
                for (int j = 0; j < foundRows.Length; j++)
                {
                    obj.Add(foundRows[j].ItemArray[type]);
                }
                foreach (var item in obj)
                {
                    if (item.ToString() != "")
                        Sum += Convert.ToInt32(item);
                }
                good[index] = Sum;
                index++;
            }
        }

    }
}