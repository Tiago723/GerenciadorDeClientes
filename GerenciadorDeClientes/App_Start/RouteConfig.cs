using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace GerenciadorDeClientes
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
               name: "ValidaLogin",
               url: "ValidaLogin",
               new { controller = "Login", action = "ValidaLogin", id = 0 }
           );

            // ***************************************************************************************************

            routes.MapRoute(
                name: "ListaClientes",
                url: "ListaClientes",
                new { controller = "Cliente", action = "Index", id = 0 }
            );

            routes.MapRoute(
                name: "CadastrarCliente",
                url: "CadastrarCliente",
                defaults: new { controller = "Cliente", action = "CadastrarCliente", id = 0 }
            );

            routes.MapRoute(
                name: "GerenciadorDeClientes/EditarCliente",
                url: "GerenciadorDeClientes/EditarCliente",
                new { controller = "Cliente", action = "Editar", id = 0 }
            );

            routes.MapRoute(
                name: "AlteraDadosCliente",
                url: "AlteraDadosCliente",
                new { controller = "Cliente", action = "AlteraDadosCliente", id = 0 }
            );

            routes.MapRoute(
                name: "Excluir/{id}",
                url: "Excluir/{id}",
                new { controller = "Cliente", action = "Excluir", id = 0 }
            );

            routes.MapRoute(
                name: "DeletaCliente/{id}",
                url: "DeletaCliente/{id}",
                new { controller = "Cliente", action = "DeletaCliente", id = 0 }
            );

            routes.MapRoute(
                name: "BuscaPorId/{id}",
                url: "BuscaPorId/{id}",
                new { controller = "Cliente", action = "BuscaPorId", id = 0 }
            );

            routes.MapRoute(
                name: "ConsultaClientePorId/{id}",
                url: "ConsultaClientePorId/{id}",
                new { controller = "Cliente", action = "ConsultaClientePorId", id = 0 }
            );

            routes.MapRoute(
               name: "Relatorio/{id}",
               url: "Relatorio/{id}",
               new { controller = "Cliente", action = "Relatorio", id = 0 }
           );

            routes.MapRoute(
               name: "RecuperaToken",
               url: "RecuperaToken",
               new { controller = "Cliente", action = "RecuperaToken", id = 0 }
           );

            routes.MapRoute(
               name: "PageNotFound",
               url: "PageNotFound",
               new { controller = "Cliente", action = "PageNotFound", id = 0 }
           );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
