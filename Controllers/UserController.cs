using Microsoft.AspNetCore.Mvc;
using SocialSite.Models;
using SocialSite.Data;

namespace SocialSite.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly ILogger<UserController> _logger;
    private readonly DataContextDapper _dataContextDapper;

    IConfigurationBuilder configBuilder = new ConfigurationBuilder();

    public UserController(ILogger<UserController> logger, IConfiguration configuration)
    {
        _logger = logger;
        _dataContextDapper = new DataContextDapper(configuration);
    }

    

    [HttpGet(Name = nameof(GetUsers))]
    public IEnumerable<Users> GetUsers()
    {
        string sqlSelect = "SELECT * FROM TutorialAppSchema.Users";

        IEnumerable<Users> users = _dataContextDapper.LoadData<Users>(sqlSelect).ToList();

        return Enumerable.Range(0, 5).Select(index => new Users
        {
            UserId = users.ElementAt(index).UserId,
            FirstName = users.ElementAt(index).FirstName,
            LastName = users.ElementAt(index).LastName,
            Email = users.ElementAt(index).Email,
            Gender = users.ElementAt(index).Gender,
            Active = users.ElementAt(index).Active,
        })
        .ToArray();
    }

    [HttpGet("{idVal:int}", Name = nameof(GetSingleUser))]
    public Users GetSingleUser(int idVal)
    {
        string sqlSelect = "SELECT * FROM TutorialAppSchema.Users WHERE UserId =" + idVal;
        Users user = new Users();
        try
        {
            user = _dataContextDapper.LoadDataSingle<Users>(sqlSelect);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            user.error = new Error("User not found");
            return user;
        }


        return user;


    }
}
