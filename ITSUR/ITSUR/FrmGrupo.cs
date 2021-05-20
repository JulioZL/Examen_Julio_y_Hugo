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
using Modelos;
namespace ITSUR
{
    public partial class FrmGrupo : Form
    {
        private int idGrupo;
        
        public FrmGrupo()
        {
            InitializeComponent();
            this.Text = "Agregar nuevo grupo";
            
            cboCarrera.DataSource = new DAOCarrera().obtenerTodas();
            cboCarrera.DisplayMember = "Nombre";
            cboCarrera.ValueMember = "Clave";

            if (idGrupo == 0) {
                pnlId.Visible = false;
                cboCarrera.SelectedIndex = 0;
                cboMateria.DataSource = new DAOMateria().obtenerXCarrera(int.Parse(cboCarrera.SelectedValue.ToString()));
                cboMateria.DisplayMember = "Materia";
                cboMateria.ValueMember = "Id";
                this.cboCarrera.SelectedValueChanged += new System.EventHandler(this.cboCarrera_SelectedValueChanged);
                for (int i = 0; i < 5; i++)
                {
                    cklDias.SetItemChecked(i, true);
                }
                cboHorario.SelectedIndex = 0;
            }

        }

        public FrmGrupo(int idGrupo):this() {
            this.Text = "Editar grupo " + idGrupo;
            //Cargar el idGrupo en la variable local
            this.idGrupo = idGrupo;
            //Cargar la info del grupo
            DAOGrupo dao = new DAOGrupo();
            Grupo grupo = dao.obtenerUno(idGrupo);
            //Llenar los datos del grupo
            cboCarrera.SelectedValue = grupo.ClaveCarrera;
            cboMateria.DataSource = new DAOMateria().obtenerXCarrera(grupo.ClaveCarrera);
            cboMateria.DisplayMember = "Materia";
            cboMateria.ValueMember = "Id";
            cboMateria.SelectedValue = grupo.ClaveMateria;
            txtIdGrupo.Text = idGrupo + "";
            txtIdGrupo.Enabled = false;
            txtClaveGrupo.Text = grupo.ClaveGrupo;
            txtCupo.Text = grupo.Cupo + "";
            this.cboCarrera.SelectedValueChanged += new System.EventHandler(this.cboCarrera_SelectedValueChanged);
            cboHorario.SelectedItem = grupo.Horario+"";
            for (int i = 0; i < 5; i++)
            {
                if (grupo.Dias.Contains(cklDias.Items[i].ToString().Substring(0,3))){
                    cklDias.SetItemChecked(i, true);
                }
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (esValido())
            {
                Grupo obj = llenarDatos();
                //Crear una instancia del DAO
                DAOGrupo dao = new DAOGrupo();
                //Enviar el objeto grupo creado al método insertar del DAO 
                bool resultado;
                try
                {
                    //y obtener el resultado
                    if (idGrupo == 0)
                    {
                        //Es insertar
                        resultado = dao.insertar(obj);
                    }
                    else
                    {
                        //Es actualizar
                        resultado = dao.actualizar(obj);
                    }

                    //Cuando la operación se llevó a cabo de manera correcta
                    //envía un mensaje informando que se guardó y cierra el form
                    if (resultado)
                    {
                        MessageBox.Show(this, "Grupo almacenado exitósamente", "Operación exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                }
                catch (Exception ex) {
                    if (ex.Message.Equals("Duplicado")) {
                        MessageBox.Show(this, "La clave del grupo está duplicada", "Operación fallida", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else{
                        MessageBox.Show(this, "Ha ocurrido un error al realizar la operación", "Operación fallida", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    
                }
            }
            else {
                MessageBox.Show(this,"Uno o varios de los datos del grupo no son válidos, revisa la información y vuelve a intentar", "Datos no válidos",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
        }


        private Grupo llenarDatos() {
            Grupo grupo = new Grupo();
            grupo.Id = idGrupo;
            grupo.ClaveGrupo = txtClaveGrupo.Text.Trim();
            grupo.ClaveMateria = int.Parse(cboMateria.SelectedValue.ToString());
            grupo.Cupo= Byte.Parse(txtCupo.Text);
            String dias = "";
            for (int i = 0; i < cklDias.CheckedItems.Count; i++)
            {
                dias += (cklDias.CheckedItems[i]+"").Substring(0,3) + ",";
            }
            dias = dias.Substring(0, dias.Length - 1);
            grupo.Dias = dias;
            grupo.Horario = Byte.Parse(cboHorario.SelectedItem.ToString());
            return grupo;
        }

        /// <summary>
        /// Verifica que cada dato proporcionado en el formulario
        /// esté completo y correcto
        /// </summary>
        /// <returns>true en caso que todo sea válido, false en caso contrario</returns>
        private bool esValido() {
            //TODO: Verifica cada dato proporcionado en el formulario
            //para asegurarse que los datos estén completos y correctos
            //Hacer coincidir con la BD
            //Trabajar con error provider

            //TODO: Cambiar con la respuesta adecuada
            return true;
        }

        private void cboCarrera_SelectedValueChanged(object sender, EventArgs e)
        {
            cboMateria.DataSource = new DAOMateria().obtenerXCarrera(int.Parse(cboCarrera.SelectedValue.ToString()));
            cboMateria.DisplayMember = "Materia";
            cboMateria.ValueMember = "Id";
        }
    }
}
