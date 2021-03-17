using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure;
using Azure.Storage;
using ProyectoFinal.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using Azure.Storage.Blobs;

namespace ProyectoFinal.Controllers
{
    public class AdminController: Controller
    {
        DatabaseContext db = new DatabaseContext();
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Pedidos()
        {
            List<Order> lista = new List<Order>();
            var consulta = from s in db.Orders
                           select new Order
                           {
                               id_order = s.idOrder,
                               id_product = s.idProduct,
                               id_cliente = s.idClient,
                               date = s.DateOrder
                           };
            lista = consulta.ToList();

            return View(lista);
        }


        public IActionResult Products()
        {
            List<Product> lista = new List<Product>();
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

            return View(lista);
        }


        public IActionResult CreateProduct()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateProduct(Product producto)
        {

            IFormFile image = producto.image;

            string conn = "DefaultEndpointsProtocol=https;AccountName=spacedecor2021;AccountKey=IDhWTZdewBV6W9QOfYPE5A3HwGlWNCQN/5jz1mzaCweXllq1fabsA4PFi0Meg4/VYh2mGEQSfMnvSXyUG8BKzw==;EndpointSuffix=core.windows.net";
            
            BlobServiceClient blobServiceClient = new BlobServiceClient(conn);


            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient("productsimage");

            BlobClient blobClient = containerClient.GetBlobClient("prueba");


            using (var fileStream = image.OpenReadStream())
            {

                blobClient.Upload(fileStream, true);
            }

             
          



            return View();
        }




        
      
    }
}
