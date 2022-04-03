using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bpgweb.Server.Data;
using bpgweb.Shared.Models;

namespace bpgweb.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        static List<Group> groups = new List<Group> {
            new Group { ID = 1, Name = "Basic"},
            new Group { ID = 2, Name = "Staff"}
        };

        static List<User> heroes = new List<User> {
            new User { 
                    ID = 1, 
                    FirstName = "Tester", 
                    LastName = "Numone", 
                    Username = "tester1", 
                    Group = groups[0]
                },
            new User { 
                    ID = 2, 
                    FirstName = "Tester", 
                    LastName = "Two", 
                    Username = "tester2", 
                    Group = groups[1]
                }
        };

        private readonly DataContext _context;

        public UserController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("groups")]
        public async Task<IActionResult> GetGroups()
        {
            return Ok(await _context.Groups.ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            return base.Ok(await GetDbUsers());
        }

        private async Task<List<User>> GetDbUsers()
        {
            return await _context.Users.Include(u => u.Group).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSingleUser(int ID)
        {
            var user = await _context.Users
                .Include(u => u.Group)
                .FirstOrDefaultAsync(u => u.ID == ID);
            if (user == null)
                return NotFound("User wasn't found");

            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok(await GetDbUsers());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSuperHero(User user, int ID)
        {
            var dbUser = await _context.Users
                .Include(u => u.Group)
                .FirstOrDefaultAsync(u => u.ID == ID);
            if (dbUser == null)
                return NotFound("User wasn't found");

            dbUser.FirstName    = user.FirstName;
            dbUser.LastName     = user.LastName;
            dbUser.Username     = user.Username;
            dbUser.GroupID      = user.GroupID;

            await _context.SaveChangesAsync();

            return Ok(await GetDbUsers());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSuperHero(int ID)
        {
            var dbUser = await _context.Users
                .Include(u => u.Group)
                .FirstOrDefaultAsync(u => u.ID == ID);
            if (dbUser == null)
                return NotFound("User wasn't found");

            _context.Users.Remove(dbUser);
            await _context.SaveChangesAsync();

            return Ok(await GetDbUsers());
        }
    }
}
