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
        public IActionResult CreateProduct(Product product)
        {

            List<Product> lista = new List<Product>();
            int result;
            using (DatabaseContext db = new DatabaseContext())
            {
                var consulta = from s in db.Products
                               where s.idProducts == product.idProducts
                               select new Product
                               {
                                   idProducts = s.idProducts,
                               };

                lista = consulta.ToList();
            }


            if(lista.Count == 0)
            {

                try
                {
                    using (DatabaseContext db = new DatabaseContext())
                    {
                        Products insertProduct = new Products();
                        insertProduct.idProducts = product.idProducts;
                        insertProduct.ProductName = product.ProductDesc;
                        insertProduct.ProductDesc = product.ProductDesc;
                        insertProduct.Marca = product.Marca;
                        insertProduct.Price = product.Price;
                        insertProduct.Color = product.Color;
                        insertProduct.Dimensions = product.Dimensions;
                        insertProduct.Materials = product.Materials;
                        insertProduct.ProductType = product.ProductType;
                        insertProduct.StockQ = product.StockQ;
                        insertProduct.Thumbnail = SaveThumbnail(product.ThumbnailImage);


                        db.Products.Add(insertProduct);

                        int filasAfectadas = db.SaveChanges();
                        if (filasAfectadas > 0)
                        {
                            Console.WriteLine("Agregado exitosamente");
                            foreach (var image in product.Images)
                            {
                                SaveImages(image, product.idProducts);
                            }
                        }
                        else
                        {
                            ViewBag.err = 1;
                            ViewBag.msj = "error al agregar imagenes a db consulta";
                        }

                    }
                }
                catch (Exception ex)
                {
                    ViewBag.err = 1;

                    ViewBag.msj = "error al ingresar producto a db"+ex;
                    return View();
                }

            }
            else
            {
                ViewBag.err = 1;
                ViewBag.msj = "Mismo id";

            }

            return View();
        }



        public void SaveImages(IFormFile fileSelect, int id)
        {

            string url = "https://spacedecor2021.blob.core.windows.net/productsimage/";
            string urlFinal;

            //Subir a Azure
            string conn = "DefaultEndpointsProtocol=https;AccountName=spacedecor2021;AccountKey=IDhWTZdewBV6W9QOfYPE5A3HwGlWNCQN/5jz1mzaCweXllq1fabsA4PFi0Meg4/VYh2mGEQSfMnvSXyUG8BKzw==;EndpointSuffix=core.windows.net";

            BlobServiceClient blobServiceClient = new BlobServiceClient(conn);
            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient("productsimage");

            if (fileSelect != null)
            {
                using (var fileStream = fileSelect.OpenReadStream())
                {
                    var fileArray = fileSelect.FileName.Split('.');
                    string extFile = fileArray[1];
                    string fileName = $"{DateTime.Now.Ticks}.{extFile}";
                    BlobClient blobClient = containerClient.GetBlobClient(fileName);
                    urlFinal = url + fileName;


                    blobClient.Upload(fileStream, true);
                    
                }

                try
                {
                    using (DatabaseContext db = new DatabaseContext())
                    {
                        ProductsImg img = new ProductsImg();

                        img.idProducto = id;
                        img.ImageUrl = urlFinal;

                        db.ProductsImg.Add(img);

                        int filasAfectadas = db.SaveChanges();
                        if (filasAfectadas > 0)
                            Console.WriteLine("Agregado exitosamente");
                        else
                        {
                            ViewBag.err = 1;
                            ViewBag.msj= "error al agregar imagenes a db consulta";
                        }

                    };         
                }
                catch (Exception ex)
                {
                    ViewBag.err = 1;
                    ViewBag.msj = "error al agregar imagenes a db";
                }


            }
        }

        public string SaveThumbnail(IFormFile fileSelect)
        {
            string conn = "DefaultEndpointsProtocol=https;AccountName=spacedecor2021;AccountKey=IDhWTZdewBV6W9QOfYPE5A3HwGlWNCQN/5jz1mzaCweXllq1fabsA4PFi0Meg4/VYh2mGEQSfMnvSXyUG8BKzw==;EndpointSuffix=core.windows.net";

            BlobServiceClient blobServiceClient = new BlobServiceClient(conn);
            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient("productsimage");
            string url = "https://spacedecor2021.blob.core.windows.net/productsimage/";

            if (fileSelect != null)
            {
                using (var fileStream = fileSelect.OpenReadStream())
                {
                    var fileArray = fileSelect.FileName.Split('.');
                    string extFile = fileArray[1];
                    string fileName = $"{DateTime.Now.Ticks}.{extFile}";
                    BlobClient blobClient = containerClient.GetBlobClient(fileName);

                    blobClient.Upload(fileStream, true);

                    return url + fileName;
                } 
            }
            return "Error";
        }





    }
}
