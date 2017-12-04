using Dunfoss.Data;
using Dunfoss.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace Dunfoss.Services
{
    public class SecurityService : ISecurityService
    {
        private EfDbContext _context;
        private readonly HttpSessionState _session;
        private int UserId
        {
            get
            {
                return Convert.ToInt32(_session["UserId"]);
            }
            set
            {
                _session["UserId"] = value;
            }
        }

        public SecurityService(HttpSessionState session = null)
        {
            _context = new EfDbContext();
            _session = session ?? HttpContext.Current.Session;
        }

        public bool Authenticate(string name, string password)
        {
            User user = _context.Users.SingleOrDefault(x => x.Name == name);
            if (user == null)
                return false;
            if (user.Password != password)
                return false;
            return true;
        }

        public User GetCurrentUser()
        {
            if (!IsAuthenticate())
                return null;
            return _context.Users.Find(UserId);
        }

        public bool IsAuthenticate()
        {
            return UserId > 0;
        }

        public void Login(User user)
        {
            UserId = user.Id;
        }

        public void Logout()
        {
            _session.Abandon();
        }
    }
}