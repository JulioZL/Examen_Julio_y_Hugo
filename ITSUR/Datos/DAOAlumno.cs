using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Modelos;
namespace Datos
{
    public class DAOAlumno
    {
        public DataTable obtenerTodos() {
            MySqlCommand consulta =
                new MySqlCommand(@"SELECT NoControl `Num Control`,
                    CONCAT(Apellido1, ' ',
                    CASE WHEN Apellido2 is null THEN '' ELSE
                    CONCAT(Apellido2, ' ') END,
                    a.Nombre) Alumno,
                    c.Nombre Carrera
                    FROM Alumnos a 
                    JOIN Carreras c ON a.ClaveCarrera = c.ClaveCarrera
                    ORDER BY Carrera, Alumno");
            return Conexion.ejecutarConsulta(consulta);

        }

        public Alumno obtenerUno(String noControl)
        {
            MySqlCommand consulta =
                new MySqlCommand(@"SELECT NoControl,
                        Nombre,
                        Apellido1,
                        ifnull(Apellido2,'') Apellido2,
                        Telefono,
                        FechaNac,
                        ClaveCarrera
                    FROM Alumnos a 
                    WHERE Nocontrol=@NoControl");
            consulta.Parameters.AddWithValue("@NoControl",noControl);
            DataTable resultado= Conexion.ejecutarConsulta(consulta);
            if (resultado != null && resultado.Rows.Count > 0)
            {
                DataRow fila = resultado.Rows[0];
                //Llenar los datos en un a alumno
                Alumno alumno = new Alumno() {
                    NoControl = noControl,
                    Nombre = fila["Nombre"].ToString(),
                    Apellido1 = fila["Apellido1"].ToString(),
                    Apellido2 = fila["Apellido2"].ToString(),
                    Telefono = fila["Telefono"].ToString(),
                    FechaNac = DateTime.Parse(fila["FechaNac"].ToString()),
                    ClaveCarrera = int.Parse(fila["ClaveCarrera"].ToString())
                };
                return alumno;
            }
            else {
                return null;
            }

        }
        public bool insertar(Alumno obj) {
            MySqlCommand insert = new MySqlCommand(
                @"INSERT INTO Alumnos VALUES(@NoControl,
                    @Nombre,@Apellido1,@Apellido2,
                    @Telefono,@FechaNac,@ClaveCarrera)"
                );
            insert.Parameters.AddWithValue("@NoControl", obj.NoControl);
            insert.Parameters.AddWithValue("@Nombre", obj.Nombre);
            insert.Parameters.AddWithValue("@Apellido1", obj.Apellido1);
            insert.Parameters.AddWithValue("@Apellido2", obj.Apellido2);
            insert.Parameters.AddWithValue("@Telefono", obj.Telefono);
            insert.Parameters.AddWithValue("@FechaNac", obj.FechaNac);
            insert.Parameters.AddWithValue("@ClaveCarrera", obj.ClaveCarrera);

            int resultado = Conexion.ejecutarSentencia(insert);
            return (resultado > 0);
        }

        public bool actualizar(Alumno obj)
        {
            MySqlCommand update = new MySqlCommand(
                @"UPDATE Alumnos
                SET Nombre=@Nombre, Apellido1=@Apellido1, 
                Apellido2=@Apellido2, Telefono=@Telefono,
                FechaNac=@FechaNac, ClaveCarrera=@ClaveCarrera
                WHERE NoControl=@NoControl"
                );
            update.Parameters.AddWithValue("@NoControl", obj.NoControl);
            update.Parameters.AddWithValue("@Nombre", obj.Nombre);
            update.Parameters.AddWithValue("@Apellido1", obj.Apellido1);
            update.Parameters.AddWithValue("@Apellido2", obj.Apellido2);
            update.Parameters.AddWithValue("@Telefono", obj.Telefono);
            update.Parameters.AddWithValue("@FechaNac", obj.FechaNac);
            update.Parameters.AddWithValue("@ClaveCarrera", obj.ClaveCarrera);

            int resultado = Conexion.ejecutarSentencia(update);
            return (resultado > 0);
        }

        public bool eliminar(String noControl)
        {
            MySqlCommand delete = new MySqlCommand(
                @"DELETE FROM alumnos
                WHERE NOCONTROL=@NoControl"
                );
            delete.Parameters.AddWithValue("@NoControl", noControl);
            
            int resultado = Conexion.ejecutarSentencia(delete);
            return (resultado > 0);
        }
    }
}
