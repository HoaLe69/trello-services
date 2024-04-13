using AutoMapper;
using Microsoft.EntityFrameworkCore;
using trello_services.Data;
using trello_services.Entities;
using trello_services.Helpers;
using trello_services.IRepository;
using trello_services.Models.Request;
using trello_services.Models.Response;

namespace trello_services.Services.Implement
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDBContext _context;
        private readonly IMapper _mapper;
        public UserRepository(ApplicationDBContext context , IMapper mapper) { 
            _context = context;
            _mapper = mapper;
        }

        public async Task<UserResponseVM> GetUserByEmailAsync(string email)
        {
            var user = await _context.Users.Where(u => u.email == email).SingleOrDefaultAsync();
            if (user == null) return null; 
            return _mapper.Map<UserResponseVM>(user);
        }
        public async Task<UserResponseVM?> GetByIdAsync(Guid id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return null;
            }
            return _mapper.Map<UserResponseVM>(user);
        }

        public Task<List<UserResponseVM>> GetByListIdAsync(Guid[] ids)
        {
            throw new NotImplementedException();
        }
        public async Task<UserResponseVM> CreateNewUserAsync(UserRequestModel request)
        {
            var passwordEncrypt = PasswordConvert.EncryptPasswordBase64(request.password);
            var newUser = new User
            {
                userId = Guid.NewGuid(),
                displayName = request.displayName,
                email = request.email,
                password = passwordEncrypt,
                avatar_path = request.avatar_path,
            };
            await _context.Users.AddAsync(newUser);
            await _context.SaveChangesAsync();
            return _mapper.Map<UserResponseVM>(newUser);
        }

        public async Task<UserResponseVM?> UpdateUserAsync(UserRequestModel request , Guid id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return null ;
            }
            //user = _mapper.Map<User>(request);
            if (request.displayName != null) user.displayName = request.displayName;
            if (request.avatar_path != null) user.avatar_path = request.avatar_path;
            await _context.SaveChangesAsync();
            return _mapper.Map<UserResponseVM>(user);
        }

      
    }
}
