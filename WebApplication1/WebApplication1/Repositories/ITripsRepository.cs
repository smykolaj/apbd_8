using WebApplication1.Models;

namespace WebApplication1.Repositories;

public interface ITripsRepository
{
    Task<List<TripDTO>> GetTrips(int page, int pageSize);

    Task<int> GetTripsAmount();
    Task<ClientTripDTO> AssignClientToATrip(int tripId, PostAddClientToTripDTO data);
    Task<bool> ClientWithPeselAlreadyOnTrip(string pesel, int tripId);
    Task<bool> TripExists(int tripId);
    Task<bool> TripIsInFuture(int tripId);
    
}