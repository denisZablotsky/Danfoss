using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dunfoss.Models;

namespace Dunfoss.Data
{
    public class EfLetterRepository : ILetterRepository
    {
        private EfDbContext Context;
        public EfLetterRepository()
        {
            Context = new EfDbContext();
        }
        public IQueryable<Letter> Letter
        {
            get
            {
                return Context.Letters;
            }
        }

        public Letter CreateLetter(Letter letter)
        {
            Letter newLetter = Context.Letters.Add(letter);
            Context.SaveChanges();
            return newLetter;

        }

        public Letter GetLetterById(int id)
        {
            return Context.Letters.Find(id);
        }
    }
}