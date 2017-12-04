using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dunfoss.Models;

namespace Dunfoss.Data
{
    public class EfUserRepository : IUserRepository
    {
        private EfDbContext Context;
        public EfUserRepository()
        {
            Context = new EfDbContext();
        }
        public User CreateUser(User user)
        {
            User newuser = Context.Users.Add(user);
            Context.SaveChanges();
            return newuser;
        }

        public IQueryable<User> Users
        {
            get
            {
                return Context.Users;
            }
        }

        public User GetUserById(int Id)
        {
            User user = Context.Users.Find(Id);
            return user;
        }
        public User GetUserByName(string name)
        {
            User user = Context.Users.SingleOrDefault(x => x.Name == name);
            return user;
        }

    }
}