using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tech_Test_Payment_Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Tech_Test_Payment_Api.Context
{
    public class PaymentContext : DbContext
    {
        public PaymentContext(DbContextOptions<PaymentContext> options) : base(options)
        {

        }
       public DbSet<Venda> Vendas { get; set; }
       public DbSet<Vendedor> Vendedores { get; set; }
    }
}