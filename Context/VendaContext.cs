using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using desafioPottencialSeguradora.Entities;
using Microsoft.EntityFrameworkCore;

namespace desafioPottencialSeguradora.Context
{
    public class VendaContext : DbContext
    {
        public VendaContext(DbContextOptions<VendaContext> options) : base(options){

        }
        public DbSet<Venda> Vendas { get; set; }
    }
}