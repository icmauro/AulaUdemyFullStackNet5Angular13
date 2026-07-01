using Microsoft.AspNetCore.Identity;
using ProEventos.Aula.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProEventos.Aula.Application.Interface
{
    public interface IUserService
    {
        Task<bool> UserExists(string nome);

        Task<UserUpdateDto> GetUserByNomeAsync(string nome);

        Task<SignInResult> CheckUserPasswordAsync(UserUpdateDto userUpdateDto, string password);

        Task<UserDto> CreatUserAsync(UserDto userDto);

        Task<UserUpdateDto> UpdateUserAsync(UserUpdateDto userUpdateDto);
    }
}
