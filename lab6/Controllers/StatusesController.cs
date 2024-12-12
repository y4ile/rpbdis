/*
 *       @Author: yaile
 */

using lab6.Data;
using lab6.Models.Dto;
using lab6.Models.Dto.Statuses;
using lab6.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace lab6.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StatusesController : ControllerBase
{
    private readonly PlansDbContext _context;

    public StatusesController(PlansDbContext context)
    {
        _context = context;
    }
    
    [HttpGet]
    public IActionResult GetAllStatuses()
    {
        var statuses = _context.Statuses.ToList();
        return Ok(statuses);
    }
    
    [HttpGet]
    [Route("{id:int}")]
    public IActionResult GetStatusById(int id)
    {
        var status = _context.Statuses.Find(id);
        return Ok(status);
    }

    [HttpPost]
    public IActionResult AddStatus(AddStatusDto addStatusDto)
    {
        var status = new Status()
        {
            StatusName = addStatusDto.StatusName,
        };
        _context.Statuses.Add(status);
        _context.SaveChanges();
        
        return Ok(status);
    }

    [HttpPut]
    [Route("{id:int}")]
    public IActionResult UpdateStatus(int id, UpdateStatusDto updateStatusDto)
    {
        var status = _context.Statuses.Find(id);
        if (status == null)
            return NotFound();
        
        status.StatusName = updateStatusDto.StatusName;
        _context.SaveChanges();
        
        return Ok(status);
    }

    [HttpDelete]
    [Route("{id:int}")]
    public IActionResult DeleteStatus(int id)
    {
        var status = _context.Statuses.Find(id);
        if (status == null)
            return NotFound();
        
        _context.Statuses.Remove(status);
        _context.SaveChanges();
        return Ok();
    }
}