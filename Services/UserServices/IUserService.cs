using Microsoft.AspNetCore.Mvc;
using Services.UserServices.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.UserServices
{
    public interface IUserService
    {
        Task<UserDto> Register(RegisterDto registerDto);
        Task<UserDto> LogIn(LoginDto loginDto);

        //Task<UserDto> GetCurrentUser();


    }
}
