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
using System.Threading.Tasks;

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
            var user = _userService.Authenticate(userDto.Email, userDto.Password).Result;

            if (user == null)
                return BadRequest("Email or password is incorrect");

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
                Email = user.Email,
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
            var user = _mapper.Map<ApplicationUser>(userDto);

            try 
            {
                // validation
                if (string.IsNullOrWhiteSpace(userDto.Password))
                    return BadRequest("Password is required");

                if (_userService.GetAll().Any(x => x.Email == user.Email))
                    return BadRequest($"Email '{user.Email}' is already in use");
                
                // save 
                var result = _userService.Create(user, userDto.Password);
                if(result != null && result.Any())
                {
                    return BadRequest(result);
                }
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

        //[HttpPut("{id}")]
        //public IActionResult Update(Guid id, [FromBody]UserDto userDto)
        //{
        //    // map dto to entity and set id
        //    var userParam = _mapper.Map<ApplicationUser>(userDto);
        //    userParam.Id = id;

        //    var user = _userService.GetById(id);

        //    try 
        //    {
        //        if (user == null)
        //            return BadRequest("User not found");

        //        if (userParam.Email != user.Email)
        //        {
        //            // username has changed so check if the new username is already taken
        //            if (_userService.GetAll().Any(x => x.Email == userParam.Email))
        //                return BadRequest($"Username '{userParam.Email}' is already taken");
        //        }

        //        // update user properties
        //        user.FirstName = userParam.FirstName;
        //        user.LastName = userParam.LastName;
        //        user.Email = userParam.Email;

        //        // save 
        //        _userService.Update(user);
        //        return Ok();
        //    } 
        //    catch(AppException ex)
        //    {
        //        // return error message if there was an exception
        //        return BadRequest(ex.Message);
        //    }
        //}

        //[HttpDelete("{id}")]
        //public IActionResult Delete(Guid id)
        //{
        //    _userService.Delete(id);
        //    return Ok();
        //}

        [HttpGet("[action]")]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(Guid userId, string code)
        {
            // currently not used
            if (userId == null || code == null)
            {
                return BadRequest();
            }
            var user = _userService.GetById(userId);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{userId}'.");
            }
            await _userService.ConfirmEmailAsync(user, code);
            return Ok();
        }
    }
}
