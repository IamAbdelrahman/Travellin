//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;

//namespace Travellin.Travellin.Api.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//using Microsoft.AspNetCore.Authorization;
//    using Microsoft.AspNetCore.Mvc;
//    using Microsoft.EntityFrameworkCore;

//    namespace Nestin.Api.Controllers
//    {
//        public class AccountsController : BaseController
//        {
//            private readonly IIdentityFactory _identityFactory;
//            private readonly IServiceFactory _serviceFactory;

//            public AccountsController(IUnitOfWork unitOfWork, IIdentityFactory identityFactory, IServiceFactory serviceFactory) : base(unitOfWork)
//            {
//                _identityFactory = identityFactory;
//                _serviceFactory = serviceFactory;
//            }

//            [HttpPost("register")]
//            [EndpointSummary("Register new user.")]
//            [Consumes("application/json")]
//            [Produces("application/json")]
//            [ProducesResponseType(typeof(NewUserDto), StatusCodes.Status201Created)]
//            [ProducesResponseType(StatusCodes.Status429TooManyRequests)]
//            public async Task<IActionResult> Register([FromBody] RegisterDto dto)
//            {
//                var username = ExtractUsernameFromEmail(dto.Email);

//                var appUser = new AppUser
//                {
//                    UserName = username,
//                    Email = dto.Email,
//                };

//                var createdUser = await _identityFactory.UserManager.CreateAsync(appUser, dto.Password);

//                if (createdUser.Succeeded)
//                {
//                    var roleResult = await _identityFactory.UserManager.AddToRoleAsync(appUser, "Guest");

//                    if (roleResult.Succeeded)
//                    {
//                        var user = await _identityFactory.UserManager.Users.Include(x => x.Roles)
//                            .FirstAsync(x => x.Id == appUser.Id);
//                        var token = _serviceFactory.AuthTokenService.CreateToken(user);
//                        _serviceFactory.AuthTokenService.SetAccessTokenCookie(HttpContext, token);

//                        var roles = await _identityFactory.UserManager.GetRolesAsync(appUser);

//                        return StatusCode(201, new NewUserDto
//                        {
//                            Id = appUser.Id,
//                            UserName = appUser.UserName,
//                            Roles = roles.ToList(),
//                            Token = token,
//                        });
//                    }
//                    else
//                    {
//                        return StatusCode(500, roleResult.ToErrorList());
//                    }
//                }

//                return StatusCode(500, createdUser.ToErrorList());
//            }

//            [HttpPost("login")]
//            [EndpointSummary("Login user.")]
//            [Consumes("application/json")]
//            [Produces("application/json")]
//            [ProducesResponseType(typeof(NewUserDto), StatusCodes.Status200OK)]
//            [ProducesResponseType(StatusCodes.Status429TooManyRequests)]
//            public async Task<IActionResult> Login([FromBody] LoginDto dto)
//            {
//                var userName = ExtractUsernameFromEmail(dto.Email);

//                var user = await _identityFactory.UserManager.Users
//                    .Include(x => x.Roles)
//                    .FirstOrDefaultAsync(x => x.UserName == userName);

//                if (user is not null)
//                {
//                    var passCheckResult = await _identityFactory.SignInManager.CheckPasswordSignInAsync(user, dto.Password, false);

//                    if (passCheckResult.Succeeded)
//                    {
//                        var token = _serviceFactory.AuthTokenService.CreateToken(user);
//                        // Set secure HTTP-only cookie
//                        _serviceFactory.AuthTokenService.SetAccessTokenCookie(HttpContext, token);

//                        var roles = await _identityFactory.UserManager.GetRolesAsync(user);

//                        return Ok(new NewUserDto
//                        {
//                            Id = user.Id,
//                            UserName = user.UserName,
//                            Roles = roles.ToList(),
//                            Token = token
//                        });
//                    }
//                }

//                return Unauthorized("Invalid creditionals.");
//            }

//            [HttpPost("logout")]
//            public IActionResult Logout()
//            {
//                _serviceFactory.AuthTokenService.UnsetAccessTokenCookie(HttpContext);
//                return Ok();
//            }

//            [Authorize]
//            [HttpPost("change-password")]
//            [EndpointSummary("Change password loggedIn user password")]
//            [Consumes("application/json")]
//            [Produces("application/json")]
//            [ProducesResponseType(StatusCodes.Status204NoContent)]
//            [ProducesResponseType(typeof(IEnumerable<string>), StatusCodes.Status400BadRequest)]
//            [ProducesResponseType(StatusCodes.Status401Unauthorized)]
//            public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto changePasswordDto)
//            {
//                // Get current user from Identity
//                var user = await _identityFactory.UserManager.FindByIdAsync(CurrentUser.Id);
//                if (user == null)
//                {
//                    return Unauthorized("User not found");
//                }

//                // Verify old password matches
//                var isOldPasswordValid = await _identityFactory.UserManager.CheckPasswordAsync(user, changePasswordDto.OldPassword);
//                if (!isOldPasswordValid)
//                {
//                    return BadRequest("Current password is incorrect");
//                }

//                // Check if new password is different
//                if (changePasswordDto.OldPassword == changePasswordDto.NewPassword)
//                {
//                    return BadRequest("New password must be different from current password");
//                }

//                // Change password using Identity
//                var changeResult = await _identityFactory.UserManager.ChangePasswordAsync(
//                    user,
//                    changePasswordDto.OldPassword,
//                    changePasswordDto.NewPassword);

//                if (!changeResult.Succeeded)
//                {
//                    return BadRequest(changeResult.Errors.Select(e => e.Description));
//                }


//                _serviceFactory.AuthTokenService.UnsetAccessTokenCookie(HttpContext);

//                return NoContent();
//            }

//            private string ExtractUsernameFromEmail(string email)
//            {
//                if (string.IsNullOrEmpty(email)) return email;
//                return new MailAddress(email).User;
//            }
//        }
//    }
//}
