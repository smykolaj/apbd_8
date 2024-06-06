using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Repositories;

public class ClientsRepository(ApbdContext context) : IClientsRepository
{
    public async Task<bool> ClientExists(int clientId)
    {
        return await context.Clients.AnyAsync(c => c.IdClient.Equals(clientId));
    }

    public async Task<bool> ClientDoesntHaveTrips(int clientId)
    {
        return ! await context.ClientTrips.AnyAsync(ct => ct.IdClient.Equals(clientId));
    }

    public async Task<bool> DeleteClient(int clientId)
    {
        if (! await ClientExists(clientId))
            throw new Exception("Client doesn't exist!");
        if (! await ClientDoesntHaveTrips(clientId))
            throw new Exception("Client has trips associated with them!");
        var clientToRemove = new Client()
        {
            IdClient = clientId
        };
        context.Attach(clientToRemove);
        var entry = context.Entry(clientToRemove);
        entry.State = EntityState.Deleted;
        await context.SaveChangesAsync();
        return true;

    }
    
    public async Task<bool> ClientWithPeselExists(string pesel)
    {
        return await context.Clients.AnyAsync(c => c.Pesel.Equals(pesel));
    }
}