using System.ComponentModel.DataAnnotations;

namespace GerenciadorCursos.API.ViewModels
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "O email é obrigatório")]
        [EmailAddress(ErrorMessage = "Formato de email inválido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória")]
        [StringLength(20, ErrorMessage = "A {0} deve ter no mínimo {2} e no máximo {1} caracteres",
            MinimumLength = 10)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "A confirmação da senha é obrigatória")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "A senha e a confirmação não coincidem")]
        public string ConfirmPassword { get; set; }
    }
}
