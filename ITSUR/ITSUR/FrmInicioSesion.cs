using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Modelos;
using Datos;
namespace ITSUR
{
    public partial class FrmInicioSesion : Form
    {
        public FrmInicioSesion()
        {
            InitializeComponent();
        }

        private void btnEntrar_Click(object sender, EventArgs e)
        {
            Usuario obj = new Usuario();
            Alumno alum = new Alumno();
            
            obj.NombreUsuario = txtUsuario.Text;
            obj.Contrasenia = txtContraseña.Text;

            if (new DAOUsuario().validarUsuario(obj))
            {
                FrmPrincipal.ClaveUsuario = obj.ClaveGenerica;
                FrmPrincipal.TipoUsuario = obj.TipoUsuario;
                FrmPrincipal.NoControl = obj.NombreUsuario;


                new FrmPrincipal().Show();
                this.Hide();
            }
            else {
                MessageBox.Show("Usuario y/o contraseña incorrectos");
            }
        }

        private void FrmInicioSesion_Load(object sender, EventArgs e)
        {

        }

        public void txtUsuario_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
