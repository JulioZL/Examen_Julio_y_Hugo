using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;
using Modelos;
using Datos;

namespace Datos
{
    public class DAOCarrera
    {
        public DataTable obtenerTodas()
        {
            MySqlCommand consulta =
                new MySqlCommand(@"select ClaveCarrera, Nombre, Inicial
                    from carreras
                    order by Nombre");
            return Conexion.ejecutarConsulta(consulta);
        }

        public Carrera obtenerUna(String ClaveCarrera)
        {
            MySqlCommand consulta = new MySqlCommand(
                  @"select ClaveCarrera, Nombre, Inicial
                    from carreras
                    where ClaveCarrera = @ClaveCarrera");
            consulta.Parameters.AddWithValue("@ClaveCarrera", ClaveCarrera);
            DataTable resultado = Conexion.ejecutarConsulta(consulta);
            if (resultado != null && resultado.Rows.Count > 0)
            {
                DataRow fila = resultado.Rows[0];

                //Llenar los datos en una carrera
                Carrera carrera = new Carrera()
                {
                    ClaveCarrera = int.Parse(fila["ClaveCarrera"].ToString()),
                    Nombre = fila["Nombre"].ToString(),
                    Inicial = char.Parse(fila["Inicial"].ToString())
                };
                return carrera;
            }
            else
            {
                return null;
            }
        }

        public bool insertar(Carrera obj)
        {
            MySqlCommand insert = new MySqlCommand(
                    @"insert into Carreras 
                      values(@ClaveCarrera, @Nombre, @Inicial)"
                );
            insert.Parameters.AddWithValue("@ClaveCarrera", obj.ClaveCarrera);
            insert.Parameters.AddWithValue("@Nombre", obj.Nombre);
            insert.Parameters.AddWithValue("@Inicial", obj.Inicial);

            int resultado = Conexion.ejecutarSentencia(insert);
            return (resultado > 0);
        }

        public bool actualizar(Carrera obj)
        {
            MySqlCommand update = new MySqlCommand(
                    @"update carreras
                    set Nombre = @Nombre, Inicial = @Inicial
                    where ClaveCarrera = @ClaveCarrera"
                );
            update.Parameters.AddWithValue("@ClaveCarrera", obj.ClaveCarrera);
            update.Parameters.AddWithValue("@Nombre", obj.Nombre);
            update.Parameters.AddWithValue("@Inicial", obj.Inicial);

            int resultado = Conexion.ejecutarSentencia(update);
            return (resultado > 0);
        }

        public bool eliminar(String ClaveCarrera)
        {
            MySqlCommand delete = new MySqlCommand(
                    @"Delete from carreras 
                    where ClaveCarrera = @ClaveCarrera"
                );
            delete.Parameters.AddWithValue("@ClaveCarrera", ClaveCarrera);

            int resultado = Conexion.ejecutarSentencia(delete);
            return (resultado > 0);
        }
    }
}
