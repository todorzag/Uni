using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniBackend.Controllers.ApiEndpoints;
using UniBackend.Controllers.Dtos;
using UniBackend.Models;

[ApiController]
[Authorize]
public class OrdersController : ControllerBase
{
    private readonly AppDbContext _db;

    public OrdersController(AppDbContext db)
    {
        _db = db;
    }

    [HttpGet(ApiEndpoints.Orders.GetMyOrders)]
    public async Task<IActionResult> GetMyOrders()
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userIdClaim))
        {
            return BadRequest("User identifier claim is missing.");
        }

        var userId = int.Parse(userIdClaim);

        var orders = await _db.Order
            .Where(o => o.UserId == userId)
            .Include(o => o.Items)
            .ToListAsync();

        return Ok(orders);
    }

    [HttpPost(ApiEndpoints.Orders.CreateOrder)]
    public async Task<IActionResult> CreateOrder([FromBody] CreateOrderDto request)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userIdClaim))
        {
            return BadRequest("User identifier claim is missing.");
        }

        var userId = int.Parse(userIdClaim);

        var items = request.Items.Select(i => new OrderItemModel
        {
            ComputerId = i.ComputerId,
            Quantity = i.Quantity,
            Price = _db.Computer.First(c => c.Id == i.ComputerId).Price
        }).ToList();

        var total = items.Sum(item => item.Price * item.Quantity);

        var order = new OrderModel
        {
            UserId = userId,
            OrderAddress = request.OrderAddress,
            CreatedAt = DateTime.UtcNow,
            Items = items,
            Total = total
        };

        _db.Order.Add(order);
        await _db.SaveChangesAsync();

        return Ok(order);
    }
}
