using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dunfoss.Models
{
    public class Letter
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int month { get; set; }
        public string Division { get; set; }
        public string Table1 { get; set; }
        public string Chart1 { get; set; }
        public string Table2 { get; set; }
        public string Table3 { get; set; }
        public string Chart2 { get; set; }
        public string Table4 { get; set; }
    }
}