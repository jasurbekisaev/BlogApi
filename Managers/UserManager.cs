using BlogApi.Context;
using BlogApi.DtoModels.UserDtoModel;
using BlogApi.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BlogApi.Managers;

public class UserManager
{
    private readonly BlogdbContext _context;
    private ILogger<UserManager> _logger;
    private readonly JwtTokenManager _jwtTokenManager;

    public UserManager(BlogdbContext context, ILogger<UserManager> logger, JwtTokenManager jwtTokenManager)
    {
        _context = context;
        _logger = logger;
        _jwtTokenManager = jwtTokenManager;
    }

    public async Task<User> Register(CreateUserDto model)
    {
        if (await _context.Users.AnyAsync(u => u.UserName == model.Username))
        {
            throw new Exception("Username already exists");
        }

        User user = new User()
        {
            Name = model.Name,
            UserName = model.Username
        };

        user.PasswordHash = new PasswordHasher<User>().HashPassword(user, model.Password);
        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return user;
    }

    public async Task<string> Login(LoginUserDto model)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == model.Username);
        if (user == null)
        {
            throw new Exception("Username or Password is incorrect");
        }

        var result = new PasswordHasher<User>().VerifyHashedPassword(user, user.PasswordHash, model.Password);

        if (result == PasswordVerificationResult.Failed)
        {
            throw new Exception("Username or Password is incorrect");
        }

        var token = _jwtTokenManager.GenerateToken(user);

        return token;
    }

    public async Task<User?> GetUser(string username)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.UserName == username);
    }
    public async Task<User?> GetUser(Guid id)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.UserId == id);
    }

}
