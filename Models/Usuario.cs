using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutEFCookie.Models
{
    public class Usuario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MyProperty { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 4)]
        public string Nome { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 4)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 4)]
        [DataType(DataType.Password)]
        public string Senha { get; set; }

        public ICollection<UsuarioPermissao> UsuarioPermissao { get; set; }

    }
}