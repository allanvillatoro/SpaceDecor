using Microsoft.Data.SqlClient;
using System.Data;


namespace ProyectoFinal.Models
{
    public class ConexionContacto
    {
        public string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=SpaceDecorDB;Integrated Security=True";


        public int addContacto(Contacto cont)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                    SqlCommand cmd = new SqlCommand("insert into Contacto values (@nombre, @correo, @descripcion)", con);
                    cmd.Parameters.AddWithValue("@nombre", cont.nombre);
                    cmd.Parameters.AddWithValue("@correo", cont.correo);
                    cmd.Parameters.AddWithValue("@descripcion", cont.descripcion);
                    con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                
                    
                return 0;

            }
        }

        

    }
}
