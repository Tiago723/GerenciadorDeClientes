using GerenciadorDeClientes.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using GerenciadorDeClientes.URL;
using System.Collections;
using System.Data.SqlClient;
using Microsoft.Ajax.Utilities;
using System.Text;
using System.Web.UI;
using System.Security.Cryptography;
using iTextSharp.text.pdf.security;
using iTextSharp.text.pdf;
using PagedList;

namespace GerenciadorDeClientes.Controllers
{
    public class ClienteController : Controller
    {
        MetodosApi Api = new MetodosApi();
        private readonly HttpClient _client = new HttpClient();
        public string erro = "ERRO NO PROCESSAMENTO";       

        [OutputCache(NoStore = true, Duration = 0)]
        public async Task<ActionResult> Index()
        {
            var lista = await ListaClientes();

            if (Session["Autorizado"] == "OK")
            {
                return View(lista);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [OutputCache(NoStore = true, Duration = 0)]
        public async Task<ClienteViewModel> ListaClientes()
        {
            try
            {
                _client.BaseAddress = new System.Uri(Api.ListaTodosClientes);
                _client.DefaultRequestHeaders.Accept.Clear();
                _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await _client.GetAsync(_client.BaseAddress);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<ClienteViewModel>(result);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                erro = ex.Message;
                throw;
            }
        }

        [HttpPost]
        public async Task<ActionResult> CadastrarCliente()
        {
            try
            {
                ClienteViewModel cliente = new ClienteViewModel();

                cliente.Nome = Request["nome"];
                cliente.Tel = Request["tel"];
                cliente.Email = Request["email"];

                _client.BaseAddress = new System.Uri(Api.InsereCliente);
                _client.DefaultRequestHeaders.Accept.Clear();
                _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // Converter o objeto para JSON
                string json = JsonConvert.SerializeObject(cliente);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _client.PostAsync(_client.BaseAddress, content);

                if (response.IsSuccessStatusCode)
                {
                    TempData["sucesso_cad"] = "Cadastro realizado com sucesso";
                    return RedirectToAction("Index", "Cliente");
                }
                else
                {
                    return RedirectToAction("Index", "Cliente");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        [OutputCache(NoStore = true, Duration = 0)]
        public ActionResult Novo()
        {
            if (Session["Autorizado"] == "OK")
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [OutputCache(NoStore = true, Duration = 0)]
        public ActionResult PageNotFound()
        {
            return View();
        }

        public async Task<ClienteViewModel> ConsultaClientePorId(string id)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new System.Uri(Api.ConsultaCliente + "/id?" + "id=" + id);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage response = await _client.GetAsync(client.BaseAddress);

                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync();

                        return JsonConvert.DeserializeObject<ClienteViewModel>(result);
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                erro = ex.Message;
                throw;
            }
        }

        public async Task<ClienteViewModel> AtualizaDadosCliente(string id, string nome, string tel, string email)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var uri = new Uri(Api.EditaCliente + "?id=" + id + "&nome=" + nome + "&tel=" + tel + "&email=" + email);

                    var parameters = new Dictionary<string, string>
                    {
                        { "id", id },
                        { "nome", nome },
                        { "tel", tel },
                        { "email", email }
                    };

                    var content = new FormUrlEncodedContent(parameters);

                    HttpResponseMessage response = await client.PutAsync(uri, content);

                    if (response.IsSuccessStatusCode)
                    {
                        var result = "";
                        var resultado = JsonConvert.DeserializeObject<ClienteViewModel>(result);
                        return resultado;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                erro = ex.Message;
                throw;
            }
        }

        [HttpGet] // Apenas retorna os dados atuais do cliente antes de fazer a atualização
        [OutputCache(NoStore = true, Duration = 0)]
        public async Task<ActionResult> Editar(string id)
        {
            ClienteViewModel cliente = await ConsultaClientePorId(id);

            if (Session["Autorizado"] == "OK")
            {
                return View(cliente);
            }
            else
            {
                return RedirectToAction("Index", "Cliente");
            }
        }

        [HttpPost]
        public async Task<ActionResult> AlteraDadosCliente()
        {
            string id = Request["id"];
            string nome = Request["nome"];
            string tel = Request["tel"];
            string email = Request["email"];

            ClienteViewModel erro = await AtualizaDadosCliente(id, nome, tel, email);

            if (erro == null)
            {
                TempData["AtualizacaoDeDados"] = "Dados atualizados com sucesso";
                return RedirectToAction("Index", "Cliente");
            }
            else
            {
                TempData["Erro"] = "Erro no processamento dos dados";
                return RedirectToAction("Index", "Cliente");
            }
        }

        public async Task<ClienteViewModel> ExcluiCliente(string id)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var uri = new Uri(Api.ExcluiCliente + "/" + "id?id=" + id);

                    var parameters = new Dictionary<string, string>
                    {
                        { "id", id }
                    };

                    var content = new FormUrlEncodedContent(parameters);

                    HttpResponseMessage response = await client.PostAsync(uri, content);

                    if (response.IsSuccessStatusCode)
                    {
                        var result = "";
                        var resultado = JsonConvert.DeserializeObject<ClienteViewModel>(result);
                        return resultado;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                erro = ex.Message;
                throw;
            }
        }

        [OutputCache(NoStore = true, Duration = 0)]
        public async Task<ActionResult> Excluir(string id)
        {
            ClienteViewModel cliente = await ConsultaClientePorId(id);

            if (Session["Autorizado"] == "OK")
            {
                return View(cliente);
            }
            else
            {
                return RedirectToAction("Index", "Cliente");
            }
        }

        public async Task<ActionResult> DeletaCliente(string id)
        {
            ClienteViewModel erro = await ExcluiCliente(id);

            if (erro == null)
            {
                TempData["DeletaCliente"] = "Cliente excluído com sucesso";
                return RedirectToAction("Index", "Cliente");
            }
            else
            {
                TempData["Erro"] = "Erro no processamento dos dados";
                return RedirectToAction("Index", "Cliente");
            }
        }

        public async Task<ActionResult> Relatorio(string id)
        {
            try
            {
                var token = await RecuperaToken(id);

                if (token != null)
                {
                    string IdUsuario = "?id=" + id;

                    var urlCompleta = Api.RelatorioPDF + "/" + token + IdUsuario;

                    return Redirect(urlCompleta);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<string> RecuperaToken(string id)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    // Recebe a URL da Api que retornará um token
                    var url = Api.token + "id=" + id;

                    // Executa a URL contida na variável `url`
                    HttpResponseMessage response = await client.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        // Recebe o token obtido na variável `url`
                        string token = await response.Content.ReadAsStringAsync();

                        // Retorna o token
                        return token;
                    }
                    else
                    {
                        return null;
                    }
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }
    }
}