using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Policy;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using GerenciadorDeClientes.Models;
using GerenciadorDeClientes.URL;
using Newtonsoft.Json;

namespace GerenciadorDeClientes.Controllers
{
    public class LoginController : Controller
    {
        Validacao url = new Validacao();
        private readonly HttpClient _client = new HttpClient();
        public string erro = "";

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> ValidaLogin()
        {
            string usuario = Request["usuario"];
            string senha = Request["senha"];

            try
            {
                using (var client = new HttpClient())
                {
                    var uri = new Uri(url.ValidaLogin + "usuario=" + usuario + "&senha=" + senha);

                    var parameters = new Dictionary<string, string>
                    {
                        { "usuario", usuario },
                        { "senha", senha }
                    };

                    var content = new FormUrlEncodedContent(parameters);

                    HttpResponseMessage response = await client.PostAsync(uri, content);

                    if (response.ReasonPhrase == "OK")
                    {
                        var result = "";
                        var resultado = JsonConvert.DeserializeObject<string>(result);

                        if (resultado == null)
                        {
                            Session["Autorizado"] = "OK";
                            return RedirectToAction("Index", "Cliente");
                        }
                        else
                        {
                            TempData["AcessoNegado"] = "Usuário ou senha inválidos";
                            return RedirectToAction("Index", "Home");
                        }
                    }
                    else if (response.ReasonPhrase == "Unauthorized")
                    {
                        TempData["AcessoNegado"] = "AcessoNegado";
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        TempData["FalhaDeServiço"] = "FalhaDeServiço";
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            catch (Exception ex)
            {
                erro = ex.Message;
                throw;
            }
        }
    }
}