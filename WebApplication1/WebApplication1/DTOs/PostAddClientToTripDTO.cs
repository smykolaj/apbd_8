using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models;

public class PostAddClientToTripDTO
{
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
    [Required]
    public string Email { get; set; }
    [Required]
    public string Telephone { get; set; }
    [Required]
    public string Pesel { get; set; }
    [Required]
    public int IdTrip { get; set; }
    [Required]
    public string TripName { get; set; }
    public DateTime? PaymentDate { get; set; }
}