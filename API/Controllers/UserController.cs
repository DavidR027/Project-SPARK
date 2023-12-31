﻿using API.Contracts;
using API.Models;
using API.ViewModels.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UserController : GeneralController<User, UserVM>
    {
        public UserController(IUserRepository userRepository, IMapper<User, UserVM> mapper) : base(userRepository, mapper)
        {
        }
    }
}
