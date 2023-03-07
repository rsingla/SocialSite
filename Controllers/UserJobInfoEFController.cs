using Microsoft.AspNetCore.Mvc;
using SocialSite.Models;
using SocialSite.Data;
using AutoMapper;

namespace SocialSite.Controllers;

[ApiController]
[Route("[controller]")]
public class UserJobInfoEFController : ControllerBase
{
    private readonly ILogger<UserJobInfoEFController> _logger;
    private readonly DataContextEF _entityFramework;  

    IMapper _mapper;

    IConfigurationBuilder configBuilder = new ConfigurationBuilder();

    public UserJobInfoEFController(ILogger<UserJobInfoEFController> logger, IConfiguration configuration)
    {
        _logger = logger;
        _entityFramework = new DataContextEF(configuration);
        _mapper = new Mapper(new MapperConfiguration(cfg =>{
            cfg.CreateMap<UserDto, User>();
        }));
    }



    [HttpGet("/jobinfo")]
    public IEnumerable<UserJobInfo> GetUserJobInfoEF()
    {
        IEnumerable<UserJobInfo> userJobInfos = _entityFramework.UserJobInfo.ToList<UserJobInfo>();
        return userJobInfos;
    }

    [HttpGet("/jobinfo/{idVal}", Name = nameof(GetSingleUserJobInfoEF))]
    public UserJobInfo GetSingleUserJobInfoEF(int idVal)
    {
        UserJobInfo? userJobInfo =   _entityFramework.UserJobInfo.Where(u => u.UserId == idVal).FirstOrDefault<UserJobInfo>();
       
        if (userJobInfo == null)   {
            throw new Exception("Failed to Retrieve the User");
        }
        return userJobInfo;
    }

    [HttpPut("/jobinfo/{idVal:int}")]
    public IActionResult EditUserEF(UserJobInfo userJobInfo, int idVal)
    {
        UserJobInfo userJobInfoMap = _mapper.Map<UserJobInfo>(userJobInfo);

        bool result = _entityFramework.UserJobInfo.Update(userJobInfoMap).IsKeySet;
        if (result)
        {
            return Ok();
        }
        return BadRequest();
    }

    [HttpDelete("/jobinfo/{userId}")]
    public IActionResult DeleteUserEF(int userId)
    {

         UserJobInfo? userJobInfo =   _entityFramework.UserJobInfo.Where(u => u.UserId == userId).FirstOrDefault<UserJobInfo>();
       
        if (userJobInfo == null)
        {
            throw new Exception("Failed to Retrieve the User on Delete");
        }
        bool result = _entityFramework.UserJobInfo.Remove(userJobInfo).IsKeySet;
        if (result)
        {
            return Ok();
        }
        throw new Exception("Failed to Delete User");
    }

    [HttpPost("/jobinfo")]
    public IActionResult AddUserEF(UserJobInfo userJobInfo)
    {
        UserJobInfo userJobInfoMapper = _mapper.Map<UserJobInfo>(userJobInfo);

        bool result = _entityFramework.UserJobInfo.Add(userJobInfoMapper).IsKeySet;

        if (result)
        {
            return Ok();
        }
        return BadRequest();

    }
}
