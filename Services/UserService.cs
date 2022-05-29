using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using pickflicks2.Models;
using pickflicks2.Models.DTO;
using pickflicks2.Services;
using pickflicks2.Services.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace pickflicks2.Services
{
    public class UserService : ControllerBase
    {
        private readonly DataContext _context;
        public UserService(DataContext context) 
        {
            _context = context;
        }

        public bool DoesUserExists(string? username)
        {
            return _context.UserInfo.SingleOrDefault(user => user.Username == username) != null;
        }

        public PasswordDTO HashPassword(string? password)
        {
            PasswordDTO newHashedPassword = new PasswordDTO();
            byte[] SaltBytes = new byte[64];
            var provider = RandomNumberGenerator.Create();
            provider.GetNonZeroBytes(SaltBytes);
            var Salt = Convert.ToBase64String(SaltBytes);
            var rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, SaltBytes, 10000);
            var HashPassword = Convert.ToBase64String(rfc2898DeriveBytes.GetBytes(256));
            newHashedPassword.Salt = Salt;
            newHashedPassword.Hash = HashPassword;
            return newHashedPassword;
        }

        public IActionResult AddUser(CreateAccountDTO userToAdd)
        {
            //bool result = false;
            if (!DoesUserExists(userToAdd.Username))
            {
                UserModel newUser = new UserModel();
                newUser.Id = 0;
                newUser.Username = userToAdd.Username;
                newUser.Icon = userToAdd.Icon;
                newUser.MyMWGId = userToAdd.MyMWGId;
                newUser.ListOfMWGId = userToAdd.ListOfMWGId;
                newUser.FavoritedMWGId = userToAdd.FavoritedMWGId;
                newUser.IsDeleted = false;

                var hashedPassword = HashPassword(userToAdd.Password);
                newUser.Salt = hashedPassword.Salt;
                newUser.Hash = hashedPassword.Hash;
                
                _context.Add(newUser);

                _context.SaveChanges();
            }

            IActionResult Result = Unauthorized();
            if (DoesUserExists(userToAdd.Username))
            {
                var foundUser = FindUserByUsername(userToAdd.Username);
                var verifyPass = VerifyUserPassword(userToAdd.Password, foundUser.Hash, foundUser.Salt);
                if (verifyPass)
                {
                    var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ILoveToSolveKatasAllDay@209"));
                    var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                    var tokeOptions = new JwtSecurityToken(
                        issuer: "http://localhost:5000",
                        audience: "http://localhost:5000",
                        claims: new List<Claim>(),
                        expires: DateTime.Now.AddMinutes(30),
                        signingCredentials: signinCredentials
                    );
                    
                    var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
                    Result = Ok(new { Token = tokenString });
                    
                }
            }
            return Result;
        }


        public bool VerifyUserPassword(string? password, string? storedHash, string? storedSalt)
        {
            var SaltBytes = Convert.FromBase64String(storedSalt);
            var rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, SaltBytes, 10000);
            var newHash = Convert.ToBase64String(rfc2898DeriveBytes.GetBytes(256));
            return newHash == storedHash;
        }


        public IActionResult Login([FromBody] LoginDTO user)
        {
            IActionResult Result = Unauthorized();
            if (DoesUserExists(user.Username))
            {
                var foundUser = FindUserByUsername(user.Username);
                var verifyPass = VerifyUserPassword(user.Password, foundUser.Hash, foundUser.Salt);
                if (verifyPass)
                {
                    var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ILoveToSolveKatasAllDay@209"));
                    var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                    var tokeOptions = new JwtSecurityToken(
                        issuer: "http://localhost:5000",
                        audience: "http://localhost:5000",
                        claims: new List<Claim>(),
                        expires: DateTime.Now.AddMinutes(30),
                        signingCredentials: signinCredentials
                    );
                    
                    var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
                    Result = Ok(new { Token = tokenString });
                    
                }
            }
            return Result;
        }

        public UserDTO GetUserByUsername(string? username)
        {
            var dtoInfo = new UserDTO();
            var foundUser = _context.UserInfo.SingleOrDefault(user => user.Username == username);
            if(foundUser != null)
            {
            dtoInfo.Id = foundUser.Id;
            dtoInfo.Username = foundUser.Username;
            dtoInfo.Icon = foundUser.Icon;
            dtoInfo.MyMWGId = foundUser.MyMWGId;
            dtoInfo.ListOfMWGId = foundUser.ListOfMWGId;
            dtoInfo.FavoritedMWGId = foundUser.FavoritedMWGId;
            dtoInfo.IsDeleted = foundUser.IsDeleted;
            }
            return dtoInfo;
        }

        public UserDTO GetUserById(int id)
        {
            var dtoInfo = new UserDTO();
            var foundUser = _context.UserInfo.SingleOrDefault(user => user.Id == id);
            dtoInfo.Id = foundUser.Id;
            dtoInfo.Username = foundUser.Username;
            dtoInfo.Icon = foundUser.Icon;
            dtoInfo.MyMWGId = foundUser.MyMWGId;
            dtoInfo.ListOfMWGId = foundUser.ListOfMWGId;
            dtoInfo.FavoritedMWGId = foundUser.FavoritedMWGId;
            dtoInfo.IsDeleted = foundUser.IsDeleted;

            return dtoInfo;
        }

        public List<UserDTO> GetAllUsers()
        {
            List<UserModel> AllUser = new List<UserModel>();
            AllUser = _context.UserInfo.ToList();
            List<UserDTO>PublicDataAllUser = new List<UserDTO>();
            foreach (UserModel User in AllUser)
            {
                UserDTO PublicUserInfo = GetUserByUsername(User.Username);
                PublicDataAllUser.Add(PublicUserInfo);
            }
            return PublicDataAllUser;
        }

        public bool DeleteUser(string? username)
        {
            UserModel foundUser = FindUserByUsername(username);
            bool result=false;
            if(foundUser!=null)
            {
                foundUser.IsDeleted=!foundUser.IsDeleted;
                _context.Update<UserModel>(foundUser);
                result = _context.SaveChanges()!=0;
            }
            return result;
        }

        // Only use this for backend, NEVER PASS DATA TO FRONTEND 
        public UserModel FindUserByUsername(string? username)
        {
            return _context.UserInfo.SingleOrDefault(item => item.Username == username);
        }
        
        // Only use this for backend, NEVER PASS DATA TO FRONTEND 
        public UserModel FindUserById(int Id)
        {
            return _context.UserInfo.SingleOrDefault(item => item.Id == Id);
        }

        // Add a favorite to a MWG 
        public bool AddFavoriteMWG(int userId, int MWGId)
        {
            bool result = false;

            var foundUser = _context.UserInfo.SingleOrDefault(user => user.Id == userId);

            if (foundUser != null) {
                foundUser.FavoritedMWGId += ',' + MWGId.ToString(); 

                _context.Update<UserModel>(foundUser);
                result = _context.SaveChanges() != 0;
            }
            return result;
        }

        // Remove a favorite to a MWG 
        public bool RemoveFavoriteMWG(int userId, int MWGId)
        {
            bool result = false;
            var foundUser = _context.UserInfo.SingleOrDefault(user => user.Id == userId);

            if (foundUser != null)
            {
                int position = foundUser.FavoritedMWGId.IndexOf(MWGId.ToString());
                int lengthOfMWGId = MWGId.ToString().Length;
                if (position == foundUser.FavoritedMWGId.Length - lengthOfMWGId)
                {
                    foundUser.FavoritedMWGId = foundUser.FavoritedMWGId.Remove(position - 1, lengthOfMWGId+1);
                }
                else
                {
                    foundUser.FavoritedMWGId = foundUser.FavoritedMWGId.Remove(position, lengthOfMWGId
                    +1);
                }
                _context.Update<UserModel>(foundUser);
                result = _context.SaveChanges() != 0;
            }
            return result;
        }

        // Edit user icon
        public bool EditUserIcon(int userId, string iconName)
        {
            bool result = false;

            List<MWGModel> MWGModelsThatContainsThisUser = _context.MWGInfo.Where(user => user.MembersId.Contains(userId.ToString())).ToList();
            List<InvitationModel> InvitationModelsThatContainsThisUser = _context.InvitationInfo.Where(user => user.UserId == userId).ToList();


            var foundUser = _context.UserInfo.SingleOrDefault(user => user.Id == userId);

            if (foundUser != null) {

                if(MWGModelsThatContainsThisUser != null)
                {
                    foreach(MWGModel item in MWGModelsThatContainsThisUser)
                    {
                        item.MembersIcons = item.MembersIcons.Replace(foundUser.Icon, iconName);
                        _context.Update<MWGModel>(item);
                        result = _context.SaveChanges() != 0;
                    }
                }

                if(InvitationModelsThatContainsThisUser != null)
                {
                    foreach(InvitationModel item in InvitationModelsThatContainsThisUser)
                    {
                        item.UserIcon = iconName;
                        _context.Update<InvitationModel>(item);
                        result = _context.SaveChanges() != 0;
                    }
                }

                foundUser.Icon = iconName; 
                _context.Update<UserModel>(foundUser);
                result = _context.SaveChanges() != 0;
            }
            return result;
        }

        public bool EditUsername(int userId, string newUsername)
        {
            bool result = false;
            UserModel foundUser = _context.UserInfo.SingleOrDefault(user => user.Id == userId);

            //trying to get all MWGStatusModels that has the userId within the string of MembersId?
            List<MWGStatusModel> MWGStatusThatContainsThisUser = _context.MWGStatusInfo.Where(user => user.MembersId.Contains(userId.ToString())).ToList();
            List<MWGModel> MWGModelsThatContainsThisUser = _context.MWGInfo.Where(user => user.MembersId.Contains(userId.ToString())).ToList();
            List<InvitationModel> InvitationModelsThatContainsThisUser = _context.InvitationInfo.Where(user => user.UserId == userId).ToList();

            if(foundUser != null)
            {

                if(MWGStatusThatContainsThisUser != null)
                {
                    foreach(MWGStatusModel item in MWGStatusThatContainsThisUser)
                    {
                        //attempt to replace the name of the current with the new one in the string of members names
                        item.MembersNames = item.MembersNames.Replace(foundUser.Username, newUsername);
                        _context.Update<MWGStatusModel>(item);
                        result = _context.SaveChanges() != 0;
                    }
                }
                if(MWGModelsThatContainsThisUser != null)
                {
                    foreach(MWGModel item in MWGModelsThatContainsThisUser)
                    {
                        //attempt to replace the name of the current with the new one in the string of members names
                        item.MembersNames = item.MembersNames.Replace(foundUser.Username, newUsername);
                        _context.Update<MWGModel>(item);
                        result = _context.SaveChanges() != 0;
                    }
                }
                if(InvitationModelsThatContainsThisUser != null)
                {
                    foreach(InvitationModel item in InvitationModelsThatContainsThisUser)
                    {
                        //attempt to replace the name of the current with the new one in the string of members names
                        item.UserName = newUsername;
                        _context.Update<InvitationModel>(item);
                        result = _context.SaveChanges() != 0;
                    }
                }
                
                foundUser.Username = newUsername;
                _context.Update<UserModel>(foundUser);
                result = _context.SaveChanges() != 0;
            }
            return result;
        }
    
        public bool EditPassword(int userId, string? newPassword)
        {
            bool result = false;
            UserModel foundUser = FindUserById(userId);
            if(foundUser != null)
            {
            var hashedPassword = HashPassword(newPassword);
            foundUser.Salt = hashedPassword.Salt;
            foundUser.Hash = hashedPassword.Hash;

            _context.Update<UserModel>(foundUser);
            result = _context.SaveChanges()!=0;
            }

            return result;

        }
        
        //This just checks if the password is correct without generating a new token for to change password?
        public bool CheckPassword([FromBody] LoginDTO user)
        {
            bool result = false;
            if (DoesUserExists(user.Username))
            {
                var foundUser = FindUserByUsername(user.Username);
                var verifyPass = VerifyUserPassword(user.Password, foundUser.Hash, foundUser.Salt);
                if (verifyPass)
                {
                    result = true;
                }
            }
            return result;
        }
    }
}