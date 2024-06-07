using System.ComponentModel.DataAnnotations;

namespace APIEscolaAuth1.DTOs;

public class AlunoDTO
{
    public int Id { get; set; }

    [Required]
    [StringLength(50)]
    public string? Nome { get; set; }

    [Required]
    [StringLength(12)]
    public string? Identidade { get; set; }

    [Required]
    [DataType(DataType.Date)]
    public DateTime? Nascimento { get; set; }

    public int? TurmaId { get; set; }
}
