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
    public partial class FrmListaMaterias : Form
    {
        public FrmListaMaterias()
        {
            InitializeComponent();
            cargarLista();
        }

        private void cargarLista() {
            DataTable resultado = new DAOMateria().obtenerTodas();
            dgvLista.DataSource = resultado;
            dgvLista.Columns[2].Visible = false;
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvLista.SelectedRows.Count > 0) {
                int idMateria = int.Parse(dgvLista.SelectedRows[0].Cells[0].Value.ToString());
                String nombre = dgvLista.SelectedRows[0].Cells[1].Value.ToString();
                String carrera = dgvLista.SelectedRows[0].Cells[4].Value.ToString();
                DialogResult respuesta = MessageBox.Show(this, "¿Estás seguro de que quieres eliminar la materia " +
                    nombre + " de la carrera de " + carrera + "?", "Eliminación de materia", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (respuesta == DialogResult.Yes)
                {
                    DAOMateria dao = new DAOMateria();
                    try
                    {
                        if (dao.eliminar(idMateria))
                        {
                            MessageBox.Show("La materia se ha eliminado correctamente");
                            cargarLista();
                        }

                    }
                    catch (ServerException ex)
                    {
                        MessageBox.Show(this, ex.Message, "Operación fallida", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch (FKException ex) {
                        MessageBox.Show(this, "No se puede eliminar esta materia debido a que hay grupos dados de alta", "Operación fallida", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch (Exception ex)
                    {
                            MessageBox.Show(this, "Ha ocurrido un error al realizar la operación", "Operación fallida", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        
                    }
                }
            }
            else {
                MessageBox.Show("Selecciona la materia que deseas eliminar");
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            
            

        }
    }
}
