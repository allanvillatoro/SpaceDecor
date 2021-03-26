using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace ProyectoFinal.Models
{
    public class DatabaseContext : DbContext
    {
        //tablas
        public DbSet<Users> Users { get; set; }
        public DbSet<Products> Products { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<CustomOrders> CustomOrders { get; set; }
        public DbSet<ProductsImg> ProductsImg { get; set;}

        //conexión Rachel
        public string connectionString = @"Data Source = (LocalDB)\MSSQLLocalDB;Initial Catalog = SpaceDecorDB; Integrated Security = True";

        //Conexion Andres
        //public string connectionString = @"Data Source = localhost; Initial Catalog = SpaceDecor; Integrated Security=True;";

        protected override void OnConfiguring(DbContextOptionsBuilder options)=> options.UseSqlServer(connectionString);



    }

    public class Users
    {
        [Key]
        public int idUsers { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Userpass { get; set; }
        public int UserAdmin { get; set; }
    }

    public class Products
    {
        [Key]
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

    }

    public class Orders
    {
        [Key]
        public int idOrder { get; set; }
        public int idClient { get; set; }
        public int idProduct { get; set; }
        public string ProductName { get; set; }
        public double Price { get; set; }
        public DateTime DateOrder { get; set; }
    }

    public class ProductsImg
    {
        [Key]
        public int idImage { get; set; }
        public int idProducto { get; set; }
        public string ImageUrl { get; set; }
        public string Nombre { get; set; }
    }

    public class CustomOrders
    {
        [Key]
        public int idOrder { get; set; }
        public int idClient { get; set; }
        public string ProductName { get; set; }
        public string ProductDesc { get; set; }
        public double Price { get; set; }
        public string Color { get; set; }
        public string Dimensions { get; set; }
        public string Materials { get; set; }
        public int Quantity { get; set; }
        public int idproduct { get; set; }


    }


    

}
