using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GerenciadorDeClientes.URL
{
    public class MetodosApi
    {
        public readonly string ListaTodosClientes = "http://localhost/Pmesp_Api_Template2/Api/Pessoa/ListaTodosClientes";

        public readonly string InsereCliente = "http://localhost/Pmesp_Api_Template2/Api/Pessoa/InsereCliente";

        public readonly string EditaCliente = "http://localhost/Pmesp_Api_Template2/Api/Pessoa/EditaCliente";

        public readonly string ConsultaCliente = "http://localhost/Pmesp_Api_Template2/Api/Pessoa/ConsultaCliente";

        public readonly string ExcluiCliente = "http://localhost/Pmesp_Api_Template2/Api/Pessoa/ExcluiCliente";

        public readonly string RelatorioPDF = "http://localhost/Pmesp_Api_Template2/Api/Pessoa/Relatorio";

        public readonly string token = "http://localhost/Pmesp_Api_Template2/Api/Pessoa/Token?";
    }
}