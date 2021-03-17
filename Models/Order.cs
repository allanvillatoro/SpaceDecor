using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoFinal.Models
{
    public class Order
    {
        public int id_order { get; set; }
        public int id_product { get; set; }
        public int id_cliente { get; set; }
        public DateTime date { get; set; }
    }
}
