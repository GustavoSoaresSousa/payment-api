using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using desafioPottencialSeguradora.Context;
using desafioPottencialSeguradora.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace desafioPottencialSeguradora.Controllers
{
    
    [ApiController]
    [Route("[controller]")]
    public class VendaController : ControllerBase
    {
        private readonly VendaContext _context;

        public VendaController(VendaContext context){
            _context = context;
        }

        [HttpPost]
        public IActionResult Criar(Venda venda){
            if(venda.DadosDoVendedor.Nome == null) return BadRequest("Campo nome inválido ou vazio");
            if(venda.DadosDoVendedor.Cpf == null) return BadRequest("Campo Cpf inválido ou vazio");
            if(venda.DadosDoVendedor.Email == null) return BadRequest("Campo email inválido ou vazio");
            if(venda.DadosDoVendedor.Telefone == null) return BadRequest("Campo telefone inválido ou vazio");

            if(venda.ItensVendidos.Itens == null) return BadRequest("Campos inválido ou vazio");

            if(venda.StatusDaVenda > 0) return BadRequest("Realize o pagemento para atualizar os status de entrega");

            _context.Add(venda);
            _context.SaveChanges();
            return Ok(venda);
        }

        [HttpGet("{id}")]
        public IActionResult ObterPorId(int id){
            Venda venda = _context.Vendas.Find(id);
            if(venda == null) return NotFound("Não foi encontrado vendas pelo id informado");
            return Ok(venda);
        }

        [HttpGet("ObterTodos")]
        public IActionResult ObterTodos(){
            var vendas = _context.Vendas
            .Include(venda => venda.DadosDoVendedor)
            .Include(venda => venda.ItensVendidos)
            .ToList();
            if(vendas == null) return NotFound();
            return Ok(vendas);
        }


        // AguardandoPagamento,   0
        // PagamentoAprovado,  1
        // EnviadoParaTransportadora,  2
        // Entregue, 3
        // Cancelada 4
        
        [HttpPut("{id}")]
        public IActionResult AtualizarStatusDaCompra(int id, Venda venda){
 
            if(venda == null || ((int)venda.StatusDaVenda) >= 5) return BadRequest("campo status inválido ou vazio");
            Venda vendaBanco = _context.Vendas.Find(id);

            if(vendaBanco == null) return NotFound("Não foi encontrado vendas pelo id informado");
            
            if(((int)vendaBanco.StatusDaVenda) == 0 && ((int)venda.StatusDaVenda) == 2 
            ||((int)vendaBanco.StatusDaVenda) == 0 && ((int)venda.StatusDaVenda) == 3) return BadRequest("Faça o pagamento da compra primeiro");

            if(((int)vendaBanco.StatusDaVenda) == 1 && ((int)venda.StatusDaVenda) == 0) return BadRequest("Pagamento já aprovado!");               
            if(((int)vendaBanco.StatusDaVenda) == 2 && ((int)venda.StatusDaVenda) == 4) return BadRequest("Sua compra já está na transportadora e não pode mais ser cancelado");

            if(((int)vendaBanco.StatusDaVenda) == 3 && ((int)venda.StatusDaVenda) < 3 
            || ((int)vendaBanco.StatusDaVenda) == 3 && ((int)venda.StatusDaVenda) == 4) return BadRequest("Seu compra já foi entregue");
            

            if(((int)vendaBanco.StatusDaVenda) == 0 && ((int)venda.StatusDaVenda) == 1) {
                vendaBanco.StatusDaVenda = venda.StatusDaVenda;
                _context.Update(vendaBanco);
                _context.SaveChanges();
                return Ok("Seu pagamento foi aprovado");
            }

            if(((int)vendaBanco.StatusDaVenda) == 0 && ((int)venda.StatusDaVenda) == 4){ 
                vendaBanco.StatusDaVenda = venda.StatusDaVenda;
                _context.Update(vendaBanco);
                _context.SaveChanges();
                return Ok("Sua compra foi cancelada com sucesso");
            }

            if(((int)vendaBanco.StatusDaVenda) == 1 && ((int)venda.StatusDaVenda) == 2) {
                vendaBanco.StatusDaVenda = venda.StatusDaVenda;
                _context.Update(vendaBanco);
                _context.SaveChanges();
                return Ok("Sua compra foi enviada para a transportadora");
            }

            if(((int)vendaBanco.StatusDaVenda) == 1 && ((int)venda.StatusDaVenda) == 4) {
                vendaBanco.StatusDaVenda = venda.StatusDaVenda;
                _context.Update(vendaBanco);
                _context.SaveChanges();
                return Ok("Sua compra foi cancelada com sucesso");
            }

            if(((int)vendaBanco.StatusDaVenda) == 2 && ((int)venda.StatusDaVenda) == 3) {
                vendaBanco.StatusDaVenda = venda.StatusDaVenda;
                _context.Update(vendaBanco);
                _context.SaveChanges();
                return Ok("Sua compra foi entregue com sucesso");
            };  
            
            return BadRequest("Não foi possivel atualizar");
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id){
            Venda venda = _context.Vendas.Find(id);
            if(venda == null) return NotFound("Não foi encontrado vendas pelo id informado");

            _context.Remove(venda);
            _context.SaveChanges();

            return Ok("Venda deletada");
        }
    }
}

// Teste finalizado