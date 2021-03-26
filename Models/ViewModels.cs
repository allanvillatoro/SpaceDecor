using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoFinal.Models
{
    public class ViewModels
    {
        public IEnumerable<Product> lstproducts { get; set; }
        public IEnumerable<Images> lstimages { get; set; }

        public IEnumerable<CustomOrders> lstorders { get; set; }

        public Product prt { get; set; }
    }
}
