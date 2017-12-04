using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Web;
using System.Web.Hosting;

namespace Dunfoss.Charts
{
    public class ChartFormat45
    {

        DataSet ds = new DataSet();
        public List<int> weeks = new List<int>();
        List<string> reasons = new List<string>();
        object reader;
        public IEnumerable<string> Realreasons;
        private OleDbConnection conn;
        private string filename = HostingEnvironment.ApplicationPhysicalPath + "/xls/" + "1.xls";

        public ChartFormat45(string path)
        {
            if (path != null)
                filename = path;
            conn = new OleDbConnection
            {
                ConnectionString =
                    @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source = '" + filename + "'" +
                    @";Extended Properties=""Excel 8.0;HDR=YES;IMEX=1;ImportMixedTypes=Text;TypeGuessRows=0"""
            };
            conn.Open();

        }

        public IEnumerable<string> getReasons(int RangeA, int RangeB, int type1)
        {
            var fag = new OleDbCommand("SELECT DISTINCT F" + type1 + ", F18 from [расчеты$]", conn);
            OleDbDataAdapter da = new OleDbDataAdapter(fag);
            da.Fill(ds);
            var TableWithReasons = ds.Tables[0].Select();

            for (int i = RangeA; i <= RangeB; i++)
            {
                weeks.Add(i);
            }
            foreach (var item in weeks)
            {
                for (int i = 0; i < TableWithReasons.Count(); i++)
                {
                    if (ds.Tables[0].Rows[i].ItemArray[1].ToString() == "W" + item) { reasons.Add(ds.Tables[0].Rows[i].ItemArray[0].ToString()); }
                }
            }
            Realreasons = reasons.Distinct();
            return Realreasons;
        }

        public void CreatePointsAtAllWeek(string[] reasons, int RangeA, int RangeB, out int[]GoodReader, int type1, int type2)//возвращает сумму столбца AN для каждой причины в диапазоне недель
        {
            GoodReader = new int[reasons.Length];
            for (int i = 0; i < reasons.Count(); i++)
            {
                
                for (int j = RangeA; j <= RangeB; j++)
                {
                    string querry = "SELECT SUM([F" + type2 + "]) FROM [расчеты$] where F" + type1 +" = '" + reasons[i] + "' and F18 = 'W" + j + "'";
                    var fag = new OleDbCommand(querry, conn);
                    reader = fag.ExecuteScalar();
                    if (reader.ToString() != "") { GoodReader[i] += int.Parse(reader.ToString()); }
                }
            }
           
        }

    }
}