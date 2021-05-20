using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using Datos;
using Modelos;

namespace ITSUR
{
    public partial class FrmCarrera : Form
    {
        private String claveCarrera;

        public FrmCarrera()
        {
            InitializeComponent();
        }

        public FrmCarrera(String claveCarrera) : this()
        {
            this.Text = "Editar Carrera " + claveCarrera;
            this.claveCarrera = claveCarrera;

            // Cargar la información de la carrera
            DAOCarrera dao = new DAOCarrera();
            Carrera carrera = dao.obtenerUna(claveCarrera);

            //Llenar los datos de la carrera
            txtClaveCarrera.Text = claveCarrera;
            txtClaveCarrera.Enabled = false;
            txtNombre.Text = carrera.Nombre;
            txtInicial.Text = carrera.Inicial.ToString();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (esValido())
            {
                Carrera objCarrera = llenarDatos();

                DAOCarrera DaoCarrera = new DAOCarrera();

                bool resultado;
                try
                {
                    if (claveCarrera == null)
                    {
                        // Es insertar
                        resultado = DaoCarrera.insertar(objCarrera);
                    }
                    else
                    {
                        // Es actualizar
                        resultado = DaoCarrera.actualizar(objCarrera);
                    }

                    if (resultado)
                    {
                        MessageBox.Show(this, "Carrera almacenada exitosamente.", "Operación exitosa.",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show(this, "Ha ocurrido error al realizar la operación, " +
                            "revisa la información y vuelve a intentar.", "Operación fallida.",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    if (ex.Message.Equals("Duplicado"))
                    {
                        MessageBox.Show(this, "Revisa los datos, la clave o el nombre de la carrera esta duplicado.", "Operación fallida", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show(this, "Ha ocurrido un error al realizar la operación", "Operación fallida", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show(this, "Uno o varios de los datos de la carrera no son validos," +
                    "revisa la información y vuelve a intentar.", "Datos no validos",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private Carrera llenarDatos()
        {
            Carrera objCarrera = new Carrera();
            objCarrera.ClaveCarrera = int.Parse(txtClaveCarrera.Text.Trim());
            objCarrera.Nombre = txtNombre.Text.Trim();
            objCarrera.Inicial = char.Parse(txtInicial.Text.Trim());
            return objCarrera;
        }

        private bool esValido()
        {
            // Verifica cada dato proporcionado al formulario para asegurarse que 
            // los datos esten completos y correctos
            // Hacer coincidir con la BD 
            // Trabajar con un error provider 
            Regex clave = new Regex(@"^[0-9]+$");
            Regex nom = new Regex(@"^[A-Z\s]+$");
            Regex ini = new Regex(@"^[A-Z]$");

            ErrProv.Clear();
            int cons = 3;
            if (!clave.IsMatch(txtClaveCarrera.Text.Trim()))
            {
                ErrProv.SetError(txtClaveCarrera, "La clave debe ser un número");
                cons--;
            }
            if (!nom.IsMatch(txtNombre.Text.Trim()))
            {
                ErrProv.SetError(txtNombre, "El nombre debe estar conformado solo por letras mayúsculas.");
                cons--;
            }
            if (!ini.IsMatch(txtInicial.Text.Trim()))
            {
                ErrProv.SetError(txtInicial, "Inicial debe ser una sola letra mayuscula.");
                cons--;
            }

            if (cons == 3) return true;
            else return false;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
