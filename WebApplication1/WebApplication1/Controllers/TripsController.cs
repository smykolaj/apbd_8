using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Repositories;

namespace WebApplication1.Controllers;

[Microsoft.AspNetCore.Components.Route("/api/trips")]
[ApiController]
public class TripsController : ControllerBase

{
    private readonly ITripsRepository _tripsRepository;

    public TripsController( ITripsRepository tripsRepository)
    {
        _tripsRepository = tripsRepository;
    }


    [HttpGet("{page:int}/{pageSize:int}")]
    public async Task<IActionResult> GetAllTrips(int page, int pageSize)
    {
        var amount = await _tripsRepository.GetTripsAmount();
       
        var output = new TripsWithPageDTO
        {
            trips = await _tripsRepository.GetTrips(page, pageSize), 
            allPages = (int) Math.Ceiling((double)amount/pageSize),
            pageNum = page,
            pageSize = pageSize
        };
        return Ok(output);
    }

    

}