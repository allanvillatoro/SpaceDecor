using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.IO;

namespace ProyectoFinal.Models
{
    public class Product
    {
        public int idProducts { get; set; }
        public string ProductName { get; set; }
        public string ProductDesc { get; set; }
        public string Marca { get; set; }
        public double Price { get; set; }
        public string Color { get; set; }
        public string Dimensions { get; set; }
        public string Materials { get; set; }
        public string ProductType { get; set; }
        public int StockQ { get; set; }
        public string Thumbnail { get; set; }

        public int cantidad { get; set; }


        public IEnumerable<Images> lstimages { get; set; }
        //para subir
        public IFormFile ThumbnailImage { get; set; }
        public IEnumerable<IFormFile> Images { get; set; }
    }
      
}
   
