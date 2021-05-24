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
    public partial class ConsultaCarga : Form
    {
        public ConsultaCarga()
        {
            InitializeComponent();
            DAOGrupo dao = new DAOGrupo();
            dataGridView1.DataSource = dao.obtener_carga(FrmPrincipal.NoControl);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void ConsultaCarga_Load(object sender, EventArgs e)
        {

        }
    }
}
