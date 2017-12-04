using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web.Hosting;
using Dunfoss.Data;

namespace Dunfoss.Charts
{
    class DesignChart
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

        public DesignChart()
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

        public void Return_values_per_month(int RangA, int RangB, string TypeOfGraph, out int[] all, out int[] good)
        {


            all = new int[RangB - RangA + 1];
            good = new int[RangB - RangA + 1];
            int index = 0;

            DataRow[] foundRows;
            string query = "";
            if (TypeOfGraph == "O") { query = "([O/P/S] = '" + TypeOfGraph + "' or [O/P/S] = 'О')"; }//англ\русс
            if (TypeOfGraph == "О") { query = "([O/P/S] = '" + TypeOfGraph + "' or [O/P/S] = 'O')"; }//русс\англ
            if (TypeOfGraph == "P") { query = "([O/P/S] = '" + TypeOfGraph + "' or [O/P/S] = 'Р')"; }//англ\русс
            if (TypeOfGraph == "Р") { query = "([O/P/S] = '" + TypeOfGraph + "' or [O/P/S] = 'P')"; }//русс\англ
            if (TypeOfGraph == "S") { query = "([O/P/S] = '" + TypeOfGraph + "')"; }
            DateTime OvalueInDT, TvalueInDT;
            long Oint = 0;
            long Tint = 0;
            int count = 0;
            int AllValues = 0;
            foundRows = ds.Tables[0].Select(query);
            for (int j = RangA; j <= RangB; j++)
            {
                count = 0;
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

                all[index] = AllValues;
                good[index] = count;
                index++;
                AllValues = 0;
            }
        }


        public void Return_values_per_week(int RangA, int RangB, string TypeOfGraph, out int[] all, out int[] good)
        {
            all = new int[RangB - RangA + 1];
            good = new int[RangB - RangA + 1];
            int index = 0;

            DataRow[] foundRows;
            int j = 0;
            string query = "";
            DateTime OvalueInDT, TvalueInDT;
            long Oint = 0;
            long Tint = 0;
            int count = 0;
            int AllValues = 0;

            for (j = RangA; j <= RangB; j++)
            {
                if (TypeOfGraph == "O") { query = "(([O/P/S] = '" + TypeOfGraph + "' or [O/P/S] = 'О') and [неделя] = 'W" + j + "')"; }//англ\русс
                if (TypeOfGraph == "О") { query = "(([O/P/S] = '" + TypeOfGraph + "' or [O/P/S] = 'O') and [неделя] = 'W" + j + "')"; }//русс\англ
                if (TypeOfGraph == "P") { query = "(([O/P/S] = '" + TypeOfGraph + "' or [O/P/S] = 'Р') and [неделя] = 'W" + j + "')"; }//англ\русс
                if (TypeOfGraph == "Р") { query = "(([O/P/S] = '" + TypeOfGraph + "' or [O/P/S] = 'P') and [неделя] = 'W" + j + "')"; }//русс\англ
                if (TypeOfGraph == "S") { query = "([O/P/S] = '" + TypeOfGraph + "' and [неделя] = 'W" + j + "')"; }
                foundRows = ds.Tables[0].Select(query);
                count = 0;
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
                all[index] = AllValues;
                good[index] = count;
                index++;
                AllValues = 0;
            }
            { }
        }
    }
}
