namespace WebApplication1.Repositories;

public interface IClientsRepository
{
    Task<bool> ClientExists(int clientId);
    Task<bool> ClientDoesntHaveTrips(int clientId);
    Task<bool> DeleteClient(int clientId);
}