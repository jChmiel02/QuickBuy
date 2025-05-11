using AutoMapper;
using QuickBuy.Database.DbContext;
using QuickBuy.Database.Models;
using QuickBuy.Database.Models.Dto;
using QuickBuy.UoW.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace QuickBuy.UoW
{
    public class ManageUsersUoW : IManageUsersUoW
    {
        private readonly QuickBuyDbContext _context;
        private readonly IMapper _mapper;

        public ManageUsersUoW(QuickBuyDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<User> CreateUser(UserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);
            try
            {
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return user;
        }

        public async Task<User> GetUserById(int id)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<bool> CheckIfUserExist(UserDto userDto)
        {
            return await _context.Users
                .AnyAsync(u => u.Name == userDto.Name || u.Email == userDto.Email);
        }

        public async Task<bool> ChangePassword(UserDto userDto, string newPassword)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Name == userDto.Name && u.Password == userDto.Password);
            if (user == null)
            {
                return false;
            }
            user.Password = newPassword;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<User> CheckIfUserPasswordCorrect(UserDto userDto)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => (u.Name == userDto.Name || u.Email == userDto.Email));

            if (user == null)
            {
                return null;
            }

            if (user.Password.Equals(userDto.Password))
            {
                return user;
            }

            return null;
        }
    }
}
