using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniBackend.Controllers.ApiEndpoints;
using UniBackend.Controllers.Dtos;
using UniBackend.Models;

[ApiController]
[Authorize]
public class ComputersController : ControllerBase
{
    private readonly AppDbContext _db;

    public ComputersController(AppDbContext db)
    {
        _db = db;
    }

    [HttpGet(ApiEndpoints.Computers.GetAll)]
    public async Task<IActionResult> GetAllComputers()
    {
        List<ComputerModel> computers = await _db.Computer.ToListAsync();

        return Ok(computers);
    }

    [HttpGet(ApiEndpoints.Computers.GetById)]
    public async Task<IActionResult> GetComputerById(int id)
    {
        ComputerModel? computer = await _db.Computer.FindAsync(id);
        if (computer == null)
            return NotFound();

        return Ok(computer);
    }

    [HttpPost(ApiEndpoints.Computers.Filter)]
    public async Task<IActionResult> FilterComputers([FromBody] FilterComputersDto filter)
    {
        if (filter == null)
            return BadRequest("Request body is required.");

        var query = _db.Computer.AsQueryable();

        if (!string.IsNullOrWhiteSpace(filter.Name))
        {
            var nameLower = filter.Name.ToLower();
            query = query.Where(c => c.Name.ToLower().Contains(nameLower));
        }

        if (filter.MinPrice.HasValue && filter.MinPrice.Value < 0)
            return BadRequest("minPrice cannot be negative.");
        else if (filter.MaxPrice.HasValue && filter.MaxPrice.Value < 0)
            return BadRequest("maxPrice cannot be negative.");
        else if (filter.MinPrice.HasValue && filter.MaxPrice.HasValue && filter.MinPrice > filter.MaxPrice)
            return BadRequest("minPrice cannot be greater than maxPrice.");

        if (filter.MinPrice.HasValue && filter.MaxPrice.HasValue)
            query = query.Where(c => c.Price >= filter.MinPrice.Value && c.Price <= filter.MaxPrice);
        else if (filter.MinPrice.HasValue)
            query = query.Where(c => c.Price >= filter.MinPrice.Value);
        else if (filter.MaxPrice.HasValue)
            query = query.Where(c => c.Price <= filter.MaxPrice.Value);

        List<ComputerModel> computers = await query.ToListAsync();

        return Ok(computers);
    }
}
