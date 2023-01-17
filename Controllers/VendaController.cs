using Microsoft.AspNetCore.Mvc;
using Tech_Test_Payment_Api.Context;
using Microsoft.EntityFrameworkCore;
using Tech_Test_Payment_Api.Models;
using System.ComponentModel.DataAnnotations;
namespace Tech_Test_Payment_Api.Controllers
{
    [ApiController]
    [Route("Controller")]
    public class VendaController : ControllerBase
    {
        private readonly PaymentContext _context;

        public VendaController(PaymentContext context)
        {
            _context = context;
        }
        [HttpGet("ObterVenda/{id}")]
        public IActionResult ObterVendas(int id)
        {
            var vendaBanco = _context.Vendas.Find(id);
            if(vendaBanco == null)
                return NotFound();
            var venda = _context.Vendas.Where(x => x.Id == id).Include(v => v.Vendedor).Include(i => i.Items);

            return Ok(venda);
        }
        [HttpPost("CriarVenda")]
        public IActionResult CriarVenda([Required] int IdVendedor, [Required] DateTime Data, [Required] List<Produto> ProdutosVendidos)
        {
            Venda venda = new Venda();
            var vendedor = _context.Vendedores.Find(IdVendedor);

            if(vendedor == null)
                return NotFound("Vendedor não encontrado");
            if(ProdutosVendidos == null)
                return NotFound("Nenhum Produto Vendido");

            venda.StatusVenda = EnumStatusVenda.AguardandoPagamento;
            venda.Vendedor = vendedor;
            venda.Items = ProdutosVendidos;
            venda.Data = Data;

            _context.Add(venda);
            _context.SaveChanges();

            return CreatedAtAction(nameof(ObterVendas), new { id = venda.Id }, venda);
        }
        [HttpPut("EditarVenda/{id}")]
        public IActionResult EditarVenda(int id, EnumStatusVenda status)
        {
            var venda = _context.Vendas.Find(id);
            if(venda == null)
                return NotFound();

            if(StatusTeste(venda.StatusVenda, status))
            {
                venda.StatusVenda = status;

                _context.Vendas.Update(venda);
                _context.SaveChanges();

                return Ok(venda);
            }else{
                return BadRequest("Status Inválido");
            }
        }

         private bool StatusTeste(EnumStatusVenda StatusAnterior, EnumStatusVenda StatusNovo)
        {
            
            if (StatusAnterior == EnumStatusVenda.AguardandoPagamento
                && (StatusNovo == EnumStatusVenda.PagamentoAprovado || StatusNovo == EnumStatusVenda.Cancelado))
            {
                return true;
            }
           
            else if (StatusAnterior == EnumStatusVenda.PagamentoAprovado &&
                (StatusNovo == EnumStatusVenda.EnviadoParaTransportadora || StatusNovo == EnumStatusVenda.Cancelado))
            {
                return true;
            }
        
            else if (StatusAnterior == EnumStatusVenda.EnviadoParaTransportadora &&
               StatusNovo == EnumStatusVenda.Entregue)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}