using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutEFCookie.Models
{
    public class UsuarioPermissao
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdUsuarioPermissao { get; set; }

        [Required]
        public int IdUsuario { get; set; }

        //ForeignKey força o programa a não gerar nova coluna (UsuarioId)
        [ForeignKey("IdUsuario")]
        public Usuario Usuario { get; set; }

        [Required]
        public int IdPermissao { get; set; }

        [ForeignKey("IdPermissao")]
        public Permissao Permissao { get; set; }
    }
}