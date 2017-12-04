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
    public class DesignCities
    {
        ICurrentFile currentFile = new EfCurrentFile();
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
        DataSet ds = new DataSet();

        List<string> Cities = new List<string>();
        public DesignCities()
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
            DataRow[] foundRows;
            foundRows = ds.Tables[0].Select();
            for (int jе = 0; jе < foundRows.Length; jе++)
            {
                Cities.Add(ds.Tables[0].Rows[jе].ItemArray[8].ToString().TrimEnd(' '));
            }
            Cities = Cities.Distinct().ToList();
        }

        public IEnumerable<string> returnCitiesWeek(string TypeOfGraph, int RangA, int RangB)
        {
            List<string> Cities = new List<string>();
            IEnumerable<string> ret;
            DataRow[] foundRows;
            string query = "";
            for (int j = RangA; j <= RangB; j++)
            {
                if (TypeOfGraph == "O") { query = "(([O/P/S] = '" + TypeOfGraph + "' or [O/P/S] = 'О') and [неделя] = 'W" + j + "')"; }//англ\русс //8 элемент просто так 
                if (TypeOfGraph == "О") { query = "(([O/P/S] = '" + TypeOfGraph + "' or [O/P/S] = 'O') and [неделя] = 'W" + j + "')"; }//русс\англ 
                if (TypeOfGraph == "P") { query = "(([O/P/S] = '" + TypeOfGraph + "' or [O/P/S] = 'Р') and [неделя] = 'W" + j + "')"; }//англ\русс 
                if (TypeOfGraph == "Р") { query = "(([O/P/S] = '" + TypeOfGraph + "' or [O/P/S] = 'P') and [неделя] = 'W" + j + "')"; }//русс\англ 
                if (TypeOfGraph == "S") { query = "(([O/P/S] = '" + TypeOfGraph + "' and [неделя] = 'W" + j + "')"; }

                foundRows = ds.Tables[0].Select(query);
                for (int jе = 0; jе < foundRows.Length; jе++)
                {
                    Cities.Add(foundRows[jе].ItemArray[8].ToString().TrimEnd(' '));
                }

            }
            Cities = Cities.Distinct().ToList();
            ret = Cities;
            return ret;
        }

        public IEnumerable<string> returnCitiesMonth(string TypeOfGraph, int RangA, int RangB)
        {
            List<string> Cities = new List<string>();
            IEnumerable<string> ret = null;
            DataRow[] foundRows;
            string query = "";

            if (TypeOfGraph == "O") { query = "(([O/P/S] = '" + TypeOfGraph + "' or [O/P/S] = 'О'))"; }//англ\русс //8 элемент просто так 
            if (TypeOfGraph == "О") { query = "(([O/P/S] = '" + TypeOfGraph + "' or [O/P/S] = 'O'))"; }//русс\англ 
            if (TypeOfGraph == "P") { query = "(([O/P/S] = '" + TypeOfGraph + "' or [O/P/S] = 'Р'))"; }//англ\русс 
            if (TypeOfGraph == "Р") { query = "(([O/P/S] = '" + TypeOfGraph + "' or [O/P/S] = 'P'))"; }//русс\англ 
            if (TypeOfGraph == "S") { query = "(([O/P/S] = '" + TypeOfGraph + "')"; }

            foundRows = ds.Tables[0].Select(query);
            for (int j = RangA; j <= RangB; j++)
            {
                for (int jе = 0; jе < foundRows.Length; jе++)
                {
                    if ((foundRows[jе].ItemArray[19] is DateTime) && ((DateTime)foundRows[jе].ItemArray[19] >= StartMonth[j - 1]) && ((DateTime)foundRows[jе].ItemArray[19]) < EndMonth[j - 1])
                    {
                        Cities.Add(foundRows[jе].ItemArray[8].ToString().TrimEnd(' '));
                    }
                }
            }
            Cities = Cities.Distinct().ToList();
            ret = Cities;
            return ret;
        }

        public void Return_values_per_week(int RangA, int RangB, string TypeOfGraph, string[] CheckedValuesFromCities, out int[] all, out int[] good)
        {


            DataRow[] foundRows;

            all = new int[CheckedValuesFromCities.Length];
            good = new int[CheckedValuesFromCities.Length];
            int index = 0;

            int j = 0;
            string query = "";
            DateTime OvalueInDT, TvalueInDT;
            long Oint = 0;
            long Tint = 0;
            int count = 0;
            int AllValues = 0;
            foreach (var item in CheckedValuesFromCities)
            {

                count = 0;
                AllValues = 0;

                for (j = RangA; j <= RangB; j++)
                {
                    if (TypeOfGraph == "O") { query = "(([O/P/S] = '" + TypeOfGraph + "' or [O/P/S] = 'О') and [неделя] = 'W" + j + "') and [Регион] = '" + item + "'"; }//англ\русс //8 элемент просто так
                    if (TypeOfGraph == "О") { query = "(([O/P/S] = '" + TypeOfGraph + "' or [O/P/S] = 'O') and [неделя] = 'W" + j + "') and [Регион] = '" + item + "'"; }//русс\англ
                    if (TypeOfGraph == "P") { query = "(([O/P/S] = '" + TypeOfGraph + "' or [O/P/S] = 'Р') and [неделя] = 'W" + j + "') and [Регион] = '" + item + "'"; }//англ\русс
                    if (TypeOfGraph == "Р") { query = "(([O/P/S] = '" + TypeOfGraph + "' or [O/P/S] = 'P') and [неделя] = 'W" + j + "') and [Регион] = '" + item + "'"; }//русс\англ
                    if (TypeOfGraph == "S") { query = "(([O/P/S] = '" + TypeOfGraph + "' and [неделя] = 'W" + j + "') and [Регион] = '" + item + "'"; }
                    foundRows = ds.Tables[0].Select(query);

                    for (int i = 0; i < foundRows.Length; i++)
                    {
                        if (foundRows[i].ItemArray[19] is DateTime)
                        {
                            AllValues++;
                            object O = foundRows[i].ItemArray[14];
                            object T = foundRows[i].ItemArray[19];
                            if (O is DateTime) { OvalueInDT = (DateTime)O; Oint = OvalueInDT.ToFileTime(); }
                            if (T is DateTime) { TvalueInDT = (DateTime)T; Tint = TvalueInDT.ToFileTime(); }
                            long res = Oint - Tint;
                            if ((res == 0) || (res > 0))
                            {
                                count++;
                            }
                        }
                    }

                }
                all[index] = AllValues;
                good[index] = count;
                index++;

            }
        }

        public void Return_values_per_month(int RangA, int RangB, string TypeOfGraph, string[] CheckedValuesFromCities, out int[] all, out int[] good)
        {
            all = new int[CheckedValuesFromCities.Length];
            good = new int[CheckedValuesFromCities.Length];
            int index = 0;

            DataRow[] foundRows;
            string query = "";
            DateTime OvalueInDT, TvalueInDT;
            long Oint = 0;
            long Tint = 0;
            int count = 0;
            int AllValues = 0;
            foreach (var item in CheckedValuesFromCities)
            {
                count = 0;
                AllValues = 0;
                if (TypeOfGraph == "O") { query = "(([O/P/S] = '" + TypeOfGraph + "' or [O/P/S] = 'О') and [Регион] = '" + item + "')"; }//англ\русс
                if (TypeOfGraph == "О") { query = "(([O/P/S] = '" + TypeOfGraph + "' or [O/P/S] = 'O') and [Регион] = '" + item + "')"; }//русс\англ
                if (TypeOfGraph == "P") { query = "(([O/P/S] = '" + TypeOfGraph + "' or [O/P/S] = 'Р') and [Регион] = '" + item + "')"; }//англ\русс
                if (TypeOfGraph == "Р") { query = "(([O/P/S] = '" + TypeOfGraph + "' or [O/P/S] = 'P') and [Регион] = '" + item + "')"; }//русс\англ
                if (TypeOfGraph == "S") { query = "(([O/P/S] = '" + TypeOfGraph + "') and [Регион] = '" + item + "'"; }
                foundRows = ds.Tables[0].Select(query);
                for (int j = RangA; j <= RangB; j++)
                {

                    for (int i = 0; i < foundRows.Length; i++)
                    {
                        if ((foundRows[i].ItemArray[19] is DateTime) && ((DateTime)foundRows[i].ItemArray[19] >= StartMonth[j - 1]) && ((DateTime)foundRows[i].ItemArray[19]) < EndMonth[j - 1])
                        {
                            AllValues++;
                            object O = foundRows[i].ItemArray[14];
                            object T = foundRows[i].ItemArray[19];
                            if (O is DateTime) { OvalueInDT = (DateTime)O; Oint = OvalueInDT.ToFileTime(); }
                            if (T is DateTime) { TvalueInDT = (DateTime)T; Tint = TvalueInDT.ToFileTime(); }
                            long res = Oint - Tint;
                            if ((res == 0) || (res > 0))
                            {
                                count++;
                            }
                        }
                    }
                }
                all[index] = AllValues;
                good[index] = count;
                index++;
            }
        }

    }
}