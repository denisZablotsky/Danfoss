using Dunfoss.Data;
using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;

namespace Dunfoss.Charts
{
    public class TheLastChart
    {
        List<DateTime> EndMonth = new List<DateTime>();
        List<DateTime> StartMonth = new List<DateTime>();
        List<int> values;
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
        List<string> reasons;
        DataSet ds = new DataSet();
        public TheLastChart()
        {
            ICurrentFile currentFile = new EfCurrentFile();
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


            for (int i = 1; i < 13; i++)
            {
                var temp = new DateTime(2017, i, 1, 00, 00, 00);
                StartMonth.Add(temp);
            }
            ds = dataReader.AsDataSet();
            dataReader.Close();
        }


        //Построение первого графика
        #region
        public void First_Type_Graph_Per_Week_Count(int RangA, int RangB, out int[] Good, out int[] Bad, string[] surnames)
        {
            var Surnames_In_Range = surnames.ToList<string>();
            //здесь будет массив стрингов или что то вроде него, в котором будут фамилии выбранные из всех, 
            //их тебе и нежно передать в функцию ниже
            List<int> GoodValues = Get_Good_Values_First_Type_Graph_Per_Week_Count(RangA, RangB, Surnames_In_Range);//пока что передаются все фамилии из диапазона
            List<int> BadValues = Get_Bad_Values_First_Type_Graph_Per_Week_Count(RangA, RangB, Surnames_In_Range);//пока что передаются все фамилии из диапазона
            Good = new int[GoodValues.Count];
            Bad = new int[BadValues.Count];
            GoodValues.CopyTo(Good);
            BadValues.CopyTo(Bad);
        }

        public void First_Type_Graph_Per_Week_Sum(int RangA, int RangB, out int[] Good, out int[] Bad, string[] surnames)
        {
            var Surnames_In_Range = surnames.ToList<string>();
            //здесь будет массив стрингов или что то вроде него, в котором будут фамилии выбранные из всех, 
            //их тебе и нежно передать в функцию ниже
            List<int> GoodValues = Get_Good_Values_First_Type_Graph_Per_Week_Sum(RangA, RangB, Surnames_In_Range);//пока что передаются все фамилии из диапазона
            List<int> BadValues = Get_Bad_Values_First_Type_Graph_Per_Week_Sum(RangA, RangB, Surnames_In_Range);//пока что передаются все фамилии из диапазона
            Good = new int[GoodValues.Count];
            Bad = new int[BadValues.Count];
            GoodValues.CopyTo(Good);
            BadValues.CopyTo(Bad);
        }

        public void First_Type_Graph_Per_Month_Count(int RangA, int RangB, out int[] Good, out int[] Bad, string[] surnames)
        {
            var Surnames_In_Range = surnames.ToList<string>();
            //здесь будет массив стрингов или что то вроде него, в котором будут фамилии выбранные из всех, 
            //их тебе и нежно передать в функцию ниже
            List<int> GoodValues = Get_Good_Values_First_Type_Graph_Per_Month_Count(RangA, RangB, Surnames_In_Range);//пока что передаются все фамилии из диапазона
            List<int> BadValues = Get_Bad_Values_First_Type_Graph_Per_Month_Count(RangA, RangB, Surnames_In_Range);//пока что передаются все фамилии из диапазона
            Good = new int[GoodValues.Count];
            Bad = new int[BadValues.Count];
            GoodValues.CopyTo(Good);
            BadValues.CopyTo(Bad);
        }

        public void First_Type_Graph_Per_Month_Sum(int RangA, int RangB, out int[] Good, out int[] Bad, string[] surnames)
        {
            var Surnames_In_Range = surnames.ToList<string>();
            //здесь будет массив стрингов или что то вроде него, в котором будут фамилии выбранные из всех, 
            //их тебе и нежно передать в функцию ниже
            List<int> GoodValues = Get_Good_Values_First_Type_Graph_Per_Month_Sum(RangA, RangB, Surnames_In_Range);//пока что передаются все фамилии из диапазона
            List<int> BadValues = Get_Bad_Values_First_Type_Graph_Per_Month_Sum(RangA, RangB, Surnames_In_Range);//пока что передаются все фамилии из диапазона
            Good = new int[GoodValues.Count];
            Bad = new int[BadValues.Count];
            GoodValues.CopyTo(Good);
            BadValues.CopyTo(Bad);
        }
        #endregion

        //Построение второго графика
        #region
        public void Second_Type_Graph_Per_Week_Count(int RangA, int RangB, out int[] Good, out int[] Bad, string[] surnames)
        {
            var Surnames_In_Range = surnames.ToList<string>();
            //здесь будет массив стрингов или что то вроде него, в котором будут фамилии выбранные из всех, 
            //их тебе и нежно передать в функцию ниже
            List<int> GoodValues = Get_Good_Values_Second_Type_Graph_Per_Week_Count(RangA, RangB, Surnames_In_Range);//пока что передаются все фамилии из диапазона
            List<int> BadValues = Get_Bad_Values_Second_Type_Graph_Per_Week_Count(RangA, RangB, Surnames_In_Range);//пока что передаются все фамилии из диапазона
            Good = new int[GoodValues.Count];
            Bad = new int[BadValues.Count];
            GoodValues.CopyTo(Good);
            BadValues.CopyTo(Bad);
        }

        public void Second_Type_Graph_Per_Week_Sum(int RangA, int RangB, out int[] Good, out int[] Bad, string[] surnames)
        {
            var Surnames_In_Range = surnames.ToList<string>();
            //здесь будет массив стрингов или что то вроде него, в котором будут фамилии выбранные из всех, 
            //их тебе и нежно передать в функцию ниже
            List<int> GoodValues = Get_Good_Values_Second_Type_Graph_Per_Week_Sum(RangA, RangB, Surnames_In_Range);//пока что передаются все фамилии из диапазона
            List<int> BadValues = Get_Bad_Values_Second_Type_Graph_Per_Week_Sum(RangA, RangB, Surnames_In_Range);//пока что передаются все фамилии из диапазона
            Good = new int[GoodValues.Count];
            Bad = new int[BadValues.Count];
            GoodValues.CopyTo(Good);
            BadValues.CopyTo(Bad);
        }

        public void Second_Type_Graph_Per_Month_Count(int RangA, int RangB, out int[] Good, out int[] Bad, string[] surnames)
        {
            var Surnames_In_Range = surnames.ToList<string>();
            //здесь будет массив стрингов или что то вроде него, в котором будут фамилии выбранные из всех, 
            //их тебе и нежно передать в функцию ниже
            List<int> GoodValues = Get_Good_Values_Second_Type_Graph_Per_Month_Count(RangA, RangB, Surnames_In_Range);//пока что передаются все фамилии из диапазона
            List<int> BadValues = Get_Bad_Values_Second_Type_Graph_Per_Month_Count(RangA, RangB, Surnames_In_Range);//пока что передаются все фамилии из диапазона
            Good = new int[GoodValues.Count];
            Bad = new int[BadValues.Count];
            GoodValues.CopyTo(Good);
            BadValues.CopyTo(Bad);
        }

        public void Second_Type_Graph_Per_Month_Sum(int RangA, int RangB, out int[] Good, out int[] Bad, string[] surnames)
        {
            var Surnames_In_Range = surnames.ToList<string>();
            //здесь будет массив стрингов или что то вроде него, в котором будут фамилии выбранные из всех, 
            //их тебе и нежно передать в функцию ниже
            List<int> GoodValues = Get_Good_Values_Second_Type_Graph_Per_Month_Sum(RangA, RangB, Surnames_In_Range);//пока что передаются все фамилии из диапазона
            List<int> BadValues = Get_Bad_Values_Second_Type_Graph_Per_Month_Sum(RangA, RangB, Surnames_In_Range);//пока что передаются все фамилии из диапазона
            Good = new int[GoodValues.Count];
            Bad = new int[BadValues.Count];
            GoodValues.CopyTo(Good);
            BadValues.CopyTo(Bad);
        }
        #endregion

        //Построение третьего графика
        #region
        public void Third_Type_Graph_Per_Week_Count(int RangA, int RangB, out int[] Good, out int[] Bad, string[] surnames)
        {
            var Surnames_In_Range = surnames.ToList<string>();
            //здесь будет массив стрингов или что то вроде него, в котором будут фамилии выбранные из всех, 
            //их тебе и нежно передать в функцию ниже
            List<int> GoodValues = Get_Good_Values_Third_Type_Graph_Per_Week_Count(RangA, RangB, Surnames_In_Range);//пока что передаются все фамилии из диапазона
            List<int> BadValues = Get_Bad_Values_Third_Type_Graph_Per_Week_Count(RangA, RangB, Surnames_In_Range);//пока что передаются все фамилии из диапазона
            Good = new int[GoodValues.Count];
            Bad = new int[BadValues.Count];
            GoodValues.CopyTo(Good);
            BadValues.CopyTo(Bad);
        }

        public void Third_Type_Graph_Per_Week_Sum(int RangA, int RangB, out int[] Good, out int[] Bad, string[] surnames)
        {
            var Surnames_In_Range = surnames.ToList<string>();
            //здесь будет массив стрингов или что то вроде него, в котором будут фамилии выбранные из всех, 
            //их тебе и нежно передать в функцию ниже
            List<int> GoodValues = Get_Good_Values_Third_Type_Graph_Per_Week_Sum(RangA, RangB, Surnames_In_Range);//пока что передаются все фамилии из диапазона
            List<int> BadValues = Get_Bad_Values_Third_Type_Graph_Per_Week_Sum(RangA, RangB, Surnames_In_Range);//пока что передаются все фамилии из диапазона
            Good = new int[GoodValues.Count];
            Bad = new int[BadValues.Count];
            GoodValues.CopyTo(Good);
            BadValues.CopyTo(Bad);
        }

        public void Third_Type_Graph_Per_Month_Count(int RangA, int RangB, out int[] Good, out int[] Bad, string[] surnames)
        {
            var Surnames_In_Range = surnames.ToList<string>();
            //здесь будет массив стрингов или что то вроде него, в котором будут фамилии выбранные из всех, 
            //их тебе и нежно передать в функцию ниже
            List<int> GoodValues = Get_Good_Values_Third_Type_Graph_Per_Month_Count(RangA, RangB, Surnames_In_Range);//пока что передаются все фамилии из диапазона
            List<int> BadValues = Get_Bad_Values_Third_Type_Graph_Per_Month_Count(RangA, RangB, Surnames_In_Range);//пока что передаются все фамилии из диапазона
            Good = new int[GoodValues.Count];
            Bad = new int[BadValues.Count];
            GoodValues.CopyTo(Good);
            BadValues.CopyTo(Bad);
        }

        public void Third_Type_Graph_Per_Month_Sum(int RangA, int RangB, out int[] Good, out int[] Bad, string[] surnames)
        {
            var Surnames_In_Range = surnames.ToList<string>();
            //здесь будет массив стрингов или что то вроде него, в котором будут фамилии выбранные из всех, 
            //их тебе и нежно передать в функцию ниже
            List<int> GoodValues = Get_Good_Values_Third_Type_Graph_Per_Month_Sum(RangA, RangB, Surnames_In_Range);//пока что передаются все фамилии из диапазона
            List<int> BadValues = Get_Bad_Values_Third_Type_Graph_Per_Month_Sum(RangA, RangB, Surnames_In_Range);//пока что передаются все фамилии из диапазона
            Good = new int[GoodValues.Count];
            Bad = new int[BadValues.Count];
            GoodValues.CopyTo(Good);
            BadValues.CopyTo(Bad);
        }
        #endregion



        public List<string> Get_Surnames_Per_Week(int RangA, int RangB)
        {
            DataRow[] foundRows;
            reasons = new List<string>();
            foundRows = ds.Tables[1].Select();
            foreach (var item in foundRows)
            {
                for (int r = RangA; r <= RangB; r++)
                {
                    if ((item.ItemArray[28].ToString() != "") && (item.ItemArray[17].ToString() == "W" + r)) { reasons.Add(item.ItemArray[28].ToString().TrimEnd(' ')); }
                }
            }
            reasons = reasons.Distinct().ToList();//Все уникальные фамилии в диапазоне недель
            return reasons;
        }
        public List<string> Get_Surnames_Per_Month(int RangA, int RangB)
        {
            DataRow[] foundRows;
            reasons = new List<string>();
            foundRows = ds.Tables[1].Select();
            foreach (var item in foundRows)
            {
                for (int r = RangA; r <= RangB; r++)
                {
                    if ((item.ItemArray[28].ToString() != "") && (item.ItemArray[13] is DateTime) && ((DateTime)item.ItemArray[13] >= StartMonth[r - 1]) && ((DateTime)item.ItemArray[13]) < EndMonth[r - 1]) { reasons.Add(item.ItemArray[28].ToString().TrimEnd(' ')); }
                }
            }
            reasons = reasons.Distinct().ToList();//Все уникальные фамилии в диапазоне месяцов
            return reasons;
        }

        ////функции первого графика
        #region
        public List<int> Get_Good_Values_First_Type_Graph_Per_Week_Count(int RangA, int RangB, List<string> Surnames)////////////////////////////первый график из трех
        {
            DataRow[] foundRows;
            int Sum = 0;
            foundRows = ds.Tables[1].Select("[column18] IN ('0day', '1day', '2day', '3day')");
            List<int> lol = new List<int>();
            foreach (var elem in Surnames)
            {
                Sum = 0;
                for (int i = RangA; i <= RangB; i++)
                {
                    foreach (var item in foundRows)
                    {
                        if ((item.ItemArray[28].ToString().TrimEnd(' ') == elem) && (item.ItemArray[17].ToString() == "W" + i)) { Sum++; }
                    }
                }
                lol.Add(Sum);
            }
            return lol;
        }

        public List<int> Get_Good_Values_First_Type_Graph_Per_Month_Count(int RangA, int RangB, List<string> Surnames)////////////////////////////первый график из трех
        {
            DataRow[] foundRows;
            int Sum = 0;
            foundRows = ds.Tables[1].Select("[column18] IN ('0day', '1day', '2day', '3day')");
            List<int> lol = new List<int>();
            foreach (var elem in Surnames)
            {
                Sum = 0;
                for (int i = RangA; i <= RangB; i++)
                {
                    foreach (var item in foundRows)
                    {
                        if (((item.ItemArray[28].ToString().TrimEnd(' ') == elem) && (item.ItemArray[13] is DateTime) && ((DateTime)item.ItemArray[13] >= StartMonth[i - 1]) && ((DateTime)item.ItemArray[13]) < EndMonth[i - 1]) && (item.ItemArray[37].ToString() != "")) { Sum++; }
                    }
                }
                lol.Add(Sum);
            }

            int a = 0;
            foreach (var item in lol)
            {
                a += item;
            }
            return lol;
        }

        public List<int> Get_Good_Values_First_Type_Graph_Per_Week_Sum(int RangA, int RangB, List<string> Surnames)////////////////////////////первый график из трех
        {
            DataRow[] foundRows;
            int Sum = 0;
            foundRows = ds.Tables[1].Select("[column18] IN ('0day', '1day', '2day', '3day')");
            List<int> lol = new List<int>();
            foreach (var elem in Surnames)
            {
                Sum = 0;
                for (int i = RangA; i <= RangB; i++)
                {
                    foreach (var item in foundRows)
                    {
                        if ((item.ItemArray[28].ToString().TrimEnd(' ') == elem) && (item.ItemArray[17].ToString() == "W" + i) && (item.ItemArray[37].ToString() != "")) { Sum += Convert.ToInt32(item.ItemArray[37]); }
                    }
                }
                lol.Add(Sum);
            }
            return lol;
        }

        public List<int> Get_Good_Values_First_Type_Graph_Per_Month_Sum(int RangA, int RangB, List<string> Surnames)////////////////////////////первый график из трех
        {
            DataRow[] foundRows;
            int Sum = 0;
            foundRows = ds.Tables[1].Select("[column18] IN ('0day', '1day', '2day', '3day')");
            List<int> lol = new List<int>();
            foreach (var elem in Surnames)
            {
                Sum = 0;
                for (int i = RangA; i <= RangB; i++)
                {
                    foreach (var item in foundRows)
                    {
                        if (((item.ItemArray[28].ToString().TrimEnd(' ') == elem) && (item.ItemArray[13] is DateTime) && ((DateTime)item.ItemArray[13] >= StartMonth[i - 1]) && ((DateTime)item.ItemArray[13]) < EndMonth[i - 1]) && (item.ItemArray[37].ToString() != "")) { Sum += Convert.ToInt32(item.ItemArray[37]); }
                    }
                }
                lol.Add(Sum);
            }

            int a = 0;
            foreach (var item in lol)
            {
                a += item;
            }
            return lol;
        }

        ///////////////////////////////////////////////////////////////////

        public List<int> Get_Bad_Values_First_Type_Graph_Per_Week_Count(int RangA, int RangB, List<string> Surnames)////////////////////////////первый график из трех
        {
            DataRow[] foundRows;
            int Sum = 0;
            foundRows = ds.Tables[1].Select("[column18] NOT IN ('0day', '1day', '2day', '3day')");
            List<int> lol = new List<int>();
            foreach (var elem in Surnames)
            {
                Sum = 0;
                for (int i = RangA; i <= RangB; i++)
                {
                    foreach (var item in foundRows)
                    {
                        if ((item.ItemArray[28].ToString().TrimEnd(' ') == elem) && (item.ItemArray[17].ToString() == "W" + i)) { Sum++; }
                    }
                }
                lol.Add(Sum);
            }
            int a = 0;
            foreach (var item in lol)
            {
                a += item;
            }
            return lol;
        }

        public List<int> Get_Bad_Values_First_Type_Graph_Per_Month_Count(int RangA, int RangB, List<string> Surnames)////////////////////////////первый график из трех
        {
            DataRow[] foundRows;
            int Sum = 0;
            foundRows = ds.Tables[1].Select("[column18] NOT IN ('0day', '1day', '2day', '3day')");
            List<int> lol = new List<int>();
            foreach (var elem in Surnames)
            {
                Sum = 0;
                for (int i = RangA; i <= RangB; i++)
                {
                    foreach (var item in foundRows)
                    {
                        if (((item.ItemArray[28].ToString().TrimEnd(' ') == elem) && (item.ItemArray[13] is DateTime) && ((DateTime)item.ItemArray[13] >= StartMonth[i - 1]) && ((DateTime)item.ItemArray[13]) < EndMonth[i - 1]) && (item.ItemArray[37].ToString() != "")) { Sum++; }
                    }
                }
                lol.Add(Sum);
            }

            int a = 0;
            foreach (var item in lol)
            {
                a += item;
            }
            return lol;
        }

        public List<int> Get_Bad_Values_First_Type_Graph_Per_Week_Sum(int RangA, int RangB, List<string> Surnames)////////////////////////////первый график из трех
        {
            DataRow[] foundRows;
            int Sum = 0;
            foundRows = ds.Tables[1].Select("[column18] NOT IN ('0day', '1day', '2day', '3day')");
            List<int> lol = new List<int>();
            foreach (var elem in Surnames)
            {
                Sum = 0;
                for (int i = RangA; i <= RangB; i++)
                {
                    foreach (var item in foundRows)
                    {
                        if ((item.ItemArray[28].ToString().TrimEnd(' ') == elem) && (item.ItemArray[17].ToString() == "W" + i) && (item.ItemArray[37].ToString() != "")) { Sum += Convert.ToInt32(item.ItemArray[37]); }
                    }
                }
                lol.Add(Sum);
            }
            int a = 0;
            foreach (var item in lol)
            {
                a += item;
            }
            return lol;
        }

        public List<int> Get_Bad_Values_First_Type_Graph_Per_Month_Sum(int RangA, int RangB, List<string> Surnames)////////////////////////////первый график из трех
        {
            DataRow[] foundRows;
            int Sum = 0;
            foundRows = ds.Tables[1].Select("[column18] NOT IN ('0day', '1day', '2day', '3day')");
            List<int> lol = new List<int>();
            foreach (var elem in Surnames)
            {
                Sum = 0;
                for (int i = RangA; i <= RangB; i++)
                {
                    foreach (var item in foundRows)
                    {
                        if (((item.ItemArray[28].ToString().TrimEnd(' ') == elem) && (item.ItemArray[13] is DateTime) && ((DateTime)item.ItemArray[13] >= StartMonth[i - 1]) && ((DateTime)item.ItemArray[13]) < EndMonth[i - 1]) && (item.ItemArray[37].ToString() != "")) { Sum += Convert.ToInt32(item.ItemArray[37]); }
                    }
                }
                lol.Add(Sum);
            }

            int a = 0;
            foreach (var item in lol)
            {
                a += item;
            }
            return lol;
        }
        #endregion

        ////функции второго графика
        #region
        public List<int> Get_Good_Values_Second_Type_Graph_Per_Week_Count(int RangA, int RangB, List<string> Surnames)////////////////////////////второй график из трех
        {
            DataRow[] foundRows;
            int Sum = 0;
            foundRows = ds.Tables[1].Select("[column18] IN ('0day', '1day', '2day', '3day')");
            List<int> lol = new List<int>();
            foreach (var elem in Surnames)
            {
                Sum = 0;
                for (int i = RangA; i <= RangB; i++)
                {
                    foreach (var item in foundRows)
                    {
                        if ((item.ItemArray[28].ToString().TrimEnd(' ') == elem) && (item.ItemArray[17].ToString() == "W" + i)) { Sum++; }
                    }
                }
                lol.Add(Sum);
            }
            return lol;
        }

        public List<int> Get_Good_Values_Second_Type_Graph_Per_Month_Count(int RangA, int RangB, List<string> Surnames)////////////////////////////второй график из трех
        {
            DataRow[] foundRows;
            int Sum = 0;
            foundRows = ds.Tables[1].Select("[column18] IN ('0day', '1day', '2day', '3day')");
            List<int> lol = new List<int>();
            foreach (var elem in Surnames)
            {
                Sum = 0;
                for (int i = RangA; i <= RangB; i++)
                {
                    foreach (var item in foundRows)
                    {
                        if (((item.ItemArray[28].ToString().TrimEnd(' ') == elem) && (item.ItemArray[13] is DateTime) && ((DateTime)item.ItemArray[13] >= StartMonth[i - 1]) && ((DateTime)item.ItemArray[13]) < EndMonth[i - 1])) { Sum++; }
                    }
                }
                lol.Add(Sum);
            }

            int a = 0;
            foreach (var item in lol)
            {
                a += item;
            }
            return lol;
        }

        public List<int> Get_Good_Values_Second_Type_Graph_Per_Week_Sum(int RangA, int RangB, List<string> Surnames)////////////////////////////второй график из трех
        {
            DataRow[] foundRows;
            int Sum = 0;
            foundRows = ds.Tables[1].Select("[column18] IN ('0day', '1day', '2day', '3day')");
            List<int> lol = new List<int>();
            foreach (var elem in Surnames)
            {
                Sum = 0;
                for (int i = RangA; i <= RangB; i++)
                {
                    foreach (var item in foundRows)
                    {
                        if ((item.ItemArray[28].ToString().TrimEnd(' ') == elem) && (item.ItemArray[17].ToString() == "W" + i) && (item.ItemArray[38].ToString() != "")) { Sum += Convert.ToInt32(item.ItemArray[38]); }
                    }
                }
                lol.Add(Sum);
            }
            return lol;
        }

        public List<int> Get_Good_Values_Second_Type_Graph_Per_Month_Sum(int RangA, int RangB, List<string> Surnames)////////////////////////////второй график из трех
        {
            DataRow[] foundRows;
            int Sum = 0;
            foundRows = ds.Tables[1].Select("[column18] IN ('0day', '1day', '2day', '3day')");
            List<int> lol = new List<int>();
            foreach (var elem in Surnames)
            {
                Sum = 0;
                for (int i = RangA; i <= RangB; i++)
                {
                    foreach (var item in foundRows)
                    {
                        if (((item.ItemArray[28].ToString().TrimEnd(' ') == elem) && (item.ItemArray[13] is DateTime) && ((DateTime)item.ItemArray[13] >= StartMonth[i - 1]) && ((DateTime)item.ItemArray[13]) < EndMonth[i - 1]) && (item.ItemArray[38].ToString() != "")) { Sum += Convert.ToInt32(item.ItemArray[38]); }
                    }
                }
                lol.Add(Sum);
            }

            int a = 0;
            foreach (var item in lol)
            {
                a += item;
            }
            return lol;
        }

        ///////////////////////////////////////////////////////////////////

        public List<int> Get_Bad_Values_Second_Type_Graph_Per_Week_Count(int RangA, int RangB, List<string> Surnames)////////////////////////////второй график из трех
        {
            DataRow[] foundRows;
            int Sum = 0;
            foundRows = ds.Tables[1].Select("[column18] NOT IN ('0day', '1day', '2day', '3day')");
            List<int> lol = new List<int>();
            foreach (var elem in Surnames)
            {
                Sum = 0;
                for (int i = RangA; i <= RangB; i++)
                {
                    foreach (var item in foundRows)
                    {
                        if ((item.ItemArray[28].ToString().TrimEnd(' ') == elem) && (item.ItemArray[17].ToString() == "W" + i)) { Sum++; }
                    }
                }
                lol.Add(Sum);
            }
            int a = 0;
            foreach (var item in lol)
            {
                a += item;
            }
            return lol;
        }

        public List<int> Get_Bad_Values_Second_Type_Graph_Per_Month_Count(int RangA, int RangB, List<string> Surnames)////////////////////////////второй график из трех
        {
            DataRow[] foundRows;
            int Sum = 0;
            foundRows = ds.Tables[1].Select("[column18] NOT IN ('0day', '1day', '2day', '3day')");
            List<int> lol = new List<int>();
            foreach (var elem in Surnames)
            {
                Sum = 0;
                for (int i = RangA; i <= RangB; i++)
                {
                    foreach (var item in foundRows)
                    {
                        if (((item.ItemArray[28].ToString().TrimEnd(' ') == elem) && (item.ItemArray[13] is DateTime) && ((DateTime)item.ItemArray[13] >= StartMonth[i - 1]) && ((DateTime)item.ItemArray[13]) < EndMonth[i - 1])) { Sum++; }
                    }
                }
                lol.Add(Sum);
            }

            int a = 0;
            foreach (var item in lol)
            {
                a += item;
            }
            return lol;
        }

        public List<int> Get_Bad_Values_Second_Type_Graph_Per_Week_Sum(int RangA, int RangB, List<string> Surnames)////////////////////////////второй график из трех
        {
            DataRow[] foundRows;
            int Sum = 0;
            foundRows = ds.Tables[1].Select("[column18] NOT IN ('0day', '1day', '2day', '3day')");
            List<int> lol = new List<int>();
            foreach (var elem in Surnames)
            {
                Sum = 0;
                for (int i = RangA; i <= RangB; i++)
                {
                    foreach (var item in foundRows)
                    {
                        if ((item.ItemArray[28].ToString().TrimEnd(' ') == elem) && (item.ItemArray[17].ToString() == "W" + i) && (item.ItemArray[38].ToString() != "")) { Sum += Convert.ToInt32(item.ItemArray[38]); }
                    }
                }
                lol.Add(Sum);
            }
            int a = 0;
            foreach (var item in lol)
            {
                a += item;
            }
            return lol;
        }

        public List<int> Get_Bad_Values_Second_Type_Graph_Per_Month_Sum(int RangA, int RangB, List<string> Surnames)////////////////////////////второй график из трех
        {
            DataRow[] foundRows;
            int Sum = 0;
            foundRows = ds.Tables[1].Select("[column18] NOT IN ('0day', '1day', '2day', '3day')");
            List<int> lol = new List<int>();
            foreach (var elem in Surnames)
            {
                Sum = 0;
                for (int i = RangA; i <= RangB; i++)
                {
                    foreach (var item in foundRows)
                    {
                        if (((item.ItemArray[28].ToString().TrimEnd(' ') == elem) && (item.ItemArray[13] is DateTime) && ((DateTime)item.ItemArray[13] >= StartMonth[i - 1]) && ((DateTime)item.ItemArray[13]) < EndMonth[i - 1]) && (item.ItemArray[38].ToString() != "")) { Sum += Convert.ToInt32(item.ItemArray[38]); }
                    }
                }
                lol.Add(Sum);
            }

            int a = 0;
            foreach (var item in lol)
            {
                a += item;
            }
            return lol;
        }
        #endregion

        ////функции третьего графика
        #region
        public List<int> Get_Good_Values_Third_Type_Graph_Per_Week_Count(int RangA, int RangB, List<string> Surnames)////////////////////////////третий график из трех
        {
            DataRow[] foundRows;
            int Sum = 0;
            foundRows = ds.Tables[1].Select("[column18] IN ('0day', '1day', '2day', '3day')");
            List<int> lol = new List<int>();
            foreach (var elem in Surnames)
            {
                Sum = 0;
                for (int i = RangA; i <= RangB; i++)
                {
                    foreach (var item in foundRows)
                    {
                        if ((item.ItemArray[28].ToString().TrimEnd(' ') == elem) && (item.ItemArray[17].ToString() == "W" + i)) { Sum++; }
                    }
                }
                lol.Add(Sum);
            }
            return lol;
        }

        public List<int> Get_Good_Values_Third_Type_Graph_Per_Month_Count(int RangA, int RangB, List<string> Surnames)////////////////////////////третий график из трех
        {
            DataRow[] foundRows;
            int Sum = 0;
            foundRows = ds.Tables[1].Select("[column18] IN ('0day', '1day', '2day', '3day')");
            List<int> lol = new List<int>();
            foreach (var elem in Surnames)
            {
                Sum = 0;
                for (int i = RangA; i <= RangB; i++)
                {
                    foreach (var item in foundRows)
                    {
                        if (((item.ItemArray[28].ToString().TrimEnd(' ') == elem) && (item.ItemArray[13] is DateTime) && ((DateTime)item.ItemArray[13] >= StartMonth[i - 1]) && ((DateTime)item.ItemArray[13]) < EndMonth[i - 1])) { Sum++; }
                    }
                }
                lol.Add(Sum);
            }

            int a = 0;
            foreach (var item in lol)
            {
                a += item;
            }
            return lol;
        }

        public List<int> Get_Good_Values_Third_Type_Graph_Per_Week_Sum(int RangA, int RangB, List<string> Surnames)////////////////////////////третий график из трех
        {
            DataRow[] foundRows;
            int Sum = 0;
            foundRows = ds.Tables[1].Select("[column18] IN ('0day', '1day', '2day', '3day')");
            List<int> lol = new List<int>();
            foreach (var elem in Surnames)
            {
                Sum = 0;
                for (int i = RangA; i <= RangB; i++)
                {
                    foreach (var item in foundRows)
                    {
                        if ((item.ItemArray[28].ToString().TrimEnd(' ') == elem) && (item.ItemArray[17].ToString() == "W" + i) && (item.ItemArray[39].ToString() != "")) { Sum += Convert.ToInt32(item.ItemArray[39]); }
                    }
                }
                lol.Add(Sum);
            }
            return lol;
        }

        public List<int> Get_Good_Values_Third_Type_Graph_Per_Month_Sum(int RangA, int RangB, List<string> Surnames)////////////////////////////третий график из трех
        {
            DataRow[] foundRows;
            int Sum = 0;
            foundRows = ds.Tables[1].Select("[column18] IN ('0day', '1day', '2day', '3day')");
            List<int> lol = new List<int>();
            foreach (var elem in Surnames)
            {
                Sum = 0;
                for (int i = RangA; i <= RangB; i++)
                {
                    foreach (var item in foundRows)
                    {
                        if (((item.ItemArray[28].ToString().TrimEnd(' ') == elem) && (item.ItemArray[13] is DateTime) && ((DateTime)item.ItemArray[13] >= StartMonth[i - 1]) && ((DateTime)item.ItemArray[13]) < EndMonth[i - 1]) && (item.ItemArray[39].ToString() != "")) { Sum += Convert.ToInt32(item.ItemArray[39]); }
                    }
                }
                lol.Add(Sum);
            }

            int a = 0;
            foreach (var item in lol)
            {
                a += item;
            }
            return lol;
        }

        ///////////////////////////////////////////////////////////////////

        public List<int> Get_Bad_Values_Third_Type_Graph_Per_Week_Count(int RangA, int RangB, List<string> Surnames)////////////////////////////третий график из трех
        {
            DataRow[] foundRows;
            int Sum = 0;
            foundRows = ds.Tables[1].Select("[column18] NOT IN ('0day', '1day', '2day', '3day')");
            List<int> lol = new List<int>();
            foreach (var elem in Surnames)
            {
                Sum = 0;
                for (int i = RangA; i <= RangB; i++)
                {
                    foreach (var item in foundRows)
                    {
                        if ((item.ItemArray[28].ToString().TrimEnd(' ') == elem) && (item.ItemArray[17].ToString() == "W" + i)) { Sum++; }
                    }
                }
                lol.Add(Sum);
            }
            int a = 0;
            foreach (var item in lol)
            {
                a += item;
            }
            return lol;
        }

        public List<int> Get_Bad_Values_Third_Type_Graph_Per_Month_Count(int RangA, int RangB, List<string> Surnames)////////////////////////////третий график из трех
        {
            DataRow[] foundRows;
            int Sum = 0;
            foundRows = ds.Tables[1].Select("[column18] NOT IN ('0day', '1day', '2day', '3day')");
            List<int> lol = new List<int>();
            foreach (var elem in Surnames)
            {
                Sum = 0;
                for (int i = RangA; i <= RangB; i++)
                {
                    foreach (var item in foundRows)
                    {
                        if (((item.ItemArray[28].ToString().TrimEnd(' ') == elem) && (item.ItemArray[13] is DateTime) && ((DateTime)item.ItemArray[13] >= StartMonth[i - 1]) && ((DateTime)item.ItemArray[13]) < EndMonth[i - 1])) { Sum++; }
                    }
                }
                lol.Add(Sum);
            }

            int a = 0;
            foreach (var item in lol)
            {
                a += item;
            }
            return lol;
        }

        public List<int> Get_Bad_Values_Third_Type_Graph_Per_Week_Sum(int RangA, int RangB, List<string> Surnames)////////////////////////////третий график из трех
        {
            DataRow[] foundRows;
            int Sum = 0;
            foundRows = ds.Tables[1].Select("[column18] NOT IN ('0day', '1day', '2day', '3day')");
            List<int> lol = new List<int>();
            foreach (var elem in Surnames)
            {
                Sum = 0;
                for (int i = RangA; i <= RangB; i++)
                {
                    foreach (var item in foundRows)
                    {
                        if ((item.ItemArray[28].ToString().TrimEnd(' ') == elem) && (item.ItemArray[17].ToString() == "W" + i) && (item.ItemArray[39].ToString() != "")) { Sum += Convert.ToInt32(item.ItemArray[39]); }
                    }
                }
                lol.Add(Sum);
            }
            int a = 0;
            foreach (var item in lol)
            {
                a += item;
            }
            return lol;
        }

        public List<int> Get_Bad_Values_Third_Type_Graph_Per_Month_Sum(int RangA, int RangB, List<string> Surnames)////////////////////////////третий график из трех
        {
            DataRow[] foundRows;
            int Sum = 0;
            foundRows = ds.Tables[1].Select("[column18] NOT IN ('0day', '1day', '2day', '3day')");
            List<int> lol = new List<int>();
            foreach (var elem in Surnames)
            {
                Sum = 0;
                for (int i = RangA; i <= RangB; i++)
                {
                    foreach (var item in foundRows)
                    {
                        if (((item.ItemArray[28].ToString().TrimEnd(' ') == elem) && (item.ItemArray[13] is DateTime) && ((DateTime)item.ItemArray[13] >= StartMonth[i - 1]) && ((DateTime)item.ItemArray[13]) < EndMonth[i - 1]) && (item.ItemArray[39].ToString() != "")) { Sum += Convert.ToInt32(item.ItemArray[39]); }
                    }
                }
                lol.Add(Sum);
            }

            int a = 0;
            foreach (var item in lol)
            {
                a += item;
            }
            return lol;
        }
        #endregion
    }
}