using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Kefcon.Services;
using Kefcon.Dtos;
using AutoMapper;
using System.IdentityModel.Tokens.Jwt;
using Kefcon.Helpers;
using Microsoft.Extensions.Options;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Kefcon.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using System.Linq;

namespace Kefcon.Controllers
{
    [Route("[controller]")]
    public class UsersController : BaseController
    {
        private IUserService _userService;
        private readonly IConfiguration _configuration;

        public UsersController(
            IUserService userService,
            IMapper mapper,
            IConfiguration configuration) : base(mapper)
        {
            _userService = userService;
            _configuration = configuration;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]UserDto userDto)
        {
            var user = _userService.Authenticate(userDto.Username, userDto.Password);

            if (user == null)
                return BadRequest("Username or password is incorrect");

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Secret"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] 
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            // return basic user info (without password) and token to store client side
            return Ok(new {
                Id = user.Id,
                Username = user.Username,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Token = tokenString
            });
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody]UserDto userDto)
        {
            // map dto to entity
            var user = _mapper.Map<User>(userDto);

            try 
            {
                // validation
                if (string.IsNullOrWhiteSpace(userDto.Password))
                    throw new AppException("Password is required");

                if (_userService.GetAll().Any(x => x.Username == user.Username))
                    throw new AppException("Username '" + user.Username + "' is already taken");

                byte[] passwordHash, passwordSalt;
                _userService.CreatePasswordHash(userDto.Password, out passwordHash, out passwordSalt);

                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
                // save 
                _userService.Create(user);
                return Ok();
            } 
            catch(AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var users =  _userService.GetAll();
            var userDtos = _mapper.Map<IList<UserDto>>(users);
            return Ok(userDtos);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            var user =  _userService.GetById(id);
            var userDto = _mapper.Map<UserDto>(user);
            return Ok(userDto);
        }

        [HttpPut("{id}")]
        public IActionResult Update(Guid id, [FromBody]UserDto userDto)
        {
            // map dto to entity and set id
            var userParam = _mapper.Map<User>(userDto);
            userParam.Id = id;

            var user = _userService.GetById(id);

            try 
            {
                if (user == null)
                    throw new AppException("User not found");

                if (userParam.Username != user.Username)
                {
                    // username has changed so check if the new username is already taken
                    if (_userService.GetAll().Any(x => x.Username == userParam.Username))
                        throw new AppException("Username " + userParam.Username + " is already taken");
                }

                // update user properties
                user.FirstName = userParam.FirstName;
                user.LastName = userParam.LastName;
                user.Username = userParam.Username;

                // update password if it was entered
                if (!string.IsNullOrWhiteSpace(userDto.Password))
                {
                    byte[] passwordHash, passwordSalt;
                    _userService.CreatePasswordHash(userDto.Password, out passwordHash, out passwordSalt);

                    user.PasswordHash = passwordHash;
                    user.PasswordSalt = passwordSalt;
                }
                // save 
                _userService.Update(user);
                return Ok();
            } 
            catch(AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            _userService.Delete(id);
            return Ok();
        }
    }
}
