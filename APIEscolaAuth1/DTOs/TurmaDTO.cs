using System.ComponentModel.DataAnnotations;

namespace APIEscolaAuth1.DTOs;

public class TurmaDTO
{
    public int Id { get; set; }

    [Required]
    [StringLength(12)]
    public string? Ano { get; set; }

    [Required]
    [StringLength(1)]
    public string? Sequencia { get; set; }

    [Required]
    public int SalaId { get; set; }
}
