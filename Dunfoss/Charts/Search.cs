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
    public class Search
    {
        ICurrentFile currentFile = new EfCurrentFile();
        DataSet ds = new DataSet();
        public Search()
        {
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
            stream.Close();
        }

        public List<string> SearchByNumber(string number)
        {
            DataRow[] foundRows;
            List<string> Values = new List<string>();
            foundRows = ds.Tables[0].Select("[код RO] = '" + number + "' or [код RU] = '" + number + "'");
            try
            {
                Values.Add(foundRows[0].ItemArray[4].ToString());
                Values.Add(foundRows[0].ItemArray[5].ToString());
                Values.Add(foundRows[0].ItemArray[2].ToString());
                Values.Add(foundRows[0].ItemArray[6].ToString());
                Values.Add(foundRows[0].ItemArray[40].ToString() + " дней срок от начала дизайна до отправки на согласование");
                Values.Add(foundRows[0].ItemArray[41].ToString() + " дней срок согласования");
                Values.Add(foundRows[0].ItemArray[42].ToString() + " дней срок с момента задачи на размещение до отправки на согласование");
            }
            catch(Exception)
            {
                Values.Add("");
                Values.Add("");
                Values.Add("");
                Values.Add("");
                Values.Add("");
                Values.Add("");
                Values.Add("");
            }
            return Values;
        }
    }
}