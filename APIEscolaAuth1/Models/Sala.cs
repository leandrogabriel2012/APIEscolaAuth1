using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace APIEscolaAuth1.Models;

[Table("sala")]
public class Sala
{
    public int Id { get; set; }

    [Required(ErrorMessage = "É obrigatório inclusão de número da sala")]
    [StringLength(20, ErrorMessage = "O número da sala deve ter no máximo {1} caracteres")]
    public string? Numero { get; set; }

    public ICollection<Turma>? Turmas { get; set; }
}