using System.ComponentModel.DataAnnotations;

namespace Domain.Request;

public record struct ClientRequest
{
    [Required(ErrorMessage = "O nome não pode ser vazio")]
    public string Name { get; set; }

    [Required(ErrorMessage = "O Email não pode ser vazio")]
    [EmailAddress(ErrorMessage = "Email em formato inválido")]
    public string Email { get; set; }

    [Required(ErrorMessage = "O CPF não pode ser vazio")]
    [RegularExpression(@"^\d{3}\.\d{3}\.\d{3}-\d{2}$", ErrorMessage = "Formato de CPF inválido")]
    public string CPF { get; set; }
    public AddressRequest AddressRequest { get; set; }
}