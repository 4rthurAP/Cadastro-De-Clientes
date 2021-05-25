using System.ComponentModel.DataAnnotations;

namespace CadastroDeClientes.ViewModel
{
    public class Cadastro
    {
        [Display(Name = "Razão Social", Description = "Razão Social")]
        [StringLength(60, MinimumLength = 3, ErrorMessage = "O campo nome deve ter de 3 a 60 dígitos")]
        [Required(ErrorMessage = "Preencha este campo")]
        public string Razao_Social { get; set; }


        [Display(Name = "Nome Fantasia", Description = "Nome Fantasia")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "O campo nome deve ter de 3 a 30 dígitos")]
        [Required(ErrorMessage = "Preencha este campo")]
        public string Nome_Fantasia { get; set; }

        [Display(Name = "Telefone", Description = "Telefone")]
        [StringLength(15, MinimumLength = 5, ErrorMessage = "O número de telefone deve ter de 5 a 11 digitos")]
        [Required(ErrorMessage = "Preencha este campo")]
        public string Telefone { get; set; }

        [Display(Name = "CNPJ", Description = "Digite seu CNPJ")]
        [StringLength(26, MinimumLength = 13, ErrorMessage = "Digite um número de CNPJ válido.")]
        [Required(ErrorMessage = "Preencha este campo")]
        public string CNPJ { get; set; }

        [Display(Name = "E-mail", Description = "Email")]
        [EmailAddress(ErrorMessage = "Precisa digitar um e-mail valido")]
        [StringLength(60, MinimumLength = 5, ErrorMessage = "O e-mail deve de 5 a 60 dígitos")]
        [Required(ErrorMessage = "Preencha este campo")]
        public string Email { get; set; }

        [Display(Name = "Senha", Description = "Senha")]
        [DataType(DataType.Password)]
        [StringLength(20, MinimumLength = 8, ErrorMessage = "A senha deve ter de 8 a 20 dígitos")]
        [Required(ErrorMessage = "Preencha este campo")]
        public string Senha { get; set; }

        [Display(Name = "Confirmar senha", Description = "Confirme sua senha")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Preencha este campo")]
        public string ConfirmarSenha { get; set; }

        [Display(Name = "Nivel de acesso", Description = "Nivel de acesso")]
        [Range(-1,2, ErrorMessage ="0 Para usuario e 1 para Admin")]
        [Required(ErrorMessage = "Preencha este campo")]
        public int Nivel_De_Acesso { get; set; }


        public int Id_Origem { get; set; }
        public int Editar { get; set; }

        public int Adicionar { get; set; }

        public int Deletar { get; set; }
    }
}
