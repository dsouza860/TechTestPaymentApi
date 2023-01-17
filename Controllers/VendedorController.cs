using Microsoft.AspNetCore.Mvc;
using Tech_Test_Payment_Api.Context;
using Microsoft.EntityFrameworkCore;
using Tech_Test_Payment_Api.Models;
using System.ComponentModel.DataAnnotations;
namespace Tech_Test_Payment_Api.Controllers
{
    [ApiController]
    [Route("Controller")]
    public class VendedorController : ControllerBase
    {
        private readonly PaymentContext _context;

        public VendedorController(PaymentContext context)
        {
            _context = context;
        }
       [HttpGet("ObterVendedores/{id}")]
       public IActionResult ObterVendedorePorId(int id)
       {
            var vendedor = _context.Vendedores.Find(id);

            if(vendedor == null)
                return NotFound();
            return Ok(vendedor);
        }
        [HttpPost("AdicionarVendedor")]
        public IActionResult AdicionarVendedor([Required] string Nome, [Required] string Cpf, [Required][EmailAddress] string Email, [Required] string Telefone)
        {
            Vendedor vendedor = new Vendedor();

            vendedor.Nome = Nome;
            vendedor.Cpf = Cpf;
            vendedor.Email = Email;
            vendedor.Telefone = Telefone;

            _context.Add(vendedor);
            _context.SaveChanges();

            return CreatedAtAction(nameof(ObterVendedorePorId), new { id = vendedor.Id }, vendedor);
        }
    }
}