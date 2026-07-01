using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProEventos.Aula.Application.Dtos;
using ProEventos.Aula.Application.Interface;
using ProEventos.Aula.Domain.Identity;
using ProEventos.Aula.Persistence;
using ProEventos.Aula.Persistence.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProEventos.Aula.Application
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IMapper _mapper;
        private readonly IUserRepositorie _userRepositorie;
        private readonly ProEventoContext _proEventoContext;

        public UserService(UserManager<User> userManager,
                           SignInManager<User> signInManager,
                           IMapper mapper,
                           IUserRepositorie userRepositorie)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _userRepositorie = userRepositorie;
        }
        public async Task<SignInResult> CheckUserPasswordAsync(UserUpdateDto userUpdateDto, string password)
        {

            try
            {
                var user = _userManager.Users.FirstOrDefault(x => x.UserName == userUpdateDto.UserName.ToLower());

                return await _signInManager.CheckPasswordSignInAsync(user, password, false);
            }
            catch(Exception ex)
            {
                
                throw;
            }
        }

        public async Task<UserDto> CreatUserAsync(UserDto userDto)
        {
            try
            {
                var user = _mapper.Map<User>(userDto);
                var result = _userManager.CreateAsync(user, userDto.Password).Result;

                if (result.Succeeded)
                { 
                    return _mapper.Map<UserDto>(user);
                }

                return null;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<UserUpdateDto> GetUserByNomeAsync(string nome)
        {
            try
            {
                var user = await _userRepositorie.GetUserByNomeAsync(nome);

                if(user is null) return null;

                return _mapper.Map<UserUpdateDto>(user);

            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<UserUpdateDto> UpdateUserAsync(UserUpdateDto userUpdateDto)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(userUpdateDto.UserName);

                if (user is null) return null;

                var token = await _userManager.GeneratePasswordResetTokenAsync(user);

                var result = await _userManager.ResetPasswordAsync(user, token, userUpdateDto.Password);

                if (result.Succeeded)
                {
                    _mapper.Map(userUpdateDto, user);

                    _userRepositorie.Update<User>(user);

                    if (await _userRepositorie.SaveChangesAsync())
                    {
                        var userRetorno = await _userRepositorie.GetUserByNomeAsync(user.UserName);

                        return _mapper.Map<UserUpdateDto>(userRetorno);
                    }
                }


                return null;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> UserExists(string nome)
        {
            try
            {
                return await _userManager.Users.AnyAsync(x => x.UserName == nome.ToLower());    
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
