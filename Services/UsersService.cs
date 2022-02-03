using System.Collections.Generic;
using System.Linq;
using WebApi.Models;

namespace WebApi.Auth
{
    public interface IUsersService
    {
        IEnumerable<User> GetAll();
        User GetById(int id);
    }

    public class UsersService : IUsersService
    {
        // users list
        private List<User> _users = new List<User>
        {
            new User {  Id = 1, FirstName = "Test", LastName = "User", 
                        Role = "User",
                        Username = "test", Password = "test" },
            new User {  Id = 2, FirstName = "Santi", LastName = "Lopez", 
                        Role = "Admin",
                        Username = "santi", Password = "1234" }
        };

        public IEnumerable<User> GetAll()
        {
            return _users;
        }
        public User GetById(int id)
        {
            return _users.FirstOrDefault(x => x.Id == id);
        }
    }
}