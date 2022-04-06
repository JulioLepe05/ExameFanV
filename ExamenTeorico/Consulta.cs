using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExamenTeorico
{
    
    public partial class Consulta : Form
    {

        Panel panel;
        public Consulta(string datos)
        {
            InitializeComponent();
            
        }

        public Consulta(string datos, FlowLayoutPanel p)
        {
            InitializeComponent();
            panel = p;

        }



        private void Consulta_Load(object sender, EventArgs e)
        {

        }

        //private void btnModificar_Click(object sender, EventArgs e)
        //{

        //}
        public void btnModificar_Click(object sender, EventArgs e)
        {
            this.Hide();
            panel.Controls.Clear();
            Datos Frm = new Datos();
            Frm.TopLevel = false;
            panel.Controls.Add(Frm);
            Frm.Show();

            //Form1 frm = new Form1();
            //this.Hide();
            ////Frm.TopLevel = false;
            ////Frm.TopLevel = false;
            ////frm.panelPrincipal.Controls.Add();
            //Frm.Show();

            Frm.btnNuevo.Text = "Guardar";


        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {

        }
    }
    
}
