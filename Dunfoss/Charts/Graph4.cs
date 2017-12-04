using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using Dunfoss.Data;

namespace Dunfoss.Charts
{
    public class Graph4
    {
        ICurrentFile currentFile = new EfCurrentFile();

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
        public Graph4()
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
            string path = HostingEnvironment.ApplicationPhysicalPath + currentFile.GetCurrentFile().Path3;
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
            ds = dataReader.AsDataSet(conf);
            dataReader.Close();
            for (int i = 1; i < 13; i++)
            {
                var temp = new DateTime(2017, i, 1, 00, 00, 00);
                StartMonth.Add(temp);
            }
        }
        public void Return_values_per_month(int RangA, int RangB, out List<object> Designers, out List<float> RESULT)
        {
            Dictionary<string, float> assessment = new Dictionary<string, float>();
            DataRow[] foundRows;
            foundRows = ds.Tables[0].Select();
            RESULT = new List<float>();
            Designers = new List<object>();
            foreach (var item in foundRows)
            {
                for (int r = RangA; r <= RangB; r++)
                {
                    if ((item.ItemArray[19] is DateTime) && ((DateTime)item.ItemArray[19] >= StartMonth[r - 1]) && ((DateTime)item.ItemArray[19]) < EndMonth[r - 1])
                    {
                        Designers.Add(item.ItemArray[2]);
                    }
                }
            }
            Designers = Designers.Distinct().ToList();//Все уникальные дизайнеры в диапазоне месяцов
            DateTime OvalueInDT, TvalueInDT;
            long Oint = 0;
            long Tint = 0;
            float count = 0;
            float AllValues = 0;
            foreach (var item in Designers)
            {
                count = 0;
                AllValues = 0;
                for (int j = RangA; j <= RangB; j++)
                {
                    for (int i = 0; i < foundRows.Length; i++)
                    {
                        if ((foundRows[i].ItemArray[19] is DateTime) && ((DateTime)foundRows[i].ItemArray[19] >= StartMonth[j - 1]) && ((DateTime)foundRows[i].ItemArray[19]) < EndMonth[j - 1] && (foundRows[i].ItemArray[2].ToString() == item.ToString()))
                        {
                            AllValues++;
                            object O = foundRows[i].ItemArray[16];
                            object T = foundRows[i].ItemArray[17];
                            if (O is DateTime) { OvalueInDT = (DateTime)O; Oint = OvalueInDT.ToFileTime(); }
                            if (T is DateTime) { TvalueInDT = (DateTime)T; Tint = TvalueInDT.ToFileTime(); }
                            long res = Tint - Oint;
                            DateTime start = new DateTime(res);
                            if ((res == 0))
                            {
                                count++;
                            }
                            else
                            {
                                var a = start.Day;
                                count += a;
                            }
                        }
                    }
                }
                RESULT.Add(count / AllValues);
                assessment.Add(item.ToString(), count / AllValues);//для наглядности)
            }
        }

        public void Return_values_per_week(int RangA, int RangB, out List<object> Designers, out List<float> RESULT)
        {
            Dictionary<string, float> assessment = new Dictionary<string, float>();
            DataRow[] foundRows;
            foundRows = ds.Tables[0].Select();
            RESULT = new List<float>();
            Designers = new List<object>();
            foreach (var item in foundRows)
            {
                for (int r = RangA; r <= RangB; r++)
                {
                    if ((item.ItemArray[43].ToString() == "W" + r))
                    {
                        Designers.Add(item.ItemArray[2]);
                    }
                }
            }
            Designers = Designers.Distinct().ToList();//Все уникальные дизайнеры в диапазоне месяцов
            DateTime OvalueInDT, TvalueInDT;
            long Oint = 0;
            long Tint = 0;
            float count = 0;
            float AllValues = 0;
            foreach (var item in Designers)
            {
                count = 0;
                AllValues = 0;
                for (int j = RangA; j <= RangB; j++)
                {
                    for (int i = 0; i < foundRows.Length; i++)
                    {
                        if ((foundRows[i].ItemArray[43].ToString() == "W" + j) && (foundRows[i].ItemArray[2].ToString() == item.ToString()))
                        {
                            AllValues++;
                            object O = foundRows[i].ItemArray[16];
                            object T = foundRows[i].ItemArray[17];
                            if (O is DateTime) { OvalueInDT = (DateTime)O; Oint = OvalueInDT.ToFileTime(); }
                            if (T is DateTime) { TvalueInDT = (DateTime)T; Tint = TvalueInDT.ToFileTime(); }
                            long res = Tint - Oint;
                            DateTime start = new DateTime(res);
                            if ((res == 0))
                            {
                                count++;
                            }
                            else
                            {
                                var a = start.Day;
                                count += a;
                            }
                        }
                    }
                }
                RESULT.Add(count / AllValues);
                assessment.Add(item.ToString(), count / AllValues);//для наглядности)
            }
        }
    }
}