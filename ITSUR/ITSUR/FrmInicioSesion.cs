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
            obj.NombreUsuario = txtUsuario.Text;
            obj.Contrasenia = txtContraseña.Text;

            if (new DAOUsuario().validarUsuario(obj))
            {
                FrmPrincipal prin = new FrmPrincipal();


                FrmPrincipal.ClaveUsuario = obj.ClaveGenerica;
                FrmPrincipal.TipoUsuario = obj.TipoUsuario;

                //new FrmPrincipal().Show();
                //llamamos al metodo para saber que cosas puede mostrar el frame
                //prin.tipoUsuarioFmr();
                new FrmPrincipal(obj).Show();
                this.Hide();
            }
            else {
                MessageBox.Show("Usuario y/o contraseña incorrectos");
            }
        }

        private void FrmInicioSesion_Load(object sender, EventArgs e)
        {

        }
    }
}
