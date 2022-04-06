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
    public partial class Form1 : Form
    {
        string x;
        
        public Form1()
        {
            InitializeComponent();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            
            

            this.panelPrincipal.Controls.Clear();


            Datos Frm = new Datos();
            Frm.TopLevel = false;
            panelPrincipal.Controls.Add(Frm);
            Frm.Show();

        }

        

        public void panelPrincipal_Paint( object sender, PaintEventArgs e)
        {

        }
        public void tamalito(object sender, PaintEventArgs e)
        {
            this.panelPrincipal.Controls.Clear();


            Datos Frm = new Datos();
            Frm.TopLevel = false;
            panelPrincipal.Controls.Add(Frm);
            Frm.Show();

        }
        private void btnConsulta_Click(object sender, EventArgs e)
        {
            this.panelPrincipal.Controls.Clear();

            Consulta frmconsulta = new Consulta(x, this.panelPrincipal);
            frmconsulta.TopLevel = false;
            panelPrincipal.Controls.Add(frmconsulta);
            frmconsulta.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }

   

}
