using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoFinal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;

namespace ProyectoFinal.Controllers
{
    public class CatalogoController : Controller
    {
        public IActionResult Index()
        {

            List<Product> lista = new List<Product>();
            using (DatabaseContext db = new DatabaseContext())
            {
                var consulta = from s in db.Products
                               select new Product
                               {
                                   idProducts = s.idProducts,
                                   ProductName = s.ProductName,
                                   ProductDesc = s.ProductDesc,
                                   Marca = s.Marca,
                                   Price = s.Price,
                                   Color = s.Color,
                                   Dimensions = s.Dimensions,
                                   Materials = s.Materials,
                                   ProductType = s.ProductType,
                                   StockQ = s.StockQ,
                                   Thumbnail=s.Thumbnail
                                  
                               };

                lista = consulta.ToList();
            }

            return View(lista);
        }


        [HttpGet]
        public IActionResult Details(int id)
        {
            List<Product> lista = new List<Product>();
            using (DatabaseContext db = new DatabaseContext())
            {
                var consulta = from s in db.Products
                               where s.idProducts == id
                               select new Product
                               {
                                   idProducts = s.idProducts,
                                   ProductName = s.ProductName,
                                   ProductDesc = s.ProductDesc,
                                   Marca = s.Marca,
                                   Price = s.Price,
                                   Color = s.Color,
                                   Dimensions = s.Dimensions,
                                   Materials = s.Materials,
                                   ProductType = s.ProductType,
                                   StockQ = s.StockQ,
                                   Thumbnail = s.Thumbnail

                               };

                lista = consulta.ToList();
            }

            List<Images> listaimage = new List<Images>();
            using (DatabaseContext db = new DatabaseContext())
            {
                var consulta = from s in db.ProductsImg
                               where s.idProducto == id
                               select new Images
                               {
                                   idProduct = s.idProducto,
                                   url = s.ImageUrl
                                        
                               };

                listaimage = consulta.ToList();
            }

            ViewModels listas = new ViewModels();
            listas.lstproducts = lista;
            listas.lstimages = listaimage;

            return View(listas);
        }


    }   

    }

   

