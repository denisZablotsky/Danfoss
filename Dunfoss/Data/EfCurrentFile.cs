using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dunfoss.Models;

namespace Dunfoss.Data
{
    public class EfCurrentFile : ICurrentFile
    {
        private EfDbContext Context;
        public EfCurrentFile()
        {
            Context = new EfDbContext();
        }
        public IQueryable<CurrentFile> CurrentFiles
        {
            get
            {
                return Context.CurrentFiles;
            }
        }

        public CurrentFile GetCurrentFile()
        {
            return Context.CurrentFiles.Find(1);
        }

        public CurrentFile InitializeCurrentFile(CurrentFile currentFile)
        {
            CurrentFile current = new CurrentFile();
            current.Path1 = "/xls/1.xls";
            current.Path2 = "/xls/2.xls";
            current.Path3 = "/xls/3.xlsx";
            currentFile = Context.CurrentFiles.Add(current);
            Context.SaveChanges();
            return currentFile;
        }

        public CurrentFile UpdateCurrentFile(CurrentFile currentFile)
        {
            CurrentFile current = Context.CurrentFiles.Find(1);
            current.Path1 = currentFile.Path1;
            current.Path2 = currentFile.Path2;
            current.Path3 = currentFile.Path3;
            Context.SaveChanges();
            return current;
        }

        public void UpdateFile1(string path)
        {
            CurrentFile current = Context.CurrentFiles.Find(1);
            current.Path1 = path;
            Context.SaveChanges();
        }

        public void UpdateFile2(string path)
        {
            CurrentFile current = Context.CurrentFiles.Find(1);
            current.Path2 = path;
            Context.SaveChanges();
        }

        public void UpdateFile3(string path)
        {
            CurrentFile current = Context.CurrentFiles.Find(1);
            current.Path3 = path;
            Context.SaveChanges();
        }
    }
}