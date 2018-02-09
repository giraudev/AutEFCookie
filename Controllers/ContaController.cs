using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using AutEFCookie.Dados;
using AutEFCookie.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AutEFCookie.Controllers
{
    public class ContaController : Controller
    {
        readonly AutenticacaoContext _contexto;

        public ContaController(AutenticacaoContext contexto)
        {
            _contexto = contexto;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(Usuario usuario)
        {
            try
            {
                //Include("UsuarioPermissao") é exatamente o nome do ICollection que esta dentro de Usuario
                //Include("UsuarioPermissao.Permissao") informa as permissões dele - nomes
                Usuario user = _contexto.Usuarios.Include("UsuarioPermissao").Include("UsuarioPermissao.Permissao").FirstOrDefault(c => c.Email == usuario.Email && c.Senha == usuario.Senha);

                if (user != null)
                {
                    //efetua o login e senha, fica armazenado no claim, assim nao precisa ficar logando em todas as páginas
                    var claims = new List<Claim>();

                    //"armazena" os dados de login para autenticar as paginas toda hora, mas isso depois de um tempo, expira
                    claims.Add(new Claim(ClaimTypes.Email, user.Email));
                    claims.Add(new Claim(ClaimTypes.Name, user.Nome));
                    claims.Add(new Claim(ClaimTypes.Sid, user.IdUsuario.ToString()));

                    foreach (var item in user.UsuarioPermissao)
                    {
                        //ele vai rodar o foreach e add ao claim as permissoes correspondentes ao usuario
                        claims.Add(new Claim(ClaimTypes.Role, item.Permissao.Nome));
                    }
                    
                    //add o objto claims e seu cookiautentication
                    var claimsIdentity = new ClaimsIdentity(
                        claims, CookieAuthenticationDefaults.AuthenticationScheme
                    );

                    //logando no usuario
                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                    //redireciona a página Index, da view Financeiro
                    return RedirectToAction("Index", "Financeiro");
                }
                return View(usuario);
            }
            catch (System.Exception)
            {

                return View(usuario);
            }
        }

        [HttpGet]
        public IActionResult Sair()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Login");
        }
    }
}