using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dunfoss.Models;
using Dunfoss.Data;

namespace Dunfoss.Data
{
    public class EfFileRepository : IFileRepository
    {
        EfDbContext Context;
        public EfFileRepository()
        {
            Context = new EfDbContext();
        }
        public IQueryable<File> Files
        {
            get
            {
                return Context.Files;
            }
        }

        public File CreateFile(File file)
        {
            File newFile = Context.Files.Add(file);
            Context.SaveChanges();
            return newFile;
        }

        public File GetFileById(int id)
        {
            return Context.Files.Find(id);
        }

    }
}