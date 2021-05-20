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
    public partial class FrmListaGrupos : Form
    {
        public FrmListaGrupos()
        {
            MessageBox.Show("El tipo de usuario es "  + FrmPrincipal.TipoUsuario);
            InitializeComponent();
            cargarLista();
        }

        private void cargarLista() {
            DataTable resultado = new DAOGrupo().obtenerTodos();
            dgvLista.DataSource = resultado;
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            FrmGrupo form = new FrmGrupo();
            form.ShowDialog();
            cargarLista();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvLista.SelectedRows.Count > 0) {
                int id= int.Parse(dgvLista.SelectedRows[0].Cells[0].Value.ToString());
                String claveGrupo = dgvLista.SelectedRows[0].Cells[1].Value.ToString();
                String materia = dgvLista.SelectedRows[0].Cells[2].Value.ToString();
                String carrera = dgvLista.SelectedRows[0].Cells[3].Value.ToString();
                DialogResult respuesta = MessageBox.Show(this, "¿Estás seguro de que quieres eliminar al grupo con clave " +
                    claveGrupo + " de la materia " + materia + " para la carrera " + carrera + "?", "Eliminación de grupo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (respuesta == DialogResult.Yes)
                {
                    DAOGrupo dao = new DAOGrupo();
                    if (dao.eliminar(id))
                    {
                        MessageBox.Show("Grupo eliminado");
                        cargarLista();
                    }
                    else
                    {
                        MessageBox.Show("Error al eliminar");
                    }
                }
            }
            else {
                MessageBox.Show("Selecciona el grupo que deseas eliminar");
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvLista.SelectedRows.Count > 0)
            {
                int idGrupo = int.Parse(dgvLista.SelectedRows[0].Cells[0].Value.ToString());
                FrmGrupo form = new FrmGrupo(idGrupo);
                form.ShowDialog();
                cargarLista();
            }
            else
            {
                MessageBox.Show("Selecciona el grupo que deseas editar");
            }

            

        }
    }
}
