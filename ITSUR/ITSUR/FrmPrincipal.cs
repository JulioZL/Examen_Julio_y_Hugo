using System;
using System.Windows.Forms;
using Modelos;
using MySql.Data.MySqlClient;

namespace ITSUR
{
    public partial class FrmPrincipal : Form
    {
        public static String ClaveUsuario { get; set; }
        public static int TipoUsuario { get; set; }

        private int childFormNumber = 0;

        public FrmPrincipal()
        {
            InitializeComponent();
            MessageBox.Show("El tipo de usuario es " + TipoUsuario);
        }
        public FrmPrincipal(String nada)
        {
            MessageBox.Show("tipo user " + TipoUsuario);
            InitializeComponent();
            if(TipoUsuario == 1)
            {
                //this.mnuCatalogos.Enabled = false;
                //this.opcionesToolStripMenuItem.Visible = false;
            }
            else if(TipoUsuario == 2)
            {
                //oculatmos opciones
                this.opcionesToolStripMenuItem.Enabled = false;
                //dejamos solo grupos
                this.mnuCatalogos.DropDownItems[0].Visible = false;
                this.mnuCatalogos.DropDownItems[1].Visible = false;
                this.mnuCatalogos.DropDownItems[2].Visible = false;
                this.mnuCatalogos.DropDownItems[3].Visible = false;
            }else if(TipoUsuario == 3)
            {
                this.mnuCatalogos.Visible = false;

            }

        }

        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }

        private void fileMenu_Click(object sender, EventArgs e)
        {

        }

        private void alumnosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmListaAlumnos childForm = new FrmListaAlumnos();
            childForm.MdiParent = this;
            childForm.Show();
        }

        private void materiasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmListaMaterias childForm = new FrmListaMaterias();
            childForm.MdiParent = this;
            childForm.Show();
        }

        private void gruposToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmListaGrupos childForm = new FrmListaGrupos();
            childForm.MdiParent = this;
            childForm.Show();
        }

        private void carrerasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmListaCarreras childForm = new FrmListaCarreras();
            childForm.MdiParent = this;
            childForm.Show();
        }

        private void FrmPrincipal_Load(object sender, EventArgs e)
        {

        }
        public void tipoUsuarioFmr()
        {
            Usuario obj = new Usuario();
            int user = obj.TipoUsuario;
            MessageBox.Show("int "+ user);
            if(user == 0)
            {
                mnuCatalogos.Visible = false;
                /* MySqlCommand consulta =
                 new MySqlCommand(@"SELECT tipoUsuario
                     FROM Usuario WHERE usuario=1 ");
                */

                // SI ENCUENTRA QUE ES UN ALUMNO SOLO MOSTRAR INSCRIPCION
                Alumno alum = new Alumno();
                // SI NO ESTA INSCRITO ENTONCES PUEDE TENER ACCESO
                //SI ESTA INSCRITO NO PUEDE HACER NADA 
                /*
                if (alumn.inscrito == true)
                {
                    mnuCatalogos.Visible=false;

                }*/
            }
            else if (user == 1)
            {
                mnuCatalogos.Visible = false;
                // SI ENCUENTRA QUE ES UN ALUMNO SOLO MOSTRAR INSCRIPCION
                if (user==4)
                {
                    opcionesToolStripMenuItem.Visible = false;

                }
            }
            else if (user == 2)
            {

            }

        }
    }
}
