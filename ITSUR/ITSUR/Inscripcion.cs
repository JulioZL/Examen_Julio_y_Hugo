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
    public partial class Inscripcion : Form
    {
        public Inscripcion()
        {
            InitializeComponent();
            actualizar();
        }
        private void actualizar()
        {
            DAOGrupo dao = new DAOGrupo();
            //MessageBox.Show(FrmPrincipal.NoControl);
            int carrera = new DAOAlumno().obtenerCarrera(FrmPrincipal.NoControl);
            dataGridView1.DataSource = dao.obtener_por_carrera(carrera);
        }

        private void Inscripcion_Load(object sender, EventArgs e)
        {

        }

        private void Terminar_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow x in dataGridView1.Rows)
            {
                if(x.DefaultCellStyle.BackColor == Color.Orange)
                {
                    //obtenemos valor del label 
                   int cont =int.Parse( label2.Text);
                    //obtener nombre de la materia 
                    string valor = x.Cells[6].Value.ToString();
                    
                    try
                    {
                        //optenemos cupo de grupo 
                        string cupo = x.Cells[3].Value.ToString();
                        int nuevo_cupo = int.Parse(cupo);
                        if (nuevo_cupo >= 1)
                        {
                            //obtenemos clave de grupo para cambiar cupo
                            string clave_grupo = x.Cells[4].Value.ToString();
                            //obtenemos id de grupo
                            string id_grupo = x.Cells[0].Value.ToString();
                            int idGrupo = int.Parse(id_grupo);
                            //QUITAMOS UN CUPO
                            DAOGrupo dao = new DAOGrupo();
                            Grupo obj_grupo = dao.obtenerUno(idGrupo);
                            obj_grupo.Cupo -= 1;
                            //actualizamos en la base de datos
                            bool check_grupo = dao.actualizar(obj_grupo);
                            //mostramos los nuevos resultados
                            actualizar();
                        }
                        
                    }
                    catch (Exception ex) { }
                    //QUITAMOS CREDITOS AL ALUMNO 
                    int creditos_alum = int.Parse(valor);
                    try
                    {
                        
                        cont -= creditos_alum;
                        if (cont >= 0)
                        {
                            label2.Text = cont.ToString();
                        }
                        else
                        {
                            cont += creditos_alum;
                            MessageBox.Show("CREDITOS INSUFICIENTES"); 
                        }
                        
                    }
                    catch (ArithmeticException arit) {}
                    
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btn_seleccionar_Click(object sender, EventArgs e)
        {
            try
            {
                int index = dataGridView1.SelectedRows[0].Index;
                dataGridView1.Rows[index].DefaultCellStyle.BackColor = Color.Orange;
               
            }catch(Exception ex)
            {

            }
        }

        private void btn_quitar_Click(object sender, EventArgs e)
        {
            int index = dataGridView1.SelectedRows[0].Index;
            dataGridView1.Rows[index].DefaultCellStyle.BackColor = Color.White;
        }
    }
}
