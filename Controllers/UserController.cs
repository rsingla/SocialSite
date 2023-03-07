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

    

    [HttpGet("")]
    public IEnumerable<User> GetUsers()
    {
        string sqlSelect = "SELECT * FROM TutorialAppSchema.Users";

        IEnumerable<User> users = _dataContextDapper.LoadData<User>(sqlSelect).ToList();

        return Enumerable.Range(0, 5).Select(index => new User
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
    public User GetSingleUser(int idVal)
    {
        string sqlSelect = "SELECT * FROM TutorialAppSchema.Users WHERE UserId =" + idVal;
        User user = new User();
        try
        {
            user = _dataContextDapper.LoadDataSingle<User>(sqlSelect);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            user.error = new Error("User not found");
            return user;
        }
        return user;
    }

    [HttpPut("{idVal:int}")]
    public IActionResult EditUser(UserDto user, int idVal) {
        string sqlUpdate = "UPDATE TutorialAppSchema.Users SET FirstName = '"+ user.FirstName +"', LastName = '"+ user.LastName+ 
        "' WHERE UserId = " + idVal;
        Console.WriteLine(sqlUpdate);
        bool result = _dataContextDapper.ExecuteSQL(sqlUpdate);
        if (result) {
            return Ok();
        }
        return BadRequest();
    }

    [HttpDelete("{idVal:int}")]
    public IActionResult DeleteUser(int idVal) {
        string sqlDelete = "DELETE FROM TutorialAppSchema.Users WHERE UserId = " + idVal;
        bool result = _dataContextDapper.ExecuteSQL(sqlDelete);
        if (result) {
            return Ok();
        }
        return BadRequest();
    }

    [HttpPost("")]
    public IActionResult AddUser(UserDto user) {
        string sqlInsert = "INSERT INTO TutorialAppSchema.Users (FirstName, LastName, Email, Gender, Active) values ("
        +"'"+ user.FirstName + "', '"
        + user.LastName + "', '"
        + user.Email + "', '"
        + user.Gender + "', '"
        + user.Active + "')";

        Console.WriteLine(sqlInsert);

        bool result = _dataContextDapper.ExecuteSQL(sqlInsert);
        if (result) {
            return Ok();
        }
        return BadRequest();

    }
}
