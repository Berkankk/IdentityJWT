using IdentityJWT.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace IdentityJWT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationsController : ControllerBase
    {
        private readonly JwtSettings _jwtSettings;

        public AuthenticationsController(IOptions<JwtSettings> jwtSettings) //Ctor geçmekteki amaç program cs de olan sorguları okumak için 
        {
            _jwtSettings = jwtSettings.Value;
        }

        [HttpPost("Login")]
        public IActionResult Login([FromBody] ApiUsers apiUsers)
        {
            var user = CheckIdentity(apiUsers);//Burada kullanıcın kontrolünü yaptık ,aşağıda da saçma metodumuzu geçtik :)
            if (user == null) //user yani kullanıcı null ise kullanıcı bulunamadı dön
            {
                return NotFound("Kullanıcı bulunamadı");
            }

            var token = CreateToken(user);//Kullanıcı doğrulanırsa token oluşturuyoruz
            return Ok(token);


        }

        private string CreateToken(ApiUsers user)
        {
            if (_jwtSettings == null) throw new Exception("Jwt ayarlarında key değerimiz null olamaz");

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claimArray = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Name),
                new Claim(ClaimTypes.Role, user.Role),
            };
            var tokenOlustur = new JwtSecurityToken(_jwtSettings.Issuer,
                _jwtSettings.Audience,
                claimArray,
                expires: DateTime.Now.AddHours(1), //Oluşturulan token ne kadar süre geçerli olsun onu geçtik
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(tokenOlustur);//Token oluşturuldu
        }

        private ApiUsers? CheckIdentity(ApiUsers apiUsers)
        {
            return UserApi
                 .apiUsers
                 .FirstOrDefault(x => x.Name == apiUsers.Name && x.Password == apiUsers.Password);

            //kullanıcımızın adı ve şifresi parametreden gelen değere eşit mi bana gönderilen değerle
        }
    }
}
