using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.OleDb;
using System.IO;
using System.Data;
using System.Text;
using System.Web.Hosting;

namespace Dunfoss.Charts
{
    public class ChartFormat
    {
        private OleDbConnection conn;
        private string filename = HostingEnvironment.ApplicationPhysicalPath + "/xls/" + "1.xls";

        public ChartFormat(string path)
        {
            if(path != null)
                filename = path;
            conn = new OleDbConnection
            {
                ConnectionString =
                    @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source = '" + filename + "'" +
                    @";Extended Properties=""Excel 8.0;HDR=YES;IMEX=1;ImportMixedTypes=Text;TypeGuessRows=0"""
            };
            conn.Open();
        }

        public void CreateGraphWithMonthFilter(out object[] ALLMonthvalues, out object[] GoodMonthvalues, int min, int max, int type)
        {
            

            object z = null;
            ALLMonthvalues = new object[max - min + 1];
            int countA = 0;
            GoodMonthvalues = new object[max - min + 1];
            int countG = 0;
            object valuefrommonth, goodvaluefrommonth;
            for (int i = min; i <= max; i++)
            {
                valuefrommonth = return_all_values_per_month(i, type);
                goodvaluefrommonth = return_good_values_per_month(i, type);
                if (valuefrommonth != z)
                {
                    ALLMonthvalues[countA] = valuefrommonth;
                    countA++;
                }
                else
                {
                    ALLMonthvalues[countA] = null;
                    countA++;
                }
                if (goodvaluefrommonth != z)
                {
                    GoodMonthvalues[countG] = goodvaluefrommonth;
                    countG++;
                }
                else
                {
                    GoodMonthvalues[countG] = null;
                    countG++;
                }
            }

        }

        public void CreateGraphWithWeekFilter(out object[] ALLWeekvalues, out object[] GoodWeekvalues, int min, int max, int type)
        {

            object zz = null;
            ALLWeekvalues = new object[max - min + 1];
            int countA = 0;
            GoodWeekvalues = new object[max - min + 1];
            int countG = 0;
            object valuefromweek, goodvaluefromweek;
            for (int i = min; i < max; i++)
            {
                valuefromweek = return_all_values_weekly(i, type);
                goodvaluefromweek = return_good_values_weekly(i, type);
                if (valuefromweek != zz)
                {
                    ALLWeekvalues[countA] = valuefromweek;
                    countA++;
                }
                else
                {
                    ALLWeekvalues[countA] = null;
                    countA++;
                }
                if (goodvaluefromweek != zz)
                {
                    GoodWeekvalues[countG] = goodvaluefromweek;
                    countG++;
                }
                else
                {
                    GoodWeekvalues[countG] = null;
                    countG++;
                }
            }

        }

        public object return_all_values_weekly(int weekNumber, int type)
        {
            string querry = "SELECT SUM([F" + type.ToString() + "]) FROM [расчеты$] where F18 = 'W" + weekNumber + "'";
            var fag = new OleDbCommand(querry, conn);
            var reader = fag.ExecuteScalar();
            return reader;
        }

        public object return_good_values_weekly(int weekNumber, int type)
        {
            string querry = "SELECT SUM([F" + type.ToString() + "]) from [расчеты$] where F18 = 'W" + weekNumber + "' and (F19 = '1day' or F19 = '2day' or F19 = '3day')";
            var fag = new OleDbCommand(querry, conn);
            var reader = fag.ExecuteScalar();
            return reader;
        }

        public object return_all_values_per_month(int monthNumber, int type)
        {
            string querry = "SELECT SUM([F" + type.ToString() + "]) FROM [расчеты$] where F14 LIKE '%/" + monthNumber + "/%'";
            var fag = new OleDbCommand(querry, conn);
            var reader = fag.ExecuteScalar();
            return reader;
        }

        public object return_good_values_per_month(int monthNumber, int type)
        {
            string querry = "SELECT SUM([F" + type.ToString() + "]) from [расчеты$] where F14 LIKE '%/" + monthNumber + "/%' and (F19 = '1day' or F19 = '2day' or F19 = '3day')";
            var fag = new OleDbCommand(querry, conn);
            var reader = fag.ExecuteScalar();
            return reader;
        }
    


    }
}