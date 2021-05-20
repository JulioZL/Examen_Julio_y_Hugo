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
namespace ITSUR
{
    public partial class FrmListaAlumnos : Form
    {
        public FrmListaAlumnos()
        {
            InitializeComponent();
            cargarLista();
        }

        private void cargarLista() {
            DataTable resultado = new DAOAlumno().obtenerTodos();
            dgvLista.DataSource = resultado;
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            FrmAlumno form = new FrmAlumno();
            form.ShowDialog();
            cargarLista();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvLista.SelectedRows.Count > 0) {
                String noControl = dgvLista.SelectedRows[0].Cells[0].Value.ToString();
                String nombre = dgvLista.SelectedRows[0].Cells[1].Value.ToString();
                DialogResult respuesta = MessageBox.Show(this, "¿Estás seguro de que quieres eliminar al alumno " +
                    nombre + " con número de control " + noControl + "?", "Eliminación de alumno", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (respuesta == DialogResult.Yes)
                {
                    DAOAlumno dao = new DAOAlumno();
                    if (dao.eliminar(noControl))
                    {
                        MessageBox.Show("Alumno eliminado");
                        cargarLista();
                    }
                    else
                    {
                        MessageBox.Show("Error al eliminar");
                    }
                }
            }
            else {
                MessageBox.Show("Selecciona al alumno que deseas eliminar");
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvLista.SelectedRows.Count > 0)
            {
                String noControl = dgvLista.SelectedRows[0].Cells[0].Value.ToString();
                FrmAlumno form = new FrmAlumno(noControl);
                form.ShowDialog();
                cargarLista();
            }
            else
            {
                MessageBox.Show("Selecciona al alumno que deseas editar");
            }

            

        }
    }
}
