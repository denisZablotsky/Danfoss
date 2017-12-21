using System;
using System.IO;
using System.Linq;
using System.Data;
using ExcelDataReader;
using System.Web.Hosting;
using Dunfoss.Data;
using System.Collections.Generic;


namespace Dunfoss.Charts
{
    public class ReasonsChart
    {
        ICurrentFile currentFile = new EfCurrentFile();
        DataSet ds = new DataSet();
        IEnumerable<string> reasons;
        string path;

        public ReasonsChart()
        {
            path = HostingEnvironment.ApplicationPhysicalPath + currentFile.GetCurrentFile().Path1;

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

            ds = dataReader.AsDataSet();
            dataReader.Close();
        }
        //

        public IEnumerable<string> Return_all_reasons5(int RangA, int RangB)
        {
            DataRow[] foundRows;
            List<string> re = new List<string>();
            
            foundRows = ds.Tables[1].Select("[column21] NOT IN ('причина корректировки расчета')");
            foreach (var item in foundRows)
            {
                for (int r = RangA; r <= RangB; r++)
                {
                    if ((item.ItemArray[21].ToString() != "") && (item.ItemArray[17].ToString() == "W" + r)) {re.Add(item.ItemArray[21].ToString()); }
                }
            }
            reasons = re.Distinct();
            return reasons;//Все уникальные причины в диапазоне недель 
        }

        public IEnumerable<string> Return_all_reasons4(int RangA, int RangB)
        {
            DataRow[] foundRows;
            List<string> re = new List<string>();
            foundRows = ds.Tables[1].Select("[column18] NOT IN ('0day','1day', '2day', '3day', 'время закрытия задачи')");
            foreach (var item in foundRows)
            {
                for (int r = RangA; r <= RangB; r++)
                {
                    if ((item.ItemArray[20].ToString() != "") && (item.ItemArray[17].ToString() == "W" + r)) { re.Add(item.ItemArray[20].ToString()); }
                }
            }
            reasons = re.Distinct();
            return reasons;//Все уникальные причины в диапазоне недель 
        }

        public void Values_per_reasons_for4graph(int RangA, int RangB, string[] reasons, out int[]values)//возвращает причины и их значения в диапазоне недель
        {
            DataRow[] foundRows;
            int Sum = 0;
            foundRows = ds.Tables[1].Select("[column18] NOT IN ('0day','1day', '2day', '3day', 'время закрытия задачи')");
            values = new int[reasons.Length];
            int index = 0;

            foreach (string item in reasons)
            {
                Sum = 0;
                for (int j = 0; j < foundRows.Length; j++)
                {
                    for (int f = RangA; f <= RangB; f++)
                    {
                        if (foundRows[j].ItemArray[20].ToString() == item.ToString() && ((foundRows[j].ItemArray[17].ToString() == "W" + f))) { Sum += Convert.ToInt32(foundRows[j].ItemArray[39]); }
                    }
                }
                values[index] = Sum;
                index++;
                //valuesforreasons.Add(Sum);//это сумма в диапазоне недель по определенной причине, 
                //соответствует reasons <reasons[i],valuesforreasons[i]> то есть первой причине в reasons соответствует первое значение в valuesforreasons
            }
        }

        public void Values_per_reasons_for5graph(int RangA, int RangB, string[] reasons, out int[] values)//возвращает причины и их значения в диапазоне недель
        {
            DataRow[] foundRows;
            int Sum = 0;

            Sum = 0;//20 17
            foundRows = ds.Tables[1].Select("[column21] NOT IN ('причина корректировки расчета')");
            
            values = new int[reasons.Length];
            int index = 0;

            foreach (var item in reasons)
            {
                Sum = 0;
                for (int j = 0; j < foundRows.Length; j++)
                {
                    for (int f = RangA; f <= RangB; f++)
                    {
                        if (foundRows[j].ItemArray[21].ToString() == item.ToString() && (foundRows[j].ItemArray[39].ToString() != "") && ((foundRows[j].ItemArray[17].ToString() == "W" + f))) { Sum += Convert.ToInt32(foundRows[j].ItemArray[39]); }
                    }
                }
                values[index] = Sum;
                index++;
                //valuesforreasons.Add(Sum);//это сумма в диапазоне недель по определенной причине, 
                //соответствует reasons <reasons[i],valuesforreasons[i]> то есть первой причине в reasons соответствует первое значение в valuesforreasons
            }
        }
    }
}