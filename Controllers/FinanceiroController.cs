using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AutEFCookie.Controllers
{
    //[Authorize] - para entrar na página, precisa de autenticação
    //colocando Roles="Financeiro",só autoriza o acesso a quem tem esta permissão
    [Authorize(Roles="Financeiro")]
    public class FinanceiroController:Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }


}