using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using Modelos;
using MySql.Data.MySqlClient;
using Datos;
using System.Threading;
using System.Collections;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Drawing;

namespace ITSUR

{
    public partial class FrmPrincipal : Form
    {
        public static String ClaveUsuario { get; set; }
        public static int TipoUsuario { get; set; }
        public static String NoControl { get; set; }
        public static String Inscrito { get; set; }

        private int childFormNumber = 0;
        public static List<String> cupos = new List<String>();
        public Task inicio_cupos;

        /*
            public FrmPrincipal()
            {
                InitializeComponent();
                MessageBox.Show("El tipo de usuario es " + TipoUsuario);
            }
        */
        public FrmPrincipal()
        {

            InitializeComponent();
            

            //MessageBox.Show("tipo user " + TipoUsuario);
            //DEPENDE EL TIPO DE USUARIO VAMOS A MOSTRAR U OCULTAR COSAS 
            if (TipoUsuario == 1)
            {
                //SOLO CATALOGOS 
                //this.mnuCatalogos.Enabled = false;
                this.opcionesToolStripMenuItem.Visible = false;
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
                //OCULTAMOS LOS CATALAGOS PORQUE ES UN ALUMNO
              
                this.mnuCatalogos.Visible = false;
                
                //DEBEMOS OBTENER LOS DATOS DEL ALUMNO
                DAOAlumno caliz = new DAOAlumno();
                //HACEMOS LA CONSULTA PARA OBTENER TODOS LOS DATOS Y SABER 
                //SI ESTA INSCRITO
                Alumno alum = caliz.obtenerUno(NoControl);

                //CON LOS DATOS LLENOS DEL ALUMNO VERIFICAMOS SI ESTA INSCRITO
                if (alum.Inscrito.Equals("S"))
                {
                    //OCULTAMOS LAS COSAS DE ALUMNO INSCRITO
                    this.capturaDeCalificacionesToolStripMenuItem.Visible = false;
                    this.cargaAcademicaToolStripMenuItem.Visible = true;
                }
                else
                {
                    MessageBox.Show("EL ALUMNO NO SE ENCUENTRA INSCRITO");
                    this.cargaAcademicaToolStripMenuItem.Visible = false;

                    //DataGridView auxCupo = new DataGridView();
                    //obtenemos la clave de la carrera
                    
                    DAOGrupo daoG = new DAOGrupo();
                    int carrera = new DAOAlumno().obtenerCarrera(FrmPrincipal.NoControl);
                    auxCupo.DataSource = daoG.obtener_cupo_grupos(carrera);

                    
                    foreach (DataGridViewRow x in auxCupo.Rows)
                    {
                        

                        if (auxCupo.Rows[x.Index].Cells[0] != null)
                        {
                            //recuperamos los valores de cada fila
                            string nombre_materia = auxCupo.Rows[x.Index].Cells[0].Value.ToString();
                            string clave_grupo = auxCupo.Rows[x.Index].Cells[1].Value.ToString();
                            string horario = auxCupo.Rows[x.Index].Cells[2].Value.ToString();
                           
                            string cupo = auxCupo.Rows[x.Index].Cells[3].Value.ToString();
                            int cupo_arr = int.Parse(cupo);
                            
                            if (cupo_arr <= 5)
                            {
                                String chido = nombre_materia + " Y CUPO: " + cupo;
                                cupos.Add(chido);
                            }
                                
                            

                        }
                    }
                    inicio_cupos = Task.Run(() => llenar_notify(cupos));
                
                    




                }
            }

        }
        public async static void llenar_notify(List<String> cupos)
        {
            
            NotifyIcon noti = new NotifyIcon();
            noti.Icon = new Icon("icono.ico");
             
            
               
            for (int i = 0; i < cupos.Count; i++)
            {
                        
                noti.Visible = true;
                noti.BalloonTipTitle = "ATENCIÓN";
                noti.BalloonTipText = cupos[i];
                noti.ShowBalloonTip(2000);
                await Task.Delay(15000);

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

        private void capturaDeCalificacionesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Inscripcion frm = new Inscripcion();
            frm.Show();
            this.Hide();
        }

        private void cargaAcademicaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConsultaCarga cargaA = new ConsultaCarga();
            cargaA.Show();
            this.Hide();
        }
    }
}
