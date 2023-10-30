using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace GerenciadorDeClientes.Controllers
{
    public class HomeController : Controller
    {
        [OutputCache(NoStore = true, Duration = 0)]
        public async Task<ActionResult> Index()
        {
            return View();
        }
    }
}