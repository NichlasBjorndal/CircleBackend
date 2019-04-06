using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using circle_backend.Database.Repositories;
using circle_backend.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace circle_backend.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {

        private readonly IUserRepository _userRepository;
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
            
        [HttpPut("{sessionId}")]
        public void AddUser(int sessionId, [FromBody]User user) 
        {
            user.SessionId = sessionId;
            _userRepository.AddUser(user);
            _userRepository.Save();
        }

        [HttpGet("{userId}")]
        public User GetUserById(int userId)
        {
            User user = _userRepository.GetUserById(userId);
            return user;
        }

        [HttpPost("{userId}/{readyStatus}")]
        public void SetUserReadyStatus(int userId, bool readyStatus)
        {
            _userRepository.SetUserReadyStatus(userId, readyStatus);
            _userRepository.Save();
        }

        [HttpPost("{userId}/{turnCompleteStatus}")]
        public void SetUserCompleteStatus(int userId, bool turnCompleteStatus)
        {
            _userRepository.SetUserReadyStatus(userId, turnCompleteStatus);
            _userRepository.Save();
        }

        [HttpDelete("{userId}")]
        public void RemoveUser(int userId) 
        {
            _userRepository.RemoveUser(userId);
            _userRepository.Save();
        } 

    }
}
