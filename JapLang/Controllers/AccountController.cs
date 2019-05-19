using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using JapLang.BLL.DTO.Account;
using JapLang.ViewModels;
using JCL.DAL.Entity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace JapLang.Controllers
{
    [Produces("application/json")]
    [Route("api/account")]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IMapper mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
        }

        public object AuthTokenOptions { get; private set; }

        [HttpPost("join")]
        public async Task<IActionResult> Join([FromBody]RegisterDTO model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    ApplicationUser user = await _userManager.FindByNameAsync(model.Login);
                    var check = await _userManager.CheckPasswordAsync(user, model.Password);
                    if (!check)
                    {
                        ModelState.AddModelError("", "Неверный логин или пароль.");
                    }
                    else
                    {
                        var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name, user.UserName)
                        };
                        var role = (await _userManager.GetRolesAsync(user)).FirstOrDefault();
                        if(role != null)
                        {
                            claims.Add(new Claim(ClaimTypes.Role, role));
                        }

                        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                        // создаем объект ClaimsIdentity
                        var principal = new ClaimsPrincipal(identity);

                        var login = HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                        return Ok(role);
                    }
                    return BadRequest(ModelState);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            return BadRequest(ModelState);
        }

        [HttpPost("reg")]
        public async Task<IActionResult> Register([FromBody]RegisterDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            };
            try
            {
                ApplicationUser user = new ApplicationUser { UserName = model.Login };
                IdentityResult res = await _userManager.CreateAsync(user, model.Password);
                if (!res.Succeeded)
                {
                    throw new Exception();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Не удалось зарегистрироваться");
            }

            return Ok();
        }

        [HttpGet("role")]
        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetRolesForUser()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var role = await _userManager.GetRolesAsync(user);

            return Ok(role.FirstOrDefault());
        }

        [HttpPost("logout")]
        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Ok();
        }
    }


}
