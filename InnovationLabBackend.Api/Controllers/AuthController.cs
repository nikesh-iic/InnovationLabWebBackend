using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using InnovationLabBackend.Api.DTO.Users;
using InnovationLabBackend.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace InnovationLabBackend.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _configuration;

        public AuthController(UserManager<User> userManager, SignInManager<User> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        private async Task<string> GenerateJwtToken(User user)
        {
            var jwtKey = _configuration["Jwt:Key"];
            var jwtIssuer = _configuration["Jwt:Issuer"];
            var jwtAudience = _configuration["Jwt:Audience"];

            if (string.IsNullOrEmpty(jwtKey) || string.IsNullOrEmpty(jwtIssuer) || string.IsNullOrEmpty(jwtAudience))
            {
                throw new InvalidOperationException("JWT Key, Issuer or Audience is not configured correctly in appsettings.");
            }

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.Email, user.Email!),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(new ClaimsIdentityOptions().SecurityStampClaimType, await _userManager.GetSecurityStampAsync(user))
            };

            var userRoles = await _userManager.GetRolesAsync(user);
            foreach (var role in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(1),
                Issuer = jwtIssuer,
                Audience = jwtAudience,
                SigningCredentials = credentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterDTO registerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new User
            {
                UserName = registerDto.Email,
                Email = registerDto.Email,
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                CreatedAt = DateTimeOffset.UtcNow,
                IsActive = true
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (result.Succeeded)
            {
                await _userManager.ResetAuthenticatorKeyAsync(user);
                await _userManager.SetTwoFactorEnabledAsync(user, true);

                var secret = await _userManager.GetAuthenticatorKeyAsync(user);
                const string issuer = "InnovationLab";
                var qrCodeUri = $"otpauth://totp/{issuer}:{user.Email}?secret={secret}&issuer={issuer}&digits=6";

                return Ok(new { Message = "User registered successfully", QrCodeUri = qrCodeUri });
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return BadRequest(ModelState);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDTO loginDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null || !user.IsActive)
            {
                return Unauthorized(new { Message = "Invalid credentials or user inactive" });
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                if (await _userManager.GetTwoFactorEnabledAsync(user))
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                }

                return Ok(new
                {
                    Message = "Two-factor authentication is required",
                    Requires2fa = true,
                    Email = user.Email
                });
            }

            if (result.IsLockedOut)
            {
                return Unauthorized(new { Message = "User account locked out" });
            }
            return Unauthorized(new { Message = "Invalid credentials" });
        }

        [HttpPost("verify")]
        public async Task<IActionResult> VerifyOtp([FromBody] UserVerifyDTO userVerifyDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userManager.FindByEmailAsync(userVerifyDTO.Email);
            if (user == null)
            {
                return Unauthorized(new { Message = "Invalid user" });
            }

            var isValidOtp = await _userManager.VerifyTwoFactorTokenAsync(user, TokenOptions.DefaultAuthenticatorProvider, userVerifyDTO.Otp);

            if (!isValidOtp)
            {
                return Unauthorized(new { Message = "Invalid or expired otp" });
            }

            var token = await GenerateJwtToken(user);
            return Ok(new
            {
                Message = "Login successful",
                Token = token
            });
        }

        [Authorize]
        [HttpGet("profile")]
        public async Task<IActionResult> GetUserInfo()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(new
            {
                user.Id,
                user.FirstName,
                user.LastName,
                user.Email,
                user.CreatedAt,
                user.IsActive
            });
        }

        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                // Update the security stamp to invalidate existing tokens for this user
                await _userManager.UpdateSecurityStampAsync(user);
            }

            await _signInManager.SignOutAsync();
            return NoContent();
        }
    }
}
