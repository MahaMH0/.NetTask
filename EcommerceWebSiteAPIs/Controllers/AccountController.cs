using EcommerceWebSiteAPIs.Models;
using EcommerceWebSiteAPIs.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace EcommerceWebSiteAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> Usermanager;
        private readonly IConfiguration Configuration;
        private readonly RoleManager<IdentityRole> RoleManager;

        public AccountController(UserManager<ApplicationUser> _userManager
            , RoleManager<IdentityRole> _roleManager
            , IConfiguration _configuration)
        {
            this.Usermanager = _userManager;
            this.Configuration = _configuration;
            this.RoleManager = _roleManager;

    }
        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterDTO userDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var userExists = await Usermanager.FindByNameAsync(userDto.Username);
            if (userExists != null)
                return BadRequest(error: "User already exists!");
            ApplicationUser user = new ApplicationUser()
            {
                Email = userDto.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = userDto.Username,
                FullName = userDto.FullName,
                Address = userDto.Address,
                DeliveryOptions = userDto.DeliveryOptions,
                PhoneNumber=userDto.Phone,
                Role="User"

            };
            var result = await Usermanager.CreateAsync(user, userDto.password);
            if(await RoleManager.RoleExistsAsync("User") == false)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "User";
                await RoleManager.CreateAsync(role);
                await Usermanager.AddToRoleAsync(user, "User");
            }
            else
            {
                await Usermanager.AddToRoleAsync(user, "User");
            }

            if (!result.Succeeded)
                return BadRequest(result.Errors.FirstOrDefault().Description);
            return Ok(user);
        }

        [HttpPost("Register/Admin")]
        public async Task<IActionResult> RegisterAsAdmin(RegisterDTO userDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var userExists = await Usermanager.FindByNameAsync(userDto.Username);
            if (userExists != null)
                return BadRequest(error: "Admin already exists!");
            ApplicationUser user = new ApplicationUser()
            {
                Email = userDto.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = userDto.Username,
                FullName = userDto.FullName,
                Address = userDto.Address,
                DeliveryOptions = userDto.DeliveryOptions,
                PhoneNumber = userDto.Phone,
                Role="Admin"

            };
            var result = await Usermanager.CreateAsync(user, userDto.password);
            if (await RoleManager.RoleExistsAsync("Admin") == false)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Admin";
                await RoleManager.CreateAsync(role);
                await Usermanager.AddToRoleAsync(user, "Admin");
            }
            else
            {
                await Usermanager.AddToRoleAsync(user, "Admin");
            }

            if (!result.Succeeded)
                return BadRequest(result.Errors.FirstOrDefault().Description);
            return Ok(user);
        }

        [HttpPut("Edit")]
        public async Task<IActionResult> Edit(RegisterDTO userDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var olduser = Usermanager.Users.FirstOrDefault(user => user.UserName == User.Identity.Name); ;
            if (olduser != null)
            {
                olduser.Email = userDto.Email;
                olduser.FullName = userDto.FullName;
                olduser.Address = userDto.Address;
                olduser.DeliveryOptions = userDto.DeliveryOptions;
                olduser.PhoneNumber = userDto.Phone;
                olduser.PasswordHash = Usermanager.PasswordHasher.HashPassword(olduser,userDto.password);
            }
            var result = await Usermanager.UpdateAsync(olduser);
            if (!result.Succeeded)
                return BadRequest(result.Errors.FirstOrDefault().Description);
            return Ok(result);
        }


        [HttpGet]
        public async Task<IActionResult> GetUser()
        { 
            ApplicationUser CurrentUser =Usermanager.Users.FirstOrDefault(user=>user.UserName== User.Identity.Name);
            if (CurrentUser == null)
            {
                return BadRequest("No User Account Login First");
            }
            return Ok(CurrentUser);
        }

        [HttpGet("Admins")]
        public async Task<IActionResult> GetAllAdmins()
        {
            List<ApplicationUser> Admins = Usermanager.Users.Where(user => user.Role == "Admin").ToList();
            if (Admins == null)
            {
                return BadRequest("No Data");
            }
            return Ok(Admins);
        }

        [HttpGet("Users")]
        public async Task<IActionResult> GetAllUsers()
        {
            List<ApplicationUser> Users = Usermanager.Users.Where(user => user.Role == "User").ToList();
            if (Users == null)
            {
                return BadRequest("No Data");
            }
            return Ok(Users);
        }



        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDTO login)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }
            ApplicationUser userModel = await Usermanager.FindByNameAsync(login.UserName);
            if (userModel != null)
            {
                if (await Usermanager.CheckPasswordAsync(userModel, login.Password) == true)
                {
                    //toke base on Claims "Name &Roles &id " +Jti ==>unique Key Token "String"
                    var mytoken = await GenerateToken(userModel);
                    return Ok(new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(mytoken),
                        expiration = mytoken.ValidTo,
                        role = userModel.Role //(User.IsInRole("User") ? "User" : "Admin")
                    });
                }
                else
                {
                    return BadRequest("User Name and Password Not Valid");
                    //return Unauthorized();//401
                }
            }
            return Unauthorized();
        }
        [NonAction]
        public async Task<JwtSecurityToken> GenerateToken(ApplicationUser userModel)
        {
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, userModel.UserName));
            claims.Add(new Claim(ClaimTypes.NameIdentifier, userModel.Id));
            var roles = await Usermanager.GetRolesAsync(userModel);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            //Jti "Identifier Token
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));

            var key =
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Secret"]));
            var mytoken = new JwtSecurityToken(
                audience: Configuration["JWT:ValidAudience"],
                issuer: Configuration["JWT:ValidIssuer"],
                expires: DateTime.Now.AddHours(1),
                claims: claims,
                signingCredentials:
                       new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
                );
            return mytoken;
        }

    }
}
