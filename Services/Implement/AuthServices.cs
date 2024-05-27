using AutoMapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using trello_services.Data;
using trello_services.Entities;
using trello_services.Helpers;
using trello_services.IRepository;
using trello_services.Models.Request;
using trello_services.Models.Response;

namespace trello_services.Services.Implement
{
    public class AuthServices : IAuth
    {
        private readonly ApplicationDBContext _context;
        private readonly IMapper _mapper;
        public AuthServices(ApplicationDBContext context , IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<UserResponseVM> AuthenticatedAsync(UserLoginModel login)
        {
            var user = await _context.Users.Where(user => user.email == login.email)
                                            .SingleOrDefaultAsync();
            if (user == null) return null;
            if (PasswordConvert.DecryptPasswordBase64(user.password) != login.password) return null;
            return _mapper.Map<UserResponseVM>(user);
        }

        public async Task<bool> RegisterAsync(UserLoginModel register)
        {
            var _user = new User
            {
                userId = Guid.NewGuid(),
                email = register.email,
                password = PasswordConvert.EncryptPasswordBase64(register.password)
            };
            await _context.Users.AddAsync(_user);
            await _context.SaveChangesAsync();
            return true;
        }

        public Task<bool> VerifyEmailAsync(UserLoginModel register)
        {
            throw new NotImplementedException();
        }
    }
}
