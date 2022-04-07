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
        public string ip;
        
        public Form1()
        {
            InitializeComponent();
        }

        private void btnNuevo_Click(object sender, EventArgs e)//declaramos un boton con nombre nuevo nuevesin
        {
            
            

            this.panelPrincipal.Controls.Clear();//limpia el panel


            Datos Frm = new Datos(ip, null);//creamos una nueva instancia donde llamamos el frame datos
            Frm.TopLevel = false;//Profundidad del frame.
            panelPrincipal.Controls.Add(Frm);//añadimos lo que queremos al panel del frame
            Frm.Show();//mostramos el frame de datos


        }

        

        public void panelPrincipal_Paint( object sender, PaintEventArgs e)
        {
            //Este evento para que lo hicieron? @lepe @Doni?
        }
       
        private void btnConsulta_Click(object sender, EventArgs e)//codigo del boton consultar
        {
            this.panelPrincipal.Controls.Clear();//limpiamos el panel

            Consulta frmconsulta = new Consulta(ip,x, this.panelPrincipal);//creamos una instancia para llamar el frame de consulta
            frmconsulta.TopLevel = false;
            panelPrincipal.Controls.Add(frmconsulta);//añadimos lo que queramos al panel
            frmconsulta.Show();//lo mostramos

        }

        private void button5_Click(object sender, EventArgs e)
        {
            Application.Exit();//cerramos la app
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            ip = txtIP.Text;
            btnConsulta.Enabled = true;
            btnNuevo.Enabled = true;
        }
    }

   

}
