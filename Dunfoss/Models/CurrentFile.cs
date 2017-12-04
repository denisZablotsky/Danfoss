using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dunfoss.Models
{
    public class CurrentFile
    {
        public int Id { get; set; }
        public string Path1 { get; set; }
        public string Path2 { get; set; }
        public string Path3 { get; set; }
        public int FileId1 { get; set; }
        public int FileId2 { get; set; }
        public int FileId3 { get; set; }

    }
}