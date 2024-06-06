using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Repositories;

public class TripsRepository : ITripsRepository
{

    private readonly ApbdContext _context;

    public TripsRepository(ApbdContext context)
    {
        _context = context;
    }

    public async Task<List<TripDTO>> GetTrips(int page, int pageSize)
    {
        var trips = await _context.Trips.Select(e => new TripDTO()
        {
            Name = e.Name,
            Description = e.Description,
            DateFrom = e.DateFrom,
            DateTo = e.DateTo,
            MaxPeople = e.MaxPeople,
            Countries = e.IdCountries.Select(co => new CountryDTO()
            {
                Name = co.Name
            }),
            Clients = e.ClientTrips.Select(cl => new ClientDTO()
            {
                FirstName = cl.IdClientNavigation.FirstName,
                LastName = cl.IdClientNavigation.LastName
            })
        }).OrderBy(e => e.DateFrom)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return trips;

    }

    public async Task<int> GetTripsAmount()
    {
        var amount = await _context.Trips.CountAsync();
        return amount;
    }

    public Task<bool> AssignClientToATrip(int tripId, PostAddClientToTripDTO data)
    {
        throw new NotImplementedException();
    }

   

    public Task<bool> ClientWithPeselAlreadyOnTrip(string pesel, int tripId)
    {
        throw new NotImplementedException();

        // return _context
        //     .ClientTrips
        //     .Where(ct => ct.IdTrip.Equals(tripId))
        //     .Join(_context.Clients,
        //         client => client.IdClient,
        //         trip => trip.IdClient
        //         ).AnyAsync()
        //     
    }

    public Task<bool> TripExists(int tripId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> TripIsInFuture(int tripId)
    {
        throw new NotImplementedException();
    }
}