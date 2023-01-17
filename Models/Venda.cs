using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tech_Test_Payment_Api.Models;
namespace Tech_Test_Payment_Api.Models
{
    public class Venda
    {
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public Vendedor Vendedor { get; set; }
        public List<Produto> Items { get; set; }
        public EnumStatusVenda StatusVenda { get; set; }
    }
}