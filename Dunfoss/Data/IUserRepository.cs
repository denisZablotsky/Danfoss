using System.Linq;
using Dunfoss.Models;

namespace Dunfoss.Data
{
     public interface IUserRepository
    {
        IQueryable<User> Users { get; }
        User GetUserById(int Id);
        User GetUserByName(string name);
        User CreateUser(User user);
    }
}