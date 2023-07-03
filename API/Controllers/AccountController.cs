using API.Contracts;
using API.Models;
using API.Repositories;
using API.Utility;
using API.ViewModels.Account;
using API.ViewModels.Others;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : GeneralController<Account, AccountVM>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper<Account, AccountVM> _mapper;
        private readonly ITokenService _tokenService;
        private readonly IEmailService _emailService;
        public AccountController(IAccountRepository accountRepository, 
            IMapper<Account, AccountVM> mapper, 
            ITokenService tokenService,
            IUserRepository userRepository,
            IEmailService emailService) : base(accountRepository, mapper)
        {
            _accountRepository = accountRepository;
            _mapper = mapper;
            _tokenService = tokenService;
            _userRepository = userRepository;
            _emailService = emailService;
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public IActionResult Login(LoginVM loginVM)
        {
            var account = _accountRepository.Login(loginVM);

            if (account == null)
            {
                return NotFound(new ResponseVM<AccountVM>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Invalid username or password",
                    Data = null
                });
            }

            if (!Hashing.ValidatePassword(loginVM.Password, account.Password))
            {
                return BadRequest(new ResponseVM<AccountVM>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Message = "Password Invalid",
                    Data = null
                });
            }

            var user = _accountRepository.GetUserByUsername(account.Username);
            if (user == null)
            {
                return NotFound(new ResponseVM<AccountVM>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Email not found",
                    Data = null
                });
            }

            var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Username),
            new(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
            new(ClaimTypes.Email, user.Email),
            new Claim("GuidValue", user.Guid.ToString())
        };

            var roles = _accountRepository.GetRoles(user.Guid);

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var token = _tokenService.GenerateToken(claims);

            return Ok(new ResponseVM<string>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Login Success",
                Data = token
            });

        }

        [HttpPost("Register")]
        [AllowAnonymous]
        public IActionResult Register(RegisterVM registerVM)
        {

            var result = _accountRepository.Register(registerVM);
            switch (result)
            {
                case 0:
                    return BadRequest(new ResponseVM<AccountVM>
                    {
                        Code = StatusCodes.Status400BadRequest,
                        Status = HttpStatusCode.BadRequest.ToString(),
                        Message = "Registration Failed",
                        Data = null
                    });
                case 1:
                    return BadRequest(new ResponseVM<AccountVM>
                    {
                        Code = StatusCodes.Status400BadRequest,
                        Status = HttpStatusCode.BadRequest.ToString(),
                        Message = "Email already exists",
                        Data = null
                    });
                case 2:
                    return BadRequest(new ResponseVM<AccountVM>
                    {
                        Code = StatusCodes.Status400BadRequest,
                        Status = HttpStatusCode.BadRequest.ToString(),
                        Message = "Phone number already exists",
                        Data = null
                    });
                case 3:
                    return Ok(new ResponseVM<AccountVM>
                    {
                        Code = StatusCodes.Status200OK,
                        Status = HttpStatusCode.OK.ToString(),
                        Message = "Registration Success",
                        Data = null
                    });
            }

            return BadRequest(new ResponseVM<AccountVM>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Registration Failed",
                Data = null
            });

        }

        [HttpPost("RegisterEM")]
        [AllowAnonymous]
        public IActionResult RegisterEM(RegisterVM registerVM)
        {

            var result = _accountRepository.RegisterEM(registerVM);
            switch (result)
            {
                case 0:
                    return BadRequest(new ResponseVM<AccountVM>
                    {
                        Code = StatusCodes.Status400BadRequest,
                        Status = HttpStatusCode.BadRequest.ToString(),
                        Message = "Registration Failed",
                        Data = null
                    });
                case 1:
                    return BadRequest(new ResponseVM<AccountVM>
                    {
                        Code = StatusCodes.Status400BadRequest,
                        Status = HttpStatusCode.BadRequest.ToString(),
                        Message = "Email already exists",
                        Data = null
                    });
                case 2:
                    return BadRequest(new ResponseVM<AccountVM>
                    {
                        Code = StatusCodes.Status400BadRequest,
                        Status = HttpStatusCode.BadRequest.ToString(),
                        Message = "Phone number already exists",
                        Data = null
                    });
                case 3:
                    return Ok(new ResponseVM<AccountVM>
                    {
                        Code = StatusCodes.Status200OK,
                        Status = HttpStatusCode.OK.ToString(),
                        Message = "Registration Success",
                        Data = null
                    });
            }

            return BadRequest(new ResponseVM<AccountVM>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Registration Failed",
                Data = null
            });

        }

        [HttpPost("ForgotPassword/{email}")]
        [AllowAnonymous]
        public IActionResult UpdateResetPass(string email)
        {

            var getGuid = _userRepository.FindGuidByEmail(email);
            if (getGuid == null)
            {
                return NotFound(new ResponseVM<AccountVM>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Email not Found",
                    Data = null
                });
            }

            var isUpdated = _accountRepository.UpdateOTP(getGuid);

            switch (isUpdated)
            {
                case 0:
                    return BadRequest(new ResponseVM<AccountVM>
                    {
                        Code = StatusCodes.Status400BadRequest,
                        Status = HttpStatusCode.BadRequest.ToString(),
                        Message = "OTP Failed to Generate",
                        Data = null
                    });
                default:
                    var hasil = new AccountResetPasswordVM
                    {
                        Email = email,
                        OTP = isUpdated
                    };

                    _emailService.SetEmail(email)
                                 .SetSubject("Forgot Password")
                                 .SetHtmlMessage($"Your OTP is {isUpdated}")
                                 .SendEmailAsync();

                    return Ok(new ResponseVM<AccountResetPasswordVM>
                    {
                        Code = StatusCodes.Status200OK,
                        Status = HttpStatusCode.OK.ToString(),
                        Message = "OTP Successfully Sent to Email",
                        Data = hasil
                    });

            }


        }

        [HttpPost("ChangePassword")]
        [AllowAnonymous]
        public IActionResult ChangePassword(ChangePasswordVM changePasswordVM)
        {
            // Cek apakah email dan OTP valid
            var account = _userRepository.FindGuidByEmail(changePasswordVM.Email);
            var changePass = _accountRepository.ChangePasswordAccount(account, changePasswordVM);
            switch (changePass)
            {
                case 0:
                    return BadRequest(new ResponseVM<ChangePasswordVM>
                    {
                        Code = StatusCodes.Status400BadRequest,
                        Status = HttpStatusCode.BadRequest.ToString(),
                        Message = "Unable to Change Password",
                        Data = null
                    });
                case 1:
                    return Ok(new ResponseVM<ChangePasswordVM>
                    {
                        Code = StatusCodes.Status200OK,
                        Status = HttpStatusCode.OK.ToString(),
                        Message = "Password has been changed successfully",
                        Data = null
                    });
                case 2:
                    return BadRequest(new ResponseVM<ChangePasswordVM>
                    {
                        Code = StatusCodes.Status400BadRequest,
                        Status = HttpStatusCode.BadRequest.ToString(),
                        Message = "Invalid OTP",
                        Data = null
                    });
                case 3:
                    return BadRequest(new ResponseVM<ChangePasswordVM>
                    {
                        Code = StatusCodes.Status400BadRequest,
                        Status = HttpStatusCode.BadRequest.ToString(),
                        Message = "OTP has already been used",
                        Data = null
                    });
                case 4:
                    return BadRequest(new ResponseVM<ChangePasswordVM>
                    {
                        Code = StatusCodes.Status400BadRequest,
                        Status = HttpStatusCode.BadRequest.ToString(),
                        Message = "OTP expired",
                        Data = null
                    });
                case 5:
                    return BadRequest(new ResponseVM<ChangePasswordVM>
                    {
                        Code = StatusCodes.Status400BadRequest,
                        Status = HttpStatusCode.BadRequest.ToString(),
                        Message = "Wrong Password, Not the Same",
                        Data = null
                    });
                default:
                    return BadRequest(new ResponseVM<ChangePasswordVM>
                    {
                        Code = StatusCodes.Status400BadRequest,
                        Status = HttpStatusCode.BadRequest.ToString(),
                        Message = "Unable to Change Password",
                        Data = null
                    });
            }
        }
    }
}
