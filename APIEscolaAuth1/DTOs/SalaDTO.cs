using System.ComponentModel.DataAnnotations;

namespace APIEscolaAuth1.DTOs;

public class SalaDTO
{
    public int Id { get; set; }

    [Required]
    [StringLength(20)]
    public string? Numero { get; set; }
}
