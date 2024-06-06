using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Repositories;

public class TripsRepository : ITripsRepository
{

    private readonly ApbdContext _context;
    private readonly IClientsRepository _clientsRepository;

    public TripsRepository(ApbdContext context, IClientsRepository clientsRepository)
    {
        _context = context;
        _clientsRepository = clientsRepository;
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

    public async Task<ClientTripDTO> AssignClientToATrip(int tripId, PostAddClientToTripDTO data)
    {
        if (await _clientsRepository.ClientWithPeselExists(data.Pesel))
            throw new Exception("Client with this pesel already exists!");
        if( await ClientWithPeselAlreadyOnTrip(data.Pesel, tripId))
            throw new Exception("Client with this pesel is already on the trip!");
        if(!await TripExists(tripId))
            throw new Exception("Trip with such id doesnt exist");
        if (!await TripIsInFuture(tripId))
            throw new Exception("The assignment for the trip has already finished!");
        var client = new Client()
        {
            FirstName = data.FirstName,
            LastName = data.LastName,
            Email = data.Email,
            Telephone = data.Telephone,
            Pesel = data.Pesel
        };
        await _context.Clients.AddAsync(client);
        await _context.SaveChangesAsync();

        var clientTrip = new ClientTrip()
        {
            IdClient = client.IdClient,
            IdTrip = tripId,
            RegisteredAt = DateTime.Now,
            PaymentDate = data.PaymentDate
        };
        await _context.ClientTrips.AddAsync(clientTrip);
        await _context.SaveChangesAsync();
        var clientTripDto = new ClientTripDTO()
        {
            IdClient = clientTrip.IdClient,
            IdTrip = clientTrip.IdTrip,
            PaymentDate = clientTrip.PaymentDate,
            RegisteredAt = clientTrip.RegisteredAt
        };
        
        return clientTripDto;






    }



    public async Task<bool> ClientWithPeselAlreadyOnTrip(string pesel, int tripId)
    {

        return await _context.Clients
            .Join(_context.ClientTrips,
                client => client.IdClient,
                clientTrip => clientTrip.IdClient,
                (client, clientTrip) => new { client, clientTrip })
            .AnyAsync(ct => ct.client.Pesel.Equals(pesel) && ct.clientTrip.IdTrip.Equals(tripId) );

    }

    public async Task<bool> TripExists(int tripId)
    {
        return await _context.Trips.AnyAsync(t => t.IdTrip.Equals(tripId));
    }

    public Task<bool> TripIsInFuture(int tripId)
    {
        return _context.Trips.AnyAsync(t => t.IdTrip.Equals(tripId) && t.DateFrom > DateTime.Now);
        
    }
}