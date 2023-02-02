using Alisveris.Business;
using Alisveris.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Alisveris.Data.Repository.IRepository;
using Alisveris.Business.Models.Member;
using Alisveris.Business.Common;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace AlisverisProjesi.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        private IConfiguration _configuration;
        private IMemberManager _memberManager;
        private IUnitOfWork _unitOfWork;

        public MemberController(IConfiguration configuration, IMemberManager memberManager, IUnitOfWork unitOfWork)
        {
            _configuration = configuration;
            _memberManager = memberManager;
            _unitOfWork = unitOfWork;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login(LoginMemberModel model)
        {
            Member member = _memberManager.Authenticate(model);

            if (member == null)
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
            claims.Add(new Claim(ClaimTypes.NameIdentifier, member.Id.ToString()));
            claims.Add(new Claim(ClaimTypes.Name, member.Name));
            claims.Add(new Claim(ClaimTypes.Role, member.Role));

            JwtSecurityToken securityToken =
                new JwtSecurityToken(
                    claims: claims,
                    signingCredentials: credentials,
                    expires: DateTime.Now.AddDays(1));

            string token = new JwtSecurityTokenHandler().WriteToken(securityToken);

            return Ok(member);
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Register(RegisterMemberModel model)
        {
            if (model == null)
            {
                BadRequest();
            }

            Member member = new Member();
            member.Name = model.Name;
            member.Email = model.Email;
            member.Password = model.Password;
            member.PhoneNumber = model.PhoneNumber;
            member.Address = model.Address;
            member.Wallet = new Wallet();
            member.ShoppingCard = new ShoppingCard();
            member.Role = Roles.Member;

            _unitOfWork.Member.Add(member);
            _unitOfWork.Save();

            return Created("", "");
        }

        [AllowAnonymous]
        [HttpPut("{id}")]
        public IActionResult EditPassword([FromRoute] int id, [FromBody] EditPasswordModel model)
        {
            Member member = _unitOfWork.Member.GetFirstOrDefault(x => x.Id == id);

            if (member == null)
            {
                return NotFound();
            }

            member.Password = model.Password;

            _unitOfWork.Member.Update(member);
            _unitOfWork.Save();

            return Ok(member);
        }

        //[Authorize(Roles = "admin")]
        [AllowAnonymous]
        [HttpDelete("{id}")]
        public IActionResult DeleteMember(int id)
        {
            Member member = _unitOfWork.Member.GetFirstOrDefault(x => x.Id == id);

            if (member == null)
            {
                return BadRequest(ModelState);
            }

            _unitOfWork.Member.Remove(member);
            _unitOfWork.Save();

            return Ok();
        }

        //[Authorize(Roles = "admin")]
        [AllowAnonymous]
        [HttpGet("{id}")]
        public IActionResult GetMemberById(int id)
        {
            Member member = _unitOfWork.Member.GetFirstOrDefault(x => x.Id == id);

            return Ok(member);
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult List()
        {
            return Ok(_unitOfWork.Member.GetAll());
        }
    }
}
