/*
 *       @Author: yaile
 */

using lab6.Data;
using lab6.Models.Dto.Statuses;
using lab6.Models.Dto.Users;
using lab6.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace lab6.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly PlansDbContext _context;

    public UsersController(PlansDbContext context)
    {
        _context = context;
    }
    
    [HttpGet]
    public IActionResult GetAllUsers()
    {
        var users = _context.Users.ToList();
        return Ok(users);
    }
    
    [HttpGet]
    [Route("{id:int}")]
    public IActionResult GetUserById(int id)
    {
        var user = _context.Users.Find(id);
        return Ok(user);
    }

    [HttpPost]
    public IActionResult AddUser(AddUserDto addUserDto)
    {
        var user = new User()
        {
            Login = addUserDto.Login,
            Password = addUserDto.Password,
            Email = addUserDto.Email
        };
        _context.Users.Add(user);
        _context.SaveChanges();
        
        return Ok(user);
    }

    [HttpPut]
    [Route("{id:int}")]
    public IActionResult UpdateUser(int id, UpdateUserDto updateUserDto)
    {
        var user = _context.Users.Find(id);
        if (user == null)
            return NotFound();
        
        user.Login = updateUserDto.Login;
        user.Email = updateUserDto.Email;
        _context.SaveChanges();
        
        return Ok(user);
    }

    [HttpDelete]
    [Route("{id:int}")]
    public IActionResult DeleteUser(int id)
    {
        var user = _context.Users.Find(id);
        if (user == null)
            return NotFound();
        
        _context.Users.Remove(user);
        _context.SaveChanges();
        return Ok();
    }
}