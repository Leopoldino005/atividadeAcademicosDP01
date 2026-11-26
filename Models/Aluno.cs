using System.ComponentModel.DataAnnotations;

namespace ProjetoAcademico.Models;

public class Aluno
{
    public int AlunoID { get; set; }

    [Required(ErrorMessage = "Informe o nome do aluno.")]
    [StringLength(120)]
    [Display(Name = "Nome")]
    public string Nome { get; set; } = string.Empty;

    [Required(ErrorMessage = "Informe o e-mail.")]
    [EmailAddress(ErrorMessage = "Informe um e-mail válido.")]
    [StringLength(160)]
    [Display(Name = "E-mail")]
    public string Email { get; set; } = string.Empty;

    [StringLength(20)]
    [Display(Name = "Telefone")]
    public string? Telefone { get; set; }

    [Required(ErrorMessage = "Informe o endereço.")]
    [StringLength(180)]
    [Display(Name = "Endereço")]
    public string Endereco { get; set; } = string.Empty;

    [StringLength(80)]
    [Display(Name = "Complemento")]
    public string? Complemento { get; set; }

    [StringLength(80)]
    [Display(Name = "Bairro")]
    public string? Bairro { get; set; }

    [StringLength(100)]
    [Display(Name = "Município")]
    public string? Municipio { get; set; }

    [StringLength(2)]
    [Display(Name = "UF")]
    public string? Uf { get; set; }

    [StringLength(9)]
    [Display(Name = "CEP")]
    public string? Cep { get; set; }
}
