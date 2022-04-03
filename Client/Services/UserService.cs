using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using bpgweb.Shared.Models;

namespace bpgweb.Client.Services
{
    public class UserService : IUserService
    {
        private readonly HttpClient _httpClient;

        public UserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public List<Group> Groups { get; set; } = new List<Group>();
        public List<User> Users { get; set; } = new List<User>();

        public event Action OnChange;

        public async Task<List<User>> CreateUser(User user)
        {
            var result  = await _httpClient.PostAsJsonAsync($"api/user", user);
            Users       = await result.Content.ReadFromJsonAsync<List<User>>();
            OnChange.Invoke();
            return Users;
        }

        public async Task<List<User>> DeleteUser(int ID)
        {
            var result  = await _httpClient.DeleteAsync($"api/user/{ID}");
            Users       = await result.Content.ReadFromJsonAsync<List<User>>();
            OnChange.Invoke();
            return Users;
        }

        public async Task GetGroups()
        {
            Groups = await _httpClient.GetFromJsonAsync<List<Group>>($"api/user/groups");
        }

        public async Task<User> GetUser(int ID)
        {
            return await _httpClient.GetFromJsonAsync<User>($"api/user/{ID}");
        }

        public async Task<List<User>> GetUsers()
        {
            Users   = await _httpClient.GetFromJsonAsync<List<User>>("api/user");
            return Users;
        }

        public async Task<List<User>> UpdateUser(User user, int ID)
        {
            var result  = await _httpClient.PutAsJsonAsync($"api/user/{ID}", user);
            Users       = await result.Content.ReadFromJsonAsync<List<User>>();
            OnChange.Invoke();
            return Users;
        }
    }
}
