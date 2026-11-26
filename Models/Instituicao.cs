using System.ComponentModel.DataAnnotations;

namespace ProjetoAcademico.Models;

public class Instituicao
{
    public int InstituicaoID { get; set; }

    [Required(ErrorMessage = "Informe o nome da instituição.")]
    [StringLength(150)]
    [Display(Name = "Nome")]
    public string Nome { get; set; } = string.Empty;

    [Required(ErrorMessage = "Informe o endereço da instituição.")]
    [StringLength(200)]
    [Display(Name = "Endereço")]
    public string Endereco { get; set; } = string.Empty;
}
