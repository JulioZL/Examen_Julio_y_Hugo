using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Datos;
using Modelos;
namespace ITSUR
{
    public partial class FrmAlumno : Form
    {
        private String noControl;
        public FrmAlumno()
        {
            InitializeComponent();
            this.Text = "Agregar nuevo alumno";
            //Coloca aquí el código requerido para cargar la lista de carreras, recuerda que debes usar las 3 propiedades del combo: datasourse, datamember y valuemember.
            cboCarrera.DataSource = new DAOCarrera().obtenerTodas();
            cboCarrera.DisplayMember = "Nombre";
            cboCarrera.ValueMember = "Clave";
            chkModificar.Visible = false;
        }

        public FrmAlumno(String noControl):this() {
            this.Text = "Editar alumno " + noControl;
            //Cargar el noControl en la variable local
            this.noControl = noControl;
            //Cargar la info del alumno
            DAOAlumno dao = new DAOAlumno();
            Alumno alumno = dao.obtenerUno(noControl);
            //Llenar los datos del alumno
            txtNoControl.Text = noControl;
            txtNoControl.Enabled = false;
            txtNombre.Text = alumno.Nombre;
            txtApellido1.Text = alumno.Apellido1;
            txtApellido2.Text = alumno.Apellido2;
            txtTelefono.Text = alumno.Telefono;
            dtpFechaNac.Value = alumno.FechaNac;
            cboCarrera.SelectedValue  = alumno.ClaveCarrera;
            chkModificar.Visible = true;
            txtContrasenia.Enabled = txtConfirmarContrasenia.Enabled = false;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (esValido())
            {
                //Crear un objeto Alumno y llenarlo con todos los datos, puedes obtener la carrera seleccionada en el combo con: comboBox1.SelectedValue, esta propiedad te devolverá el id de la carrera
                Alumno obj = llenarDatos();
                //Crear una instancia del DAO
                DAOAlumno dao = new DAOAlumno();
                //Enviar el objeto alumno creado al método insertar del DAO 
                bool resultado;
                //y obtener el resultado
                if (noControl == null)
                {
                    obj.Contrasenia = txtContrasenia.Text;
                    //Es insertar
                    resultado=dao.insertar(obj);                    
                }
                else {
                    if (chkModificar.Checked)
                    {
                        obj.Contrasenia = txtContrasenia.Text;
                    }
                    //Es actualizar
                    resultado = dao.actualizar(obj);
                }

                //Cuando la operación se llevó a cabo de manera correcta
                //envía un mensaje informando que se guardó y cierra el form
                if (resultado)
                {
                    MessageBox.Show(this, "Alumno almacenado exitósamente", "Operación exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    //Si la operacón falla, envía también un
                    //mensaje al usuario y evita cerrar el form
                    MessageBox.Show(this, "Ha ocurrido un error al realizar la operación", "Operación fallida", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else {
                MessageBox.Show(this,"Uno o varios de los datos del alumno no son válidos, revisa la información y vuelve a intentar", "Datos no válidos",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
        }


        private Alumno llenarDatos() {
            Alumno alumno = new Alumno();
            alumno.NoControl = txtNoControl.Text.Trim();
            alumno.Nombre = txtNombre.Text.Trim();
            alumno.Apellido1 = txtApellido1.Text.Trim();
            alumno.Apellido2 = txtApellido2.Text.Trim();
            alumno.Telefono = txtTelefono.Text.Trim();
            alumno.FechaNac = dtpFechaNac.Value;
            alumno.ClaveCarrera = int.Parse(cboCarrera.SelectedValue.ToString());

            return alumno;
        }

        /// <summary>
        /// Verifica que cada dato proporcionado en el formulario
        /// esté completo y correcto
        /// </summary>
        /// <returns>true en caso que todo sea válido, false en caso contrario</returns>
        private bool esValido() {
            //TODO: Verifica cada dato proporcionado en el formulario
            //para asegurarse que los datos estén completos y correctos
            //Hacer coincidir con la BD
            //Trabajar con error provider

            //TODO: Cambiar con la respuesta adecuada
            return true;
        }

        private void chkModificar_CheckedChanged(object sender, EventArgs e)
        {
            txtContrasenia.Enabled = txtConfirmarContrasenia.Enabled = chkModificar.Checked;
        }
    }
}
