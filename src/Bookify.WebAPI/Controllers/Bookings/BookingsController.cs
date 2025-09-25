using Bookify.Application.Bookings.GetBooking;
using Bookify.Application.Bookings.ReserveBooking;

using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Bookify.WebAPI.Controllers.Bookings;

[ApiController]
[Route("api/bookings")]
public class BookingsController : ControllerBase
{
    private readonly ISender _sender;

    public BookingsController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetBooking(Guid id, CancellationToken cancellationToken)
    {
        var bookingQuery = new GetBookingQuery(id);
        var result = await _sender.Send(bookingQuery, cancellationToken);
        
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }

    [HttpPost]
    public async Task<IActionResult> ReserveBooking(ReserveBookingRequest request, CancellationToken cancellationToken)
    {
        var reverseBookingCommand = new ReserveBookingCommand(
            request.ApartmentId,
            request.UserId,
            request.StartDate,
            request.EndDate);
        
        var result = await _sender.Send(reverseBookingCommand, cancellationToken);
        if (result.IsFailure)
            return BadRequest(result.Error);
        
        return CreatedAtAction(nameof(GetBooking), new { id = result.Value }, result.Value);
    }
}