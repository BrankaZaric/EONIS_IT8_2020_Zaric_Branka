using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using VPDecijeIgracke.Models.AdministratorModel;
using VPDecijeIgracke.Models.KorisnikModel;

namespace VPDecijeIgracke.Authentication
{
    public class Jwt : IJwt
    {
        private readonly IConfiguration configuration;

        public Jwt(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string AdministratorToken(Administrator administrator)
        {

            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["Jwt:Key"]));
            var SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
            var claims = new List<Claim>()
            {
                new Claim("korisnickoIme", administrator.KorisnickoImeAdmin),
                new Claim("uloga", "administrator")
            };

            var token = new JwtSecurityToken(configuration["Jwt:Issuer"],
                                          configuration["Jwt:Audience"],
                                          claims,
                                          expires: DateTime.UtcNow.AddDays(7),
                                          signingCredentials: SigningCredentials);
                                           
            return new JwtSecurityTokenHandler().WriteToken(token);

        }

        public string KorisnikToken(Korisnik korisnik)
        {

            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["Jwt:Key"]));
            var SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
            var claims = new List<Claim>()
            {
                new Claim("korisnickoIme", korisnik.KorisnickoIme),
                new Claim("uloga", "korisnik")
            };

            var token = new JwtSecurityToken(configuration["Jwt:Issuer"],
                                          configuration["Jwt:Audience"],
                                          claims,
                                          expires: DateTime.UtcNow.AddDays(7),
                                          signingCredentials: SigningCredentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


    }
}
