namespace WebApplication1.Models;

public class TripDTO
{
    public int IdTrip { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public DateTime DateFrom { get; set; }

    public DateTime DateTo { get; set; }

    public int MaxPeople { get; set; }

    public virtual IEnumerable<ClientDTO> Clients { get; set; } = new List<ClientDTO>();

    public virtual IEnumerable<CountryDTO> Countries { get; set; } = new List<CountryDTO>();

}