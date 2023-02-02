using Alisveris.Business;
using Alisveris.Business.Common;
using Alisveris.Business.Models.Admin;
using Alisveris.Data.Entities;
using Alisveris.Data.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AlisverisProjesi.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private IConfiguration _configuration;
        private IUnitOfWork _unitOfWork;
        private IAdminManager _adminManager;
        public AdminController(IUnitOfWork unitOfWork, IConfiguration configuration, IAdminManager adminManager)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
            _adminManager = adminManager;
        }

        //[Authorize(Roles = "admin")]
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login(LoginAdminModel model)
        {
            Admin admin = _adminManager.Authenticate(model);

            if (admin == null)
            {
                ModelState.AddModelError("", "Kullanıcı adı veya şifre hatalı.");
                return BadRequest(ModelState);
            }

            string secretKey = _configuration.GetValue<string>("Authentication:SecretKey");
            byte[] key = Encoding.UTF8.GetBytes(secretKey);

            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(key);
            SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim("app", "alisveris.api"));
            claims.Add(new Claim(ClaimTypes.NameIdentifier, admin.Id.ToString()));
            claims.Add(new Claim(ClaimTypes.Name, admin.Username));
            claims.Add(new Claim(ClaimTypes.Role, admin.Role));

            JwtSecurityToken securityToken =
                new JwtSecurityToken(
                    claims: claims,
                    signingCredentials: credentials,
                    expires: DateTime.Now.AddDays(1));

            string token = new JwtSecurityTokenHandler().WriteToken(securityToken);

            return Ok(token);
        }

        //[Authorize(Roles = "admin")]
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Register(RegisterAdminModel model)
        {
            Admin admin = new Admin();
            admin.Username = model.Username;
            admin.Password = model.Password;
            admin.Role = Roles.Admin;

            _unitOfWork.Admin.Add(admin);
            _unitOfWork.Save();

            return Created("", "");
        }

        //[Authorize(Roles = "admin")]
        [AllowAnonymous]
        [HttpGet]
        public IActionResult List()
        {
            return Ok(_unitOfWork.Admin.GetAll());
        }

        //[Authorize(Roles = "admin")]
        [AllowAnonymous]
        [HttpPut("{id}")]
        public IActionResult GetAdminById(int id)
        {
            Admin admin = _unitOfWork.Admin.GetFirstOrDefault(x => x.Id == id);
            return Ok(admin);
        }

        //[Authorize(Roles = "admin")]
        [AllowAnonymous]
        [HttpDelete("{id}")]
        public IActionResult RemoveAdmin(int id)
        {
            Admin admin = _unitOfWork.Admin.GetFirstOrDefault(x => x.Id == id);

            if (admin == null)
            {
                NotFound();
            }
            _unitOfWork.Admin.Remove(admin);
            _unitOfWork.Save();
            return Ok();
        }
    }
}
