using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pickflicks2.Models;
using pickflicks2.Models.DTO;
using pickflicks2.Services;
using Microsoft.AspNetCore.Mvc;

namespace pickflicks2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserService _data;

        public UserController(UserService dataFromService) {
            _data = dataFromService;
        }

        // Add a user with a CreateAccuntDTO (will return bool)
        [HttpPost("AddUser")]
          public IActionResult AddUser(CreateAccountDTO userToAdd)
        {
            return _data.AddUser(userToAdd);
        }

        // User login with LoginDTO (will return token)
        [HttpPost("Login")]
        public IActionResult Login([FromBody] LoginDTO user)
        {
            return _data.Login(user);
        } 

        // Get a list of all users by UserDTO (will return a list)
        [HttpGet("GetAllUsers")]
        public List<UserDTO> GetAllUsers()
        {
            return _data.GetAllUsers();
        }

        // Get a user's UserDTO by their string username (will return UserDTO)
        [HttpGet("GetUserByUsername/{username}")]
        public UserDTO GetUserByUsername(string? username)
        {
            return _data.GetUserByUsername(username);
        }

        // Get a user's UserDTO by their int id (will return UserDTO)
        [HttpGet("GetUserById/{id}")]
        public UserDTO GetUserById(int id)
        {
            return _data.GetUserById(id);
        }

        // Soft delete (will return bool)
        [HttpPost("DeleteUser/{username}")]
        public bool DeleteUser(string? username)
        {
            return _data.DeleteUser(username);
        }

        // Add a favorite to a MWG 
        [HttpPost("AddFavoriteMWG/{userId}/{MWGId}")]
        public bool AddFavoriteMWG(int userId, int MWGId)
        {
            return _data.AddFavoriteMWG(userId, MWGId);
        }

        // Remove a favorite to a MWG 
        [HttpPost("RemoveFavoriteMWG/{userId}/{MWGId}")]
        public bool RemoveFavoriteMWG(int userId, int MWGId)
        {
            return _data.RemoveFavoriteMWG(userId, MWGId);
        }

        // Edit user icon
        [HttpPost("EditUserIcon/{userId}/{iconName}")]
        public bool EditUserIcon(int userId, string iconName)
        {
            return _data.EditUserIcon(userId, iconName);
        }

        [HttpPost("EditUsername/{userId}/{newUsername}")]
        public bool EditUsername(int userId, string newUsername)
        {
            return _data.EditUsername(userId, newUsername);
        }
        [HttpPost("EditPassword/{userId}/{newPassword}")]
        public bool EditPassword(int userId, string newPassword)
        {
            return _data.EditPassword(userId, newPassword);
        }
        [HttpPost("CheckPassword")]
        public bool CheckPassword([FromBody] LoginDTO user)
        {
            return _data.CheckPassword(user);
        }
    }
}