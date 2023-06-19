using AutoMapper;
using BusinessObject.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository;
using System.Security.Claims;
using System.Net;
using BusinessObject.DTOs;

namespace StoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAccountRepository _repo;
        private readonly IMapper _mapper;
        private ApiResponse _response;
        public AuthController(IAccountRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
            _response = new ApiResponse();
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Get(AccountLoginDTO c)
        {
            var newCus = _mapper.Map<Account>(c);
            var cus = _repo.Login(newCus);
            if (cus != null)
            {
                List<Claim> claims = new List<Claim>() {
                    new Claim(ClaimTypes.NameIdentifier, c.Email),
                    new Claim(ClaimTypes.Role, cus.Role)
                };

                ClaimsIdentity identity = new ClaimsIdentity(claims,
                    CookieAuthenticationDefaults.AuthenticationScheme);

                AuthenticationProperties properties = new AuthenticationProperties()
                {
                    AllowRefresh = true,
                    IsPersistent = true,
                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(identity), properties);
                _response.IsSuccess = true;
                _response.StatusCode = HttpStatusCode.OK;
                _response.Result = cus;
                return Ok(_response);
            }
            _response.IsSuccess = false;
            _response.StatusCode = HttpStatusCode.NotFound;
            _response.ErrorMessages.Add("Not Found!");
            return NotFound(_response);
        }

        [HttpPost]
        [Route("Register")]
        public IActionResult Post(RegisterDTO c)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (!c.Password.Equals(c.ConfirmPassword))
                    {
                        _response.IsSuccess = false;
                        _response.ErrorMessages.Add("Confirm Password Is Incorrect!");
                        _response.StatusCode = HttpStatusCode.BadRequest;
                    }
                    var user = _repo.SearchByKeyword(c.Email);
                    if(user.Count == 0)
                    {
                        var newUser = _mapper.Map<Account>(c);
                        newUser.Role = "customer";
                        _repo.CreateNewAccount(newUser);
                        _response.IsSuccess = true;
                        _response.StatusCode = HttpStatusCode.OK;
                        return Ok(_response);
                    }
                    else
                    {
                        _response.IsSuccess = false;
                        _response.ErrorMessages.Add("Email already exists!");
                        _response.StatusCode = HttpStatusCode.BadRequest;
                    }
                    
                    return BadRequest(_response);
                }
                else
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.ErrorMessages = new List<string>()
                {
                    ex.ToString()
                };
                return BadRequest(_response);
            }
            
        }

        [HttpPost]
        [Route("Logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Ok("Logged out successfully");
        }
    }
}
