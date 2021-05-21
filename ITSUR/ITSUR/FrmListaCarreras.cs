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
    public partial class FrmListaCarreras : Form
    {

        public FrmListaCarreras()
        {
            InitializeComponent();
            actualizarTabla();
        }
            

        private void actualizarTabla()
        {
            DataTable resultado = new DAOCarrera().obtenerTodas();
            dgvListaCarreras.DataSource = resultado;
            dgvListaCarreras.Columns[2].Visible = false;
            
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            FrmCarrera form = new FrmCarrera();
            form.ShowDialog();
            actualizarTabla();
        }

        private void btnEditar_Click_1(object sender, EventArgs e)
        {
            if (dgvListaCarreras.SelectedRows.Count > 0)
            {
                String claveCarrera = dgvListaCarreras.SelectedRows[0].Cells[0].Value.ToString();
                FrmCarrera form = new FrmCarrera(claveCarrera);
                form.ShowDialog();
                actualizarTabla();
            }
            else
            {
                MessageBox.Show("Selecciona al alumno que deseas editar.");
            }
        }

        private void btnEliminar_Click_1(object sender, EventArgs e)
        {
            if (dgvListaCarreras.SelectedRows.Count > 0)
            {
                try
                {
                    String claveCarrera = dgvListaCarreras.SelectedRows[0].Cells[0].Value.ToString();
                    String nombre = dgvListaCarreras.SelectedRows[0].Cells[1].Value.ToString();
                    DialogResult respuesta = MessageBox.Show(this, "¿Estás seguro de que quieres eliminar" +
                        " la carrera " + nombre + " con clave " + claveCarrera + "?",
                        "Eliminación de carrera", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (respuesta == DialogResult.Yes)
                    {
                        DAOCarrera dao = new DAOCarrera();
                        if (dao.eliminar(claveCarrera))
                        {
                            MessageBox.Show("Carrera eliminada");
                            actualizarTabla();
                        }
                        else
                        {
                            MessageBox.Show("Error al eliminar");
                        }
                    }
                }
                catch (Exception ex)
                {
                    if (ex.Message.Equals("Padre"))
                    {
                        MessageBox.Show(this, "Esta carrera tiene datos relacionados, por ello no es posible eliminarla.", "Operación fallida", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show(this, "Ha ocurrido un error al realizar la operación", "Operación fallida", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Selecciona la carrera que deseas eliminar");
            }
        }

        private void FrmListaCarreras_Load(object sender, EventArgs e)
        {

        }
    }
}