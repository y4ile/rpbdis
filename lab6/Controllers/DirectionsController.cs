/*
 *       @Author: yaile
 */

using lab6.Data;
using lab6.Models.Dto;
using lab6.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace lab6.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DirectionsController : ControllerBase
{
    private readonly PlansDbContext _context;

    public DirectionsController(PlansDbContext context)
    {
        _context = context;
    }
    
    [HttpGet]
    public IActionResult GetAllDirections()
    {
        var directions = _context.DevelopmentDirections.ToList();
        return Ok(directions);
    }
    
    [HttpGet]
    [Route("{id:int}")]
    public IActionResult GetDirectionById(int id)
    {
        var direction = _context.DevelopmentDirections.Find(id);
        return Ok(direction);
    }

    [HttpPost]
    public IActionResult AddDirection(AddDirectionDto addDirectionDto)
    {
        var direction = new DevelopmentDirection()
        {
            DirectionName = addDirectionDto.DirectionName,
        };
        _context.DevelopmentDirections.Add(direction);
        _context.SaveChanges();
        
        return Ok(direction);
    }

    [HttpPut]
    [Route("{id:int}")]
    public IActionResult UpdateDirection(int id, UpdateDirectionDto updateDirectionDto)
    {
        var direction = _context.DevelopmentDirections.Find(id);
        if (direction == null)
            return NotFound();
        
        direction.DirectionName = updateDirectionDto.DirectionName;
        _context.SaveChanges();
        
        return Ok(direction);
    }

    [HttpDelete]
    [Route("{id:int}")]
    public IActionResult DeleteDirection(int id)
    {
        var direction = _context.DevelopmentDirections.Find(id);
        if (direction == null)
            return NotFound();
        
        _context.DevelopmentDirections.Remove(direction);
        _context.SaveChanges();
        return Ok();
    }
}