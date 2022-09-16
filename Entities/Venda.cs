using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace desafioPottencialSeguradora.Entities
{
    public class Venda
    {        
        public int Id { get; set; }
        public Vendedor DadosDoVendedor { get; set; }
        public Pedido ItensVendidos { get; set; }
        public Status StatusDaVenda { get; set; }

    }
}