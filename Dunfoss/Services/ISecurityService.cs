using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dunfoss.Models;

namespace Dunfoss.Services
{
    interface ISecurityService
    {
        bool IsAuthenticate();
        bool Authenticate(string name, string password);
        void Login(User user);
        void Logout();
        User GetCurrentUser();
    }
}
