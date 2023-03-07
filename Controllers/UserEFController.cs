using Microsoft.AspNetCore.Mvc;
using SocialSite.Models;
using SocialSite.Data;
using AutoMapper;

namespace SocialSite.Controllers;

[ApiController]
[Route("[controller]")]
public class UserEFController : ControllerBase
{
    private readonly ILogger<UserEFController> _logger;
    private readonly DataContextEF _entityFramework;  

    IMapper _mapper;

    IConfigurationBuilder configBuilder = new ConfigurationBuilder();

    public UserEFController(ILogger<UserEFController> logger, IConfiguration configuration)
    {
        _logger = logger;
        _entityFramework = new DataContextEF(configuration);
        _mapper = new Mapper(new MapperConfiguration(cfg =>{
            cfg.CreateMap<UserDto, User>();
        }));
    }



    [HttpGet("/ef")]
    public IEnumerable<User> GetUsersEF()
    {
        IEnumerable<User> users = _entityFramework.Users.ToList<User>();
        return users;
    }

    [HttpGet("/ef/{idVal}", Name = nameof(GetSingleUserEF))]
    public User GetSingleUserEF(int idVal)
    {
        User? user =   _entityFramework.Users.Where(u => u.UserId == idVal).FirstOrDefault<User>();
       
        if (user == null)   {
            throw new Exception("Failed to Retrieve the User");
        }
        return user;
    }

    [HttpPut("/ef/{idVal:int}")]
    public IActionResult EditUserEF(UserDto userDto, int idVal)
    {
        User user = _entityFramework.Users.Single(u => u.UserId == idVal);
        user.FirstName = userDto.FirstName;
        user.LastName = userDto.LastName;
        user.Active = userDto.Active;
        user.Email = userDto.Email;
        user.Gender = userDto.Gender;
        bool result = _entityFramework.Users.Update(user).IsKeySet;
        if (result)
        {
            return Ok();
        }
        return BadRequest();
    }

    [HttpDelete("/ef/{userId}")]
    public IActionResult DeleteUserEF(int userId)
    {

        User user = _entityFramework.Users.Where(u => u.UserId == userId).FirstOrDefault<User>();

        if (user == null)
        {
            throw new Exception("Failed to Retrieve the User on Delete");
        }
        bool result = _entityFramework.Users.Remove(user).IsKeySet;
        if (result)
        {
            return Ok();
        }
        throw new Exception("Failed to Delete User");
    }

    [HttpPost("/ef")]
    public IActionResult AddUserEF(UserDto userDto)
    {
        User user = _mapper.Map<User>(userDto);

        bool result = _entityFramework.Users.Add(user).IsKeySet;

        if (result)
        {
            return Ok();
        }
        return BadRequest();

    }
}
