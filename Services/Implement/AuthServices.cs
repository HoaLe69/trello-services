using AutoMapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using trello_services.Data;
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
            //var emailParam = new SqlParameter("@email" , login.email);
            //var passwordParam = new SqlParameter("@password" , login.password);
            //var isValidParam = new SqlParameter
            //{
            //    ParameterName = "@isValid",
            //    SqlDbType = System.Data.SqlDbType.Bit,
            //    Direction = System.Data.ParameterDirection.Output
            //};
            //var _user =  _context.Users.FromSqlRaw("exec login_validation @email, @password, @isValid output", emailParam, passwordParam, isValidParam).ToList().FirstOrDefault();
            //// await _context.Database.ExecuteSqlRawAsync("exec login_validation @email, @password, @isValid output", emailParam, passwordParam, isValidParam);
            //Console.WriteLine(_user);
            //if ((bool)isValidParam.Value)
            //{
            //    return _mapper.Map<UserResponseVM>(_user);
            //}
            //return null;
            var user = await _context.Users.Where(user => user.email == login.email)
                                            .SingleOrDefaultAsync();
            if (user == null) return null;
            if (PasswordConvert.DecryptPasswordBase64(user.password) != login.password) return null;
            return _mapper.Map<UserResponseVM>(user);
        }

        public async Task<bool> RegisterAsync(UserLoginModel register)
        {
            await _context.Database.ExecuteSqlInterpolatedAsync($"exec register {register.email},{register.password}");
            return true;
            //var user = await _context.Users.Where(user => user.email == register.email)
            //                                .SingleOrDefaultAsync();
            //if (user != null) return false;
            //var _user = new User
            //{
            //    userId = Guid.NewGuid(),
            //    email =  register.email,
            //    password = PasswordConvert.EncryptPasswordBase64(register.password)
            //};
            //await _context.Users.AddAsync(_user);
            //await _context.SaveChangesAsync();
            //return true;

        }

        public Task<bool> VerifyEmailAsync(UserLoginModel register)
        {
            throw new NotImplementedException();
        }
    }
}
