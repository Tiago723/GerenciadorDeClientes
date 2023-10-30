using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GerenciadorDeClientes.Models
{
    public class ResultadoOperacao <T>
    {
        public bool ExecutouComSucesso { get; set; }
        public bool ExecutouComFalha { get; set; }
        public string MensagemRetorno { get; set; }
    }
}