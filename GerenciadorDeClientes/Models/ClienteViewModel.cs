using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GerenciadorDeClientes.Models
{
    public class ClienteViewModel
    {
        public string Id { get; set; }
        public string Nome { get; set; }
        public string Tel { get; set; }
        public string Email { get; set; }
        public List<ClienteViewModel> Resultado { get; set; }
    }
}