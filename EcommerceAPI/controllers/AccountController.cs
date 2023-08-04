using Ecommerce.BL.DAT;
using Ecommerce.BL.DTA;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using WebApplication2.DAL.models;

namespace Ecommerce.EcommerceAPI.controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private IConfiguration configuration;

        private UserManager<User> userManager;

        public AccountController(IConfiguration configuration, UserManager<User> userManager)
        {
            this.configuration = configuration;
            this.userManager = userManager;
        }


        [HttpPost]
        [Route("resgisteruser")]

        public async Task<IActionResult> registeruser(Registeruser_dto rgu) 
        {
            if(!ModelState.IsValid) 
            {
                return BadRequest(ModelState); 
            }
            var user = rgu.userbinding();
            var password =  await userManager.CreateAsync(user, rgu.password);
            if (!password.Succeeded) 
            {
                return BadRequest(password.Errors);
            }

            List<Claim> claims = new List<Claim>() 
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim (ClaimTypes.Role, user.Role)
            };

            await userManager.AddClaimsAsync(user, claims);

            return Ok();
        }

        [HttpPost]
        [Route("loginuser")]
        public async Task<ActionResult> loginuser(Login_dto lg) 
        {
            var user = await userManager.FindByNameAsync(lg.username);
            if (user != null && await userManager.CheckPasswordAsync(user,lg.password)) 
            {
                var userroles = await userManager.GetRolesAsync(user);
                await userManager.AddClaimAsync(user, new Claim(ClaimTypes.Name, user.UserName));
                await userManager.AddClaimAsync(user, new Claim(ClaimTypes.NameIdentifier, user.Id));
                await userManager.AddClaimAsync(user, new Claim(JwtRegisteredClaimNames.Jti, new Guid().ToString()));

                List<Claim> authclaims = new List<Claim>()
                {
                   new Claim(ClaimTypes.Name, user.UserName),
                   new Claim(ClaimTypes.NameIdentifier, user.Id),
                   new Claim(JwtRegisteredClaimNames.Jti, new Guid().ToString())
                }; 

                foreach(var userrole in userroles) 
                {
                    authclaims.Add(new Claim(ClaimTypes.Role, userrole));
                }

                var securitykeystring = configuration.GetValue<string>("secretkey");
                var securitykeyinbytes = Encoding.ASCII.GetBytes(securitykeystring ?? "");
                SymmetricSecurityKey secretkey = new SymmetricSecurityKey(securitykeyinbytes);

                var token = new JwtSecurityToken(

                    issuer: configuration["JWT:ValidIssuer"],
                    audience: configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddHours(5),
                    claims: authclaims,
                    signingCredentials: new SigningCredentials(secretkey, SecurityAlgorithms.HmacSha256)


                    ) ;

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo,
                    authClaims = authclaims[2].Value

                });


            }
            return Unauthorized();
        }
    }
}
