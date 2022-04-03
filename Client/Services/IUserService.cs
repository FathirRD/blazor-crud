using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bpgweb.Shared.Models;

namespace bpgweb.Client.Services
{
    public interface IUserService
    {
        event Action OnChange;
        List<Group> Groups { get; set; }
        List<User> Users { get; set; }
        Task<List<User>> GetUsers();
        Task GetGroups();
        Task<User> GetUser(int ID);
        Task<List<User>> CreateUser(User user);
        Task<List<User>> UpdateUser(User user, int ID);
        Task<List<User>> DeleteUser(int ID);
    }
}
