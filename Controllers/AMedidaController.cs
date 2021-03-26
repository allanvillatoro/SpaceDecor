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
    public class AMedidaController : Controller
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
                                   Thumbnail = s.Thumbnail

                               };

                lista = consulta.ToList();
            }

            ViewBag.pedido = TempData["pedido"];
            return View(lista);
        }


        public IActionResult Edit(int id)
        {
            List<Product> lista = new List<Product>();
            List<Product> lista2 = new List<Product>();
            using (DatabaseContext db = new DatabaseContext())
            {
                var consulta = from s in db.Products
                               where s.idProducts == id
                               select new Product
                               {
                                   idProducts = s.idProducts,
                                   ProductName = s.ProductName,
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
            List<Images> listaimage2 = new List<Images>();
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

            Product pr = new Product();
            listaimage2 = listaimage.ToList();
            pr = lista[0];
            pr.lstimages = listaimage2;



            return View(pr);
        }

        [HttpPost]
        public IActionResult CreateOrder(Product product)
        {

            using (DatabaseContext db = new DatabaseContext())
            {
                CustomOrders produ = new CustomOrders();
                produ.idproduct = product.idProducts;
                produ.idClient = (int)HttpContext.Session.GetInt32("Id");
                produ.ProductName = product.ProductName;
                produ.Price = product.Price;
                produ.ProductDesc = product.ProductDesc;
                produ.Color = product.Color;
                produ.Dimensions = product.Dimensions;
                produ.Materials = product.Materials;

                db.CustomOrders.Add(produ);

                int filasAfectadas = db.SaveChanges();
                if (filasAfectadas > 0)
                {
                    TempData["pedido"] = 1;
                }

                return RedirectToAction("IndexOrder");
            }

        }

        public IActionResult IndexOrder(int id)
        {
            List<CustomOrders> lista = new List<CustomOrders>();
            using (DatabaseContext db = new DatabaseContext())
            {
                var consulta = from s in db.CustomOrders
                               where s.idClient == (int)HttpContext.Session.GetInt32("Id")
                               select new CustomOrders
                               {
                                   idproduct = s.idproduct,
                                   ProductName = s.ProductName,
                                   Price = s.Price,

                               };

                lista = consulta.ToList();
            }


            ViewModels listas = new ViewModels();
            listas.lstorders = lista;


            //ViewBag.pedido = TempData["pedido"];
            return View(listas);
        }


    }






    }

