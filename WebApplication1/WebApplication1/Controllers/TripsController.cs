using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Repositories;

namespace WebApplication1.Controllers;

[Microsoft.AspNetCore.Components.Route("/api/trips")]
[ApiController]
public class TripsController : ControllerBase

{
    private readonly IClientsRepository _clientsRepository;
    private readonly ITripsRepository _tripsRepository;

    public TripsController(IClientsRepository clientsRepository, ITripsRepository tripsRepository)
    {
        _clientsRepository = clientsRepository;
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

    [HttpDelete("{clientId}")]
    public async Task<IActionResult> DeleteClientById(int clientId)
    {
        try
        {
           await _clientsRepository.DeleteClient(clientId);

        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }

        return Ok();

    }

}