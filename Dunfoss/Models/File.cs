using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dunfoss.Models
{
    public class File
    {
        public int Id { get; set; }
        public string Path { get; set; }
        public string Name { get; set; }
        public int Type { get; set; }  
    }
}