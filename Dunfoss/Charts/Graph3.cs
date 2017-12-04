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
    public class Graph3
    {
        ICurrentFile currentFile = new EfCurrentFile();

        DataSet ds = new DataSet();
        List<object> Designers;
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
        public Graph3()
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
            for (int i = 1; i < 13; i++)
            {
                var temp = new DateTime(2017, i, 1, 00, 00, 00);
                StartMonth.Add(temp);
            }
            ds = dataReader.AsDataSet(conf);
            dataReader.Close();
        }

        public void Return_values_per_week(int Weeknumber, out List<string> Model_Designer_Name, out List<object> Good, out List<object> Medium, out List<object> Poor)
        {
            DataRow[] foundRows;
            foundRows = ds.Tables[0].Select("[неделя] = 'W" + Weeknumber + "'");
            List<object> Designers = new List<object>();
            List<object> Model = new List<object>();
            foreach (var item in foundRows)
            {
                Designers.Add(item.ItemArray[2]);
                Model.Add(item.ItemArray[3]);
            }
            Designers = Designers.Distinct().ToList();//Все уникальные дизайнеры в диапазоне месяцов
            Model = Model.Distinct().ToList();
            Model_Designer_Name = new List<string>();
            Good = new List<object>();
            Medium = new List<object>();
            Poor = new List<object>();
            float good = 0;
            float medium = 0;
            object qw = null;
            float poor = 0;
            foreach (var elem in Model)
            {
                good = 0;
                medium = 0;
                poor = 0;
                foreach (var item in Designers)
                {
                    for (int i = 0; i < foundRows.Length; i++)
                    {
                        if ((foundRows[i].ItemArray[2].ToString() == item.ToString()) && (foundRows[i].ItemArray[3].ToString() == elem.ToString()))
                        {
                            if (foundRows[i].ItemArray[44].ToString() == "good") { good++; } //else { Good.Add(0); }
                            if (foundRows[i].ItemArray[44].ToString() == "medium") { medium++; } //else { Medium.Add(0); }
                            if (foundRows[i].ItemArray[44].ToString() == "poor") { poor++; } //else { Poor.Add(0); }
                            qw = item.ToString();
                        }
                    }
                }
                Good.Add(good);
                Poor.Add(poor);
                Medium.Add(medium);
                Model_Designer_Name.Add(elem.ToString() + "_" + qw);
            }
        }

        public void Return_values_per_month(int Monthnumber, out List<string> Model_Designer_Name, out List<object> Good, out List<object> Medium, out List<object> Poor)
        {
            List<DataRow> foundRows = new List<DataRow>();
            DataRow[] foundRows1;
            foundRows1 = ds.Tables[0].Select();
            foreach (var item in foundRows1)
            {
                if ((item.ItemArray[19] is DateTime) && ((DateTime)item.ItemArray[19] >= StartMonth[Monthnumber - 1]) && ((DateTime)item.ItemArray[19]) < EndMonth[Monthnumber - 1])
                {
                    foundRows.Add(item);
                }

            }
            //RESULT = new List<float>();
            Designers = new List<object>();
            List<object> Model = new List<object>();
            foreach (var item in foundRows)
            {
                Designers.Add(item.ItemArray[2]);
                Model.Add(item.ItemArray[3]);
            }
            Designers = Designers.Distinct().ToList();//Все уникальные дизайнеры в диапазоне месяцов
            Model = Model.Distinct().ToList();//Все уникальные модели в диапазоне месяцов
            Model_Designer_Name = new List<string>();
            Good = new List<object>();
            Medium = new List<object>();
            Poor = new List<object>();
            float good = 0;
            float medium = 0;
            object qw = null;
            float poor = 0;
            foreach (var elem in Model)
            {
                good = 0;
                medium = 0;
                poor = 0;
                foreach (var item in Designers)
                {
                    for (int i = 0; i < foundRows.Count; i++)
                    {
                        if ((foundRows[i].ItemArray[2].ToString() == item.ToString()) && (foundRows[i].ItemArray[3].ToString() == elem.ToString()))
                        {
                            if (foundRows[i].ItemArray[44].ToString() == "good") { good++; } //else { Good.Add(0); }
                            if (foundRows[i].ItemArray[44].ToString() == "medium") { medium++; } //else { Medium.Add(0); }
                            if (foundRows[i].ItemArray[44].ToString() == "poor") { poor++; } //else { Poor.Add(0); }
                            qw = item.ToString();
                        }
                    }
                }
                Good.Add(good);
                Poor.Add(poor);
                Medium.Add(medium);
                Model_Designer_Name.Add(elem.ToString() + "_" + qw);
            }
        }
    }
}