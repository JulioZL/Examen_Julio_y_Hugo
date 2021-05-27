using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class DAOAlumnosgrupos
    {
        public bool insertar (String nocontrol, int id)
        {
            MySqlCommand insert =
                 new MySqlCommand(@"insert into alumnosgrupos values
            (@nocontrol, @id);");
            insert.Parameters.AddWithValue("@nocontrol", nocontrol);
            insert.Parameters.AddWithValue("@id", id);
            int n=  Conexion.ejecutarSentencia(insert);
            return (n > 0);

        }
    }
}
