using App.Models;
using App.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace App.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly UserService _userService;
    private readonly ILogger<UserController> _logger;

    public UserController(UserService userService, ILogger<UserController> logger)
    {
        _userService = userService;
        _logger = logger;

    }

    [HttpGet]
    public async Task<List<User>> Get() =>
        await _userService.GetAsync();

    [HttpDelete]
    public async Task Delete() =>
        await _userService.RemoveAsync();

    [HttpPost]
    [Route("search")]
    public async Task<List<UserDto>> Get([FromBody] string names)
    {
        var allUsers = await _userService.GetAsync();
        var lowerCaseNames = names.ToLower().Split(' '); // Names meaning it searches both first- and lastname

        return allUsers
            .Where(user => user.Name != null && user.Name.ToLower().Split(' ')
                        .Any(word => lowerCaseNames.Contains(word)))
            .Select(user => new UserDto
            {
                Name = user.Name,
                Username = user.Username,
                UserId = user.UserId,
                Followers = user.Followers,
                Following = user.Following
            })
            .ToList();
    }


    [HttpGet("{id}")]
    public async Task<ActionResult<UserDto>> Get(int id)
    {
        _logger.LogInformation("Request to user service for user ID: {UserId}", id);

        var user = await _userService.GetAsync(id);
        if (user is null)
        {
            _logger.LogWarning("User with ID: {UserId} not found.", id);
            return NotFound();
        }

        var userDto = new UserDto
        {  
            Name = user.Name,
            Username = user.Username,
            UserId = user.UserId,
            Followers = user.Followers,
            Following = user.Following
        };
        _logger.LogInformation("User with ID: {UserId} retrieved successfully.", id);
        return Ok(userDto);
    }

    [HttpPost]
    public async Task<IActionResult> Post(User newUser)
    {   
        newUser.Following.Add(newUser.UserId);
        var ifUser = await _userService.GetAsync(newUser.UserId);
        if (ifUser is not null)
        {
            return StatusCode(StatusCodes.Status409Conflict, "Duplicate entry in 'Users' list is not allowed.");
        }
        _logger.LogInformation("User : {UserId}", newUser.UserId);
        await _userService.CreateAsync(newUser);

        return CreatedAtAction(nameof(Get), new { id = newUser.Id }, newUser);
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] JsonPatchDocument<User> patchDoc)
    {
        if (patchDoc == null)
        {   
            _logger.LogInformation("Patch : {patchDoc}", patchDoc);
            return BadRequest("Patch document is null");
        }

        var user = await _userService.GetAsync(id);
        if (user == null)
        {
            return NotFound();
        }

        foreach (var operation in patchDoc.Operations)
        {
            if (operation.path.StartsWith("/Following/") && operation.OperationType == Microsoft.AspNetCore.JsonPatch.Operations.OperationType.Add)
            {
                int followingUserId;
                if (int.TryParse(operation.value.ToString(), out followingUserId))
                {
                    if (user.Following.Contains(followingUserId))
                    {
                        return StatusCode(StatusCodes.Status409Conflict, "Duplicate entry in 'Following' list is not allowed.");
                    }
                }
            }
        }


        patchDoc.ApplyTo(user);

        if (!TryValidateModel(user))
        {
            return BadRequest(ModelState);
        }

        await _userService.UpdateAsync(id, user);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var user = await _userService.GetAsync(id);

        if (user is null)
        {
            return NotFound();
        }

        await _userService.RemoveAsync(id);

        return NoContent();
    }
}