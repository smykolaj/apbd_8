using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Repositories;

namespace WebApplication1.Controllers;

[Microsoft.AspNetCore.Components.Route("/api/trips")]
[ApiController]
public class TripsController(ITripsRepository repository) : ControllerBase
{
    [HttpGet("{page:int}/{pageSize:int}")]
    public async Task<IActionResult> GetAllTrips(int page, int pageSize)
    {
        var amount = await repository.GetTripsAmount();
       
        var output = new TripsWithPageDTO
        {
            trips = await repository.GetTrips(page, pageSize), 
            allPages = (int) Math.Ceiling((double)amount/pageSize),
            pageNum = page,
            pageSize = pageSize
        };
        return Ok(output);
    }

}