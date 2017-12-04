using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dunfoss.Models;
using System.Data.Entity;

namespace Dunfoss.Data
{
    public class EfDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<Letter> Letters { get; set; }
        public DbSet<File> Files { get; set; }
        public DbSet<CurrentFile> CurrentFiles { get; set; }
         
    }
}