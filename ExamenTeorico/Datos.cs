using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;
using System.Threading;

namespace ExamenTeorico
{
    public partial class Datos : Form
    {
        private static readonly Socket ClientSocket = new Socket
            (AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        private const int PORT = 100;

        string datos;

        //int codigo, edad, semestres;
        //string nombre, apellidos, nacionalidad, genero, ciudad, estado, universidad, carrera, deporte, main, jugador, estadocivil;

        private void txtedad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '0')) // Solo permite el ingreso de numeros

            {
                e.Handled = true; //si recibe numeros los agrega al txt
            }
        }

        private void txtcodigo_TextChanged(object sender, EventArgs e)
        {
            
                //txtcodigo.ForeColor = Color.Black;
            
        }

        private void txtcodigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '0')) // Solo permite el ingreso de numeros

            {
                e.Handled = true; //si recibe numeros los agrega al txt
            }
        }

        private void txtsemestre_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '0')) // Solo permite el ingreso de numeros

            {
                e.Handled = true; //si recibe numeros los agrega al txt
            }
        }

        private void btncancelar_Click(object sender, EventArgs e)
        {
            
            txtcodigo.Enabled = false; //desactiva el txt
            txtNombre.Enabled = false;
            txtapellidos.Enabled = false;
            txtedad.Enabled = false;
            txtnacionalidad.Enabled = false;
            txtGebero.Enabled = false;
            txtciudad.Enabled = false;
            txtestado.Enabled = false;
            txtuniversidad.Enabled = false;
            txtcarrera.Enabled = false;
            txtsemestre.Enabled = false;
            txtdeporte.Enabled = false;
            txtmain.Enabled = false;
            txtJugador.Enabled = false;
            txtCivil.Enabled = false;

            btnNuevo.Text = "Nuevo";
            

            txtcodigo.Clear();
            txtNombre.Clear();
            txtapellidos.Clear();
            txtedad.Clear();
            txtnacionalidad.Clear();
            txtGebero.Clear();
            txtciudad.Clear();
            txtestado.Clear();
            txtuniversidad.Clear();
            txtcarrera.Clear();
            txtsemestre.Clear();
            txtdeporte.Clear();
            txtmain.Clear();
            txtJugador.Clear();
            txtCivil.Clear();
        }

        public Datos()
        {
            InitializeComponent();
            Thread thread1 = new Thread(iniciar);
            thread1.Start();
        }

        public void iniciar()
        {
            ConnectToServer();
            RequestLoop();

        }
        private static void ConnectToServer()
        {
            int attempts = 0;

            while (!ClientSocket.Connected)
            {
                try
                {
                    attempts++;
                    ClientSocket.Connect(IPAddress.Loopback, PORT);
                }
                catch (SocketException)
                {
                }
            }

        }
        private static void RequestLoop()
        {

            while (true)
            {
                ReceiveResponse();
            }
        }

        /// <summary>
        /// Close socket and exit program.
        /// </summary>
        private static void Exit()
        {
            SendString("exit"); // Tell the server we are exiting
            ClientSocket.Shutdown(SocketShutdown.Both);
            ClientSocket.Close();
            Environment.Exit(0);
        }

        private static void SendRequest(string query)
        {
            //Console.Write("Send a request: ");
            //string request = Console.ReadLine();
            SendString(query);

            if (query.ToLower() == "exit")
            {
                Exit();
            }
        }

        /// <summary>
        /// Sends a string to the server with ASCII encoding.
        /// </summary>
        private static void SendString(string text)
        {
            byte[] buffer = Encoding.ASCII.GetBytes(text);
            ClientSocket.Send(buffer, 0, buffer.Length, SocketFlags.None);
        }

        private static void ReceiveResponse()
        {
            var buffer = new byte[2048];
            int received = ClientSocket.Receive(buffer, SocketFlags.None);
            if (received == 0) return;
            var data = new byte[received];
            Array.Copy(buffer, data, received);
            string text = Encoding.ASCII.GetString(data);
            
            

        }



        private void btnNuevo_Click(object sender, EventArgs e)
        {
            txtcodigo.Enabled = true; //Activa el txt
            txtNombre.Enabled = true; //Activa el txt
            txtapellidos.Enabled = true; //Activa el txt
            txtedad.Enabled = true; //Activa el txt
            txtnacionalidad.Enabled = true; //Activa el txt
            txtGebero.Enabled = true; //Activa el txt
            txtciudad.Enabled = true; //Activa el txt
            txtestado.Enabled = true; //Activa el txt
            txtuniversidad.Enabled = true; //Activa el txt
            txtcarrera.Enabled = true; //Activa el txt
            txtsemestre.Enabled = true; //Activa el txt
            txtdeporte.Enabled = true; //Activa el txt
            txtmain.Enabled = true; //Activa el txt
            txtJugador.Enabled = true; //Activa el txt
            txtCivil.Enabled = true; //Activa el txt
            
            if (txtcodigo == null)
            {
                Random r = new Random();
                txtcodigo.Text = r.Next(100000, 999999) + "";
            }

            if (btnNuevo.Text == "Guardar")
            {
                


                if (txtcodigo.Text.Length < 9)
                {
                    MessageBox.Show("Ingrese un codigo con el formato correcto", "Aviso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtcodigo.Focus();
                    txtcodigo.SelectAll();
                }
                else if (txtcodigo.Text == "")
                {
                    MessageBox.Show("Ingrese su Codigo", "Aviso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtcodigo.Focus();
                    txtcodigo.SelectAll();
                }
                else
                {
                   // bool esNumero;
                   // esNumero = int.TryParse(txtcodigo.Text, out codigo);

                    if (txtNombre.Text == "")
                    {
                        MessageBox.Show("Ingrese su nombre", "Aviso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtNombre.Focus();
                        txtNombre.SelectAll();
                    }
                    else
                    {
                       // nombre = txtNombre.Text;

                        if (txtapellidos.Text == "")
                        {
                            MessageBox.Show("Ingrese sus Apellidos", "Aviso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            txtapellidos.Focus();
                            txtapellidos.SelectAll();
                        }
                        else
                        {
                            //apellidos = txtapellidos.Text;


                            if (txtedad.Text == "")
                            {
                                MessageBox.Show("Ingrese su edad", "Aviso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                txtedad.Focus();
                                txtedad.Select();
                            }
                            else
                            {
                               // bool esnum;
                               // esnum = int.TryParse(txtedad.Text, out edad);


                                if (txtnacionalidad.Text == "")
                                {
                                    MessageBox.Show("Ingrese su Nacionalidad", "Aviso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                    txtnacionalidad.Focus();
                                    txtnacionalidad.SelectAll();

                                }
                                else
                                {
                                    //nacionalidad = txtnacionalidad.Text;

                                    if (txtGebero.Text == "")
                                    {
                                        MessageBox.Show("Ingrese su Genero", "Aviso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                        txtGebero.Focus();
                                        txtGebero.SelectAll();
                                    }
                                    else
                                    {
                                       // genero = txtGebero.Text;


                                        if (txtciudad.Text == "")
                                        {
                                            MessageBox.Show("Ingrese su ciudad", "Aviso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                            txtciudad.Focus();
                                            txtciudad.SelectAll();
                                        }
                                        else
                                        {
                                            //ciudad = txtciudad.Text;

                                            if (txtestado.Text == "")
                                            {
                                                MessageBox.Show("Ingrese su estado", "Aviso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                                txtestado.Focus();
                                                txtestado.SelectAll();
                                            }
                                            else
                                            {
                                                //estado = txtestado.Text;

                                                if (txtuniversidad.Text == "")
                                                {
                                                    MessageBox.Show("Ingrese su Universidad", "Aviso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                                    txtuniversidad.Focus();
                                                    txtuniversidad.SelectAll();
                                                }
                                                else
                                                {
                                                    //universidad = txtuniversidad.Text;


                                                    if (txtcarrera.Text == "")
                                                    {
                                                        MessageBox.Show("Ingrese su Carrera", "Aviso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                                        txtcarrera.Focus();
                                                        txtcarrera.SelectAll();
                                                    }
                                                    else
                                                    {
                                                        //carrera = txtcarrera.Text;

                                                        if (txtsemestre.Text == "")
                                                        {
                                                            MessageBox.Show("Ingrese su Semestre", "Aviso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                                            txtsemestre.Focus();
                                                            txtsemestre.SelectAll();
                                                        }
                                                        else
                                                        {
                                                            //bool esNum;
                                                            //esNum = int.TryParse(txtsemestre.Text, out semestres);

                                                            if (txtdeporte.Text == "")
                                                            {
                                                                MessageBox.Show("Ingrese su Deporte Favorito", "Aviso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                                                txtdeporte.Focus();
                                                                txtdeporte.SelectAll();
                                                            }
                                                            else
                                                            {
                                                               // deporte = txtdeporte.Text;

                                                                if (txtmain.Text == "")
                                                                {
                                                                    MessageBox.Show("Ingrese su Main Favorito", "Aviso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                                                    txtmain.Focus();
                                                                    txtmain.SelectAll();
                                                                }
                                                                else
                                                                {
                                                                    //main = txtmain.Text;

                                                                    if (txtJugador.Text == "")
                                                                    {
                                                                        MessageBox.Show("Ingrese su Jugador Favorito de futbol", "Aviso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                                                        txtJugador.Focus();
                                                                        txtJugador.SelectAll();
                                                                    }
                                                                    else
                                                                    {
                                                                        //jugador = txtJugador.Text;

                                                                        if (txtCivil.Text == "")
                                                                        {
                                                                            MessageBox.Show("Ingrese su Estado Civil", "Aviso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                                                            txtCivil.Focus();
                                                                            txtCivil.SelectAll();
                                                                        }
                                                                        else
                                                                        {
                                                                            //estadocivil = txtCivil.Text;
                                                                            datos = $"{txtcodigo.Text}%{txtNombre.Text}%{txtapellidos.Text}%{txtedad.Text}%{txtnacionalidad.Text}%{txtGebero.Text}%{txtciudad.Text}%{txtestado.Text}%{txtuniversidad.Text}%{txtcarrera.Text}%{txtsemestre.Text}%{txtdeporte.Text}%{txtmain.Text}%{txtJugador.Text}%{txtCivil.Text}";

                                                                            MessageBox.Show("Datos Guardados con Exito","Aviso del Sistema",MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                                                            SendRequest(datos);
                                                                        }
                                                                    }
                                                                }

                                                            }
                                                        }
                                                    }
                                                }


                                            }
                                        }

                                    }
                                }
                            }
                        }
                    }

                }
            }
            btnNuevo.Text = "Guardar"; //Cambia el texto del boton "Nuevo" a "Guardar"



            

            Consulta frmConsulta = new Consulta(datos);
            
        }

        private void button1_Click(object sender, EventArgs e)//Boton MOdificar
        {
            

            txtcodigo.Enabled = true; //Activa el txt
            txtNombre.Enabled = true; //Activa el txt
            txtapellidos.Enabled = true; //Activa el txt
            txtedad.Enabled = true; //Activa el txt
            txtnacionalidad.Enabled = true; //Activa el txt
            txtGebero.Enabled = true; //Activa el txt
            txtciudad.Enabled = true; //Activa el txt
            txtestado.Enabled = true; //Activa el txt
            txtuniversidad.Enabled = true; //Activa el txt
            txtcarrera.Enabled = true; //Activa el txt
            txtsemestre.Enabled = true; //Activa el txt
            txtdeporte.Enabled = true; //Activa el txt
            txtmain.Enabled = true; //Activa el txt
            txtJugador.Enabled = true; //Activa el txt
            txtCivil.Enabled = true; //Activa el txt


            //btnModificar.Text = "Guardar";

            //if (btnModificar.Text == "Guardar")
            //{



            //    if (txtcodigo.Text.Length < 9)
            //    {
            //        MessageBox.Show("Ingrese un codigo con el formato correcto", "Aviso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //        txtcodigo.Focus();
            //        txtcodigo.SelectAll();
            //    }
            //    else if (txtcodigo.Text == "")
            //    {
            //        MessageBox.Show("Ingrese su Codigo", "Aviso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //        txtcodigo.Focus();
            //        txtcodigo.SelectAll();
            //    }
            //    else
            //    {
            //        // bool esNumero;
            //        // esNumero = int.TryParse(txtcodigo.Text, out codigo);

            //        if (txtNombre.Text == "")
            //        {
            //            MessageBox.Show("Ingrese su nombre", "Aviso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //            txtNombre.Focus();
            //            txtNombre.SelectAll();
            //        }
            //        else
            //        {
            //            // nombre = txtNombre.Text;

            //            if (txtapellidos.Text == "")
            //            {
            //                MessageBox.Show("Ingrese sus Apellidos", "Aviso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //                txtapellidos.Focus();
            //                txtapellidos.SelectAll();
            //            }
            //            else
            //            {
            //                //apellidos = txtapellidos.Text;


            //                if (txtedad.Text == "")
            //                {
            //                    MessageBox.Show("Ingrese su edad", "Aviso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //                    txtedad.Focus();
            //                    txtedad.Select();
            //                }
            //                else
            //                {
            //                    // bool esnum;
            //                    // esnum = int.TryParse(txtedad.Text, out edad);


            //                    if (txtnacionalidad.Text == "")
            //                    {
            //                        MessageBox.Show("Ingrese su Nacionalidad", "Aviso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //                        txtnacionalidad.Focus();
            //                        txtnacionalidad.SelectAll();

            //                    }
            //                    else
            //                    {
            //                        //nacionalidad = txtnacionalidad.Text;

            //                        if (txtGebero.Text == "")
            //                        {
            //                            MessageBox.Show("Ingrese su Genero", "Aviso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //                            txtGebero.Focus();
            //                            txtGebero.SelectAll();
            //                        }
            //                        else
            //                        {
            //                            // genero = txtGebero.Text;


            //                            if (txtciudad.Text == "")
            //                            {
            //                                MessageBox.Show("Ingrese su ciudad", "Aviso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //                                txtciudad.Focus();
            //                                txtciudad.SelectAll();
            //                            }
            //                            else
            //                            {
            //                                //ciudad = txtciudad.Text;

            //                                if (txtestado.Text == "")
            //                                {
            //                                    MessageBox.Show("Ingrese su estado", "Aviso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //                                    txtestado.Focus();
            //                                    txtestado.SelectAll();
            //                                }
            //                                else
            //                                {
            //                                    //estado = txtestado.Text;

            //                                    if (txtuniversidad.Text == "")
            //                                    {
            //                                        MessageBox.Show("Ingrese su Universidad", "Aviso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //                                        txtuniversidad.Focus();
            //                                        txtuniversidad.SelectAll();
            //                                    }
            //                                    else
            //                                    {
            //                                        //universidad = txtuniversidad.Text;


            //                                        if (txtcarrera.Text == "")
            //                                        {
            //                                            MessageBox.Show("Ingrese su Carrera", "Aviso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //                                            txtcarrera.Focus();
            //                                            txtcarrera.SelectAll();
            //                                        }
            //                                        else
            //                                        {
            //                                            //carrera = txtcarrera.Text;

            //                                            if (txtsemestre.Text == "")
            //                                            {
            //                                                MessageBox.Show("Ingrese su Semestre", "Aviso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //                                                txtsemestre.Focus();
            //                                                txtsemestre.SelectAll();
            //                                            }
            //                                            else
            //                                            {
            //                                                //bool esNum;
            //                                                //esNum = int.TryParse(txtsemestre.Text, out semestres);

            //                                                if (txtdeporte.Text == "")
            //                                                {
            //                                                    MessageBox.Show("Ingrese su Deporte Favorito", "Aviso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //                                                    txtdeporte.Focus();
            //                                                    txtdeporte.SelectAll();
            //                                                }
            //                                                else
            //                                                {
            //                                                    // deporte = txtdeporte.Text;

            //                                                    if (txtmain.Text == "")
            //                                                    {
            //                                                        MessageBox.Show("Ingrese su Main Favorito", "Aviso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //                                                        txtmain.Focus();
            //                                                        txtmain.SelectAll();
            //                                                    }
            //                                                    else
            //                                                    {
            //                                                        //main = txtmain.Text;

            //                                                        if (txtJugador.Text == "")
            //                                                        {
            //                                                            MessageBox.Show("Ingrese su Jugador Favorito de futbol", "Aviso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //                                                            txtJugador.Focus();
            //                                                            txtJugador.SelectAll();
            //                                                        }
            //                                                        else
            //                                                        {
            //                                                            //jugador = txtJugador.Text;

            //                                                            if (txtCivil.Text == "")
            //                                                            {
            //                                                                MessageBox.Show("Ingrese su Estado Civil", "Aviso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //                                                                txtCivil.Focus();
            //                                                                txtCivil.SelectAll();
            //                                                            }
            //                                                            else
            //                                                            {
            //                                                                //estadocivil = txtCivil.Text;
            //                                                                datos = $"{txtcodigo.Text}%{txtNombre.Text}%{txtapellidos.Text}%{txtedad.Text}%{txtnacionalidad.Text}%{txtGebero.Text}%{txtciudad.Text}%{txtestado.Text}%{txtuniversidad.Text}%{txtcarrera.Text}%{txtsemestre.Text}%{txtdeporte.Text}%{txtmain.Text}%{txtJugador.Text}%{txtCivil}";

            //                                                                MessageBox.Show("Datos Modificados con Exito", "Aviso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);


            //                                                            }
            //                                                        }
            //                                                    }

            //                                                }
            //                                            }
            //                                        }
            //                                    }


            //                                }
            //                            }

            //                        }
            //                    }
            //                }
            //            }
            //        }

            //    }
            //}
            //btnModificar.Text = "Guardar";            

            Consulta frmConsulta = new Consulta(datos);

        }

       

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            
            var pregunta= MessageBox.Show("¿Desea Eliminar los Datos?", "Aviso del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            
            if (pregunta.Equals(DialogResult.Yes))
            {
                txtcodigo.Clear();
                txtNombre.Clear();
                txtapellidos.Clear();
                txtedad.Clear();
                txtnacionalidad.Clear();
                txtGebero.Clear();
                txtciudad.Clear();
                txtestado.Clear();
                txtuniversidad.Clear();
                txtcarrera.Clear();
                txtsemestre.Clear();
                txtdeporte.Clear();
                txtmain.Clear();
                txtJugador.Clear();
                txtCivil.Clear();

                datos = $"{txtcodigo.Text}%{txtNombre.Text}%{txtapellidos.Text}%{txtedad.Text}%{txtnacionalidad.Text}%{txtGebero.Text}%{txtciudad.Text}%{txtestado.Text}%{txtuniversidad.Text}%{txtcarrera.Text}%{txtsemestre.Text}%{txtdeporte.Text}%{txtmain.Text}%{txtJugador.Text}%{txtCivil.Text}";
            }
            

        }
    }
            
}
