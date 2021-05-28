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
        Dictionary<int, int> bucket = new Dictionary<int, int>();
        int []A=  new int [105];
        int i = 0;
       

        public Inscripcion()
        {
            InitializeComponent();
            DAOGrupo dao = new DAOGrupo();
            //MessageBox.Show(FrmPrincipal.NoControl);
            int carrera = new DAOAlumno().obtenerCarrera(FrmPrincipal.NoControl);
            dataGridView1.DataSource = dao.obtener_por_carrera(carrera);
        }
       

        private void Inscripcion_Load(object sender, EventArgs e)
        {

        }
        //metodo para determinar el cupo de las matrias
        public void llenar_array_cupos_grupos()
        {

        }
        
        private void Terminar_Click(object sender, EventArgs e)
        {
            //FOREACH PARA VALIDAR QUE NO QUIERA INSCRIBIRSE 
            //EN DOS GRUPOS A LA MISMA HORA
            bool check_horario = true;
            foreach (DataGridViewRow x in dataGridView1.Rows)
            {
                if (dataGridView1.Rows[x.Index].DefaultCellStyle.BackColor == Color.Orange)
                {
                    //seleccionar horario

                    string horario = dataGridView1.Rows[x.Index].Cells[5].Value.ToString();
                    int horario_int = int.Parse(horario);
                    int value = 1;
                    if(bucket.TryGetValue(horario_int, out value))
                    {
                        check_horario = false;
                    }
                    else bucket.Add(horario_int, value);

                }
            }
            //SI NO CHOCA NINGUN GRUPO PUEDE INSCRIBIRSE
            if(check_horario != false)
            {
                //RECORREMOS EL GRID
                foreach (DataGridViewRow x in dataGridView1.Rows)
                {


                    //VERIFICAMOS QUE SOLO TOMEMOS LOS DATOS QUE ESTEN SELECIONADOS NARANJA
                    if (dataGridView1.Rows[x.Index].DefaultCellStyle.BackColor == Color.Orange)
                    {

                        //obtenemos valor del label 
                        int cont = int.Parse(label2.Text);
                        //obtener nombre de la materia 
                        string valor = x.Cells[6].Value.ToString();
                        bool sepudo = false;

                        try
                        {

                            //obtener cupo 
                            string cupo = dataGridView1.Rows[x.Index].Cells[3].Value.ToString();
                            int nuevo_cupo = int.Parse(cupo);
                            if (nuevo_cupo >= 1)
                            {
                                //obtenemos clave de grupo para cambiar cupo
                                string clave_grupo = dataGridView1.Rows[x.Index].Cells[4].Value.ToString();
                                //obtenemos id de grupo
                                string id_grupo = dataGridView1.Rows[x.Index].Cells[0].Value.ToString();

                                int idGrupo = int.Parse(id_grupo);
                                //QUITAMOS UN CUPO
                                DAOGrupo dao = new DAOGrupo();

                                Grupo obj_grupo = dao.obtenerUno(idGrupo);
                                //vamos a insertarlo en las materias

                                DAOAlumnosgrupos alumgrupo = new DAOAlumnosgrupos();
                                bool y = alumgrupo.insertar(FrmPrincipal.NoControl, idGrupo);


                                obj_grupo.Cupo -= 1;
                                //actualizamos en la base de datos
                                bool check_grupo = dao.actualizar(obj_grupo);
                                //mostramos los nuevos resultados

                                sepudo = true;
                            }

                        }
                        catch (Exception ex) { }
                        //QUITAMOS CREDITOS AL ALUMNO 
                        int creditos_alum = int.Parse(valor);
                       
                        try
                        {

                            cont -= creditos_alum;
                            if (cont >= 0 && sepudo == true)
                            {
                                //ingresamos el nuevo valor de creditos alumnos
                                label2.Text = cont.ToString();
                                //vamos a inscribir al alumno
                                //cambio en bd de valor N a S 
                                DAOAlumno dao = new DAOAlumno();
                                Alumno alum = new Alumno();
                                bool check = dao.inscribir_alumno(FrmPrincipal.NoControl);

                            }
                            else
                            {
                                cont += creditos_alum;
                                MessageBox.Show("CREDITOS INSUFICIENTES O ALUMNO INSCRITO");
                                MessageBox.Show("CREDITOS ACTUALES "+ creditos_alum);
                            }

                        }
                        catch (ArithmeticException arit) { }

                    }

                }
                ConsultaCarga frm = new ConsultaCarga();

                frm.Show();
                this.Hide();
            }

            else
            {
                MessageBox.Show("No se puede inscribir porque los horarios chocan");
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
                DAOGrupo dao = new DAOGrupo();
                int clave_materia = dao.obtener_clave_materia(int.Parse(dataGridView1.Rows[index].Cells[0].Value.ToString()));
                string creditos = dataGridView1.Rows[index].Cells[6].Value.ToString();
                int creditos1 = int.Parse(creditos);
                if (A[clave_materia] != 1)
                {
                    dataGridView1.Rows[index].DefaultCellStyle.BackColor = Color.Orange;
                    //obtenemos valor del label 
                    int cont = int.Parse(label2.Text);
                    
                    label2.Text =(cont - creditos1).ToString();
                }
                else
                {
                    MessageBox.Show("ya se ha seleccionado antes");
                }
                //metodo cubeta para validar que no choquen horarios
                foreach (DataGridViewRow x in dataGridView1.Rows)
                {
                    int index1 = dataGridView1.SelectedRows[0].Index;
                    int pos = int.Parse(dataGridView1.Rows[index1].Cells[3].Value.ToString());
                }

                    A[clave_materia] = 1;
               
            }catch(Exception ex)
            {

            }
        }

        private void btn_quitar_Click(object sender, EventArgs e)
        {
            int index = dataGridView1.SelectedRows[0].Index;
            string horario = dataGridView1.Rows[index].Cells[5].Value.ToString();
            int horario_int = int.Parse(horario);
            int value = 1;
            bucket.Remove(horario_int);
            
            DAOGrupo dao = new DAOGrupo();
            int clave_materia = dao.obtener_clave_materia(int.Parse(dataGridView1.Rows[index].Cells[0].Value.ToString()));


            string creditos = dataGridView1.Rows[index].Cells[6].Value.ToString();
            int creditos1 = int.Parse(creditos);
            if (dataGridView1.Rows[index].DefaultCellStyle.BackColor == Color.Orange)
            {

                //obtenemos valor del label 
                int cont = int.Parse(label2.Text);
                label2.Text = (cont + creditos1).ToString();
                dataGridView1.Rows[index].DefaultCellStyle.BackColor = Color.White;

            }
            else
            {
                MessageBox.Show("NO TIENE SELECCIONADO UNA MATERIA VALIDA");
            }

            

            A[clave_materia] = 0;
        }
    }
}
