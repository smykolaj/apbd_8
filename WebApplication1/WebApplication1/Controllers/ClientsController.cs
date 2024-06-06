using Microsoft.AspNetCore.Mvc;
using WebApplication1.Repositories;

namespace WebApplication1.Controllers;

[Microsoft.AspNetCore.Components.Route("/api/clients")]
[ApiController]
public class ClientsController : ControllerBase
{
   
    private readonly IClientsRepository _clientsRepository;

    public ClientsController(IClientsRepository clientsRepository)
    {
        _clientsRepository = clientsRepository;
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