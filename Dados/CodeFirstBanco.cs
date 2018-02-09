using System.Linq;
using AutEFCookie.Models;

namespace AutEFCookie.Dados
{
    public class CodeFirstBanco
    {
        public static void Inicializar(AutenticacaoContext contexto)
        {
            contexto.Database.EnsureCreated();

            //importa para não gravar sempre a mesma a coisa no banco
            if (contexto.Usuarios.Any())
            {
                return;
            }

            var usuario = new Usuario()
            {
                Nome = "Gabriela",
                Email = "gabigiraud@gmail.com",
                Senha = "123456"
            };
            contexto.Usuarios.Add(usuario);

            var permissao = new Permissao()
            {
                Nome = "Controladoria"
            };
            contexto.Permissao.Add(permissao);

            var usuariopermissao = new UsuarioPermissao()
            {

                IdPermissao = permissao.IdPermissao,
                IdUsuario = usuario.IdUsuario,
            };
            contexto.UsuariosPermissoes.Add(usuariopermissao);

            contexto.SaveChanges();
        }
    }
}