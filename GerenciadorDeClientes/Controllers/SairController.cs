using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GerenciadorDeClientes.Controllers
{
    public class SairController : Controller
    {
        public void Index()
        {
            Session.Abandon();
            Response.Redirect("Home/Index");

            // Destruindo seções
            Session.Remove("Autorizado");
        }
    }
}