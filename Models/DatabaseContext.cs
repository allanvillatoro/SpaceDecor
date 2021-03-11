using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace ProyectoFinal.Models
{
    public class DatabaseContext : DbContext
    {
        //tablas
        public DbSet<Users> users { get; set;}

        //conexión Rachel
        //public string connectionString = @"Data Source = (LocalDB)\MSSQLLocalDB;Initial Catalog = ProyectoDB; Integrated Security = True";

        //Conexion Andres
        public string connectionString = @"Data Source = localhost; Initial Catalog = SpaceDecor; Integrated Security=True;";

        protected override void OnConfiguring(DbContextOptionsBuilder options)=> options.UseSqlServer(connectionString);



    }

    public class Users
    {
        public int id { get; set; }
        public string Mail { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }

    }

}
