using CleanArchitecture.Application.Ecommerce.IServices.Users;
using CleanArchitecture.Application.Helpers;
using CleanArchitecture.Domain.Ecommerce.Entities.Users;
using CleanArchitecture.Domain.Ecommerce.IRepository.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Ecommerce.Services.Users
{
    public class UserService : IUserService
    {
        private IUserRepository _userRepository;
        public UserService(IUserRepository userRepository) { 
            _userRepository = userRepository;
        }

        public async Task<DatatableResponse<IEnumerable<User>>> GetUserData()
        {
            var query = await _userRepository.GetUserData();
            var data = query.ToList();

            return new DatatableResponse<IEnumerable<User>>
            {
                Data = data,
                Draw = 1,
                RecordsFiltered = 1,
                RecordsTotal = data.Count
            };
        }
    }
}
