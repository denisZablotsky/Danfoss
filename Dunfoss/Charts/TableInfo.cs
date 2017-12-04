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
    
    public class TableInfo
    {
        ICurrentFile currentFile = new EfCurrentFile();
        DataSet ds = new DataSet();
        public TableInfo()
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
        }
        public void Get_Total_Info(out int queue, out int in_the_design, out int agreement, out int to_factory)
        {
            //в очереди 9 
            queue = 0;
            in_the_design = 0;
            agreement = 0;
            to_factory = 0;
            DataRow[] foundRows;
            foundRows = ds.Tables[0].Select();
            for (int i = 0; i < foundRows.Length; i++)
            {
                if (foundRows[i].ItemArray[9].ToString() == "") { queue++; }
                if (foundRows[i].ItemArray[10].ToString() == "") { in_the_design++; }
                if (foundRows[i].ItemArray[11].ToString() == "") { agreement++; }
                if (foundRows[i].ItemArray[12].ToString() == "") { to_factory++; }
            }
        }

        public string[,] Get_More_Info_About_Total_Info()
        {
            int queue = 0, temp = 0;
            DataRow[] foundRows;
            foundRows = ds.Tables[0].Select();
            for (int i = 0; i < foundRows.Length; i++)
            {
                if ((foundRows[i].ItemArray[9].ToString() == "") || (foundRows[i].ItemArray[10].ToString() == "") || (foundRows[i].ItemArray[11].ToString() == "") || (foundRows[i].ItemArray[12].ToString() == ""))
                {
                    queue++;
                }
            }
            string[,] myArr = new string[queue, 9];
            for (int i = 0; i < foundRows.Length; i++)
            {
                if ((foundRows[i].ItemArray[9].ToString() == "") || (foundRows[i].ItemArray[10].ToString() == "") || (foundRows[i].ItemArray[11].ToString() == "") || (foundRows[i].ItemArray[12].ToString() == ""))
                {
                    myArr[temp, 0] = foundRows[i].ItemArray[1].ToString();
                    myArr[temp, 1] = foundRows[i].ItemArray[3].ToString();
                    myArr[temp, 2] = foundRows[i].ItemArray[4].ToString();
                    myArr[temp, 3] = foundRows[i].ItemArray[5].ToString();
                    myArr[temp, 4] = foundRows[i].ItemArray[6].ToString();
                    myArr[temp, 5] = foundRows[i].ItemArray[9].ToString();
                    myArr[temp, 6] = foundRows[i].ItemArray[10].ToString();
                    myArr[temp, 7] = foundRows[i].ItemArray[11].ToString();
                    myArr[temp, 8] = foundRows[i].ItemArray[12].ToString();
                    temp++;
                }
            }

            return myArr;
        }
    }
}