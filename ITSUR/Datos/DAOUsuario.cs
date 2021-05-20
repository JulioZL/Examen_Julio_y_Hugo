using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Threading.Tasks;
using Modelos;
using MySql.Data.MySqlClient;


namespace Datos
{
    public class DAOUsuario
    {
        public Usuario validarUsuario(Usuario usuario)
        {
            MySqlCommand consulta =
                new MySqlCommand(@"SELECT *
                    FROM Usuario WHERE usuario=@usuario and contrasenia=shal1(@contrasenia)");
            consulta.Parameters.AddWithValue("@usuario", usuario.NombreUsuario);
            consulta.Parameters.AddWithValue("@contrasenia", usuario.Contrasenia);
            DataTable resultado = Conexion.ejecutarConsulta(consulta);
            if (resultado != null && resultado.Rows.Count > 0) //Si coincide
            {
                DataRow fila = resultado.Rows[0];
                usuario.IdUsuario = int.Parse(fila["idUsuario"].ToString());
                usuario.Nombre = fila["nombre"].ToString();
                usuario.Apellido = fila["apellido"].ToString();
                usuario.TipoUsuario = int.Parse(fila["tipoUsuario"].ToString());
                return usuario;
            }
            else
            {
                return null;
            }

        }
    }
}

