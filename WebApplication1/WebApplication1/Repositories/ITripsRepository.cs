using WebApplication1.Models;

namespace WebApplication1.Repositories;

public interface ITripsRepository
{
    Task<List<TripDTO>> GetTrips(int page, int pageSize);

    Task<int> GetTripsAmount();
}