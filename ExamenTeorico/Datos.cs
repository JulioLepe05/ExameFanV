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
        private static readonly Socket ClientSocket = new Socket//creamos un nuevo socket y se configura el socket, como el tipo de socket, y el protocolo que va a usar
            (AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        private const int PORT = 100;//creamos una constante que utilizamos como puerto

        string datos;
        string ip2;


        private void txtedad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '0')) // Solo permite el ingreso de numeros

            {
                e.Handled = true; //si recibe numeros los agrega al txt
            }
        }

        private void txtcodigo_TextChanged(object sender, EventArgs e)
        {
            
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
            
            //txtcodigo.Enabled = false; //desactiva el txt
            txtNombre.Enabled = false;
            txtapellidos.Enabled = false;
            txtedad.Enabled = false;
            txtnacionalidad.Enabled = false;
            txtGenero.Enabled = false;
            txtciudad.Enabled = false;
            txtestado.Enabled = false;
            txtuniversidad.Enabled = false;
            txtcarrera.Enabled = false;
            txtsemestre.Enabled = false;
            txtdeporte.Enabled = false;
            txtmain.Enabled = false;
            txtJugador.Enabled = false;
            txtCivil.Enabled = false;

            btnNuevo.Text = "Nuevo";//setea el nombre del boton
            

            txtcodigo.Clear();//limpia los textos
            txtNombre.Clear();
            txtapellidos.Clear();
            txtedad.Clear();
            txtnacionalidad.Clear();
            txtGenero.Clear();
            txtciudad.Clear();
            txtestado.Clear();
            txtuniversidad.Clear();
            txtcarrera.Clear();
            txtsemestre.Clear();
            txtdeporte.Clear();
            txtmain.Clear();
            txtJugador.Clear();
            txtCivil.Clear();
            SendRequest($"$Comando$%Desbloquear%{txtcodigo.Text}.txt");
        }

        public Datos(string ip, List<string> lista)//pasamos una lista 
        {
            InitializeComponent();
            Thread thread1 = new Thread(iniciar);//Creamos un hilo que ejecute el método iniciar, el cual es basciamente la conexión, esto es para que, tal y como en Java, no se congele el form.
            thread1.Start();//iniciamos el hilo
            ip2 = ip;//ip2 es la ip que cargamos de un lado a otro, le damos un scope global para quitarnos de problemas.


            if (lista != null)//si esta lista no está vacia, tomamos cada uno de los datos y se lo asignamos a cada textBox
            {
                txtcodigo.Text=lista[0];
                txtNombre.Text = lista[1];
                txtapellidos.Text = lista[2];
                txtedad.Text = lista[3];
                txtnacionalidad.Text = lista[4];
                txtGenero.Text = lista[5];
                txtciudad.Text = lista[6];
                txtestado.Text = lista[7];
                txtuniversidad.Text = lista[8];
                txtcarrera.Text = lista[9];
                txtsemestre.Text = lista[10];
                txtdeporte.Text = lista[11];
                txtmain.Text = lista[12];
                txtJugador.Text = lista[13];
                txtCivil.Text = lista[14];
            }
        }

        public void iniciar()
        {
            ConnectToServer(ip2);//la conexion al servidor
            RequestLoop();//matenemos el servidor en escucha

        }
        private static void ConnectToServer(string ip)//metodo de conexion
        {
            int attempts = 0;//iniciamos una variable attemps que sea igual a 0

            while (!ClientSocket.Connected)//mientras no haya una conexion al servidor
            {
                try
                {
                    attempts++;//intentamos conectarlo al servidor
                    ClientSocket.Connect(ip, PORT);//pasamos la ip del servidor y el puerto
                    //IPaddress junto con el loopback serian equivalente al localhost
                }
                catch (SocketException)
                {
                }
            }

        }
        private static void RequestLoop()//creamos el metdodo para mantener el servidor en escucha
        {

            while (true)//creamos un bucle infinito donde recibiremos la respuesta
            {
                ReceiveResponse();
            }
        }

        /// <summary>
        /// cerramos el socket y el programa
        /// </summary>
        private static void Exit()//cerramos el socket
        {
            SendString("exit"); //mandamos un string de exit
            ClientSocket.Shutdown(SocketShutdown.Both);//apagamos el socket
            ClientSocket.Close();
        }

        private static void SendRequest(string query)
        {
            
            SendString(query);

            if (query.ToLower() == "exit")//convertimos lo que se encuentra en la variable query a minusculas y si es igual a exit, hacemos lo siguiente
            {
                Exit();//cerramos el socket
            }
        }

        //buffer lo usamos para guardar informacion mientras lo enviamos de un lado o al otro
        private static void SendString(string text)//enviamos una string al server con una codificacion UTF8
        {
            //como enviamos esta cadena y no es un exit, entonces ejecutamos este metodo.


            byte[] buffer = Encoding.UTF8.GetBytes(text);//pasamos el string y lo guardamos e la variable buffer
            ClientSocket.Send(buffer, 0, buffer.Length, SocketFlags.None);//lo enviamos por el socket
        }

        private static void ReceiveResponse()//Creamos el metodo o funcion para recibir la respuesta
        {
            var buffer = new byte[2048];//creamos una variable tipo buffer y le asignamos un valor para su tamaño
            int received = ClientSocket.Receive(buffer, SocketFlags.None);//cantidad de bytes recibidos en el mensaje
            if (received == 0) return;//si no hay bytes en el mensaje, se regresa al bloque de codigo anterior
            var data = new byte[received];//guardamos la info con el tamaño de datos que se hayan recibido
            Array.Copy(buffer, data, received);//guardado de info en un array por si se llega a necesitar
            string text = Encoding.UTF8.GetString(data);//buffer a string
            
            

        }



        private void btnNuevo_Click(object sender, EventArgs e)
        {
            //txtcodigo.Enabled = true; //Activa el txt
            txtNombre.Enabled = true; //Activa el txt
            txtapellidos.Enabled = true; //Activa el txt
            txtedad.Enabled = true; //Activa el txt
            txtnacionalidad.Enabled = true; //Activa el txt
            txtGenero.Enabled = true; //Activa el txt
            txtciudad.Enabled = true; //Activa el txt
            txtestado.Enabled = true; //Activa el txt
            txtuniversidad.Enabled = true; //Activa el txt
            txtcarrera.Enabled = true; //Activa el txt
            txtsemestre.Enabled = true; //Activa el txt
            txtdeporte.Enabled = true; //Activa el txt
            txtmain.Enabled = true; //Activa el txt
            txtJugador.Enabled = true; //Activa el txt
            txtCivil.Enabled = true; //Activa el txt
            
            if (btnNuevo.Text=="Nuevo") //condicion de si este campo de texto esta vacio o nulo
            {
                Random r = new Random();//generan un numero aleatorio de 6 digitos
                txtcodigo.Text = r.Next(100000, 999999) + "";//dentro de este rango
            }

            if (btnNuevo.Text == "Guardar")
            {

                    if (txtNombre.Text == "" || txtNombre.Text.Contains("%"))//estas condiciones se encargan de validar que ningun campo este vacio
                    {
                        MessageBox.Show("Ingrese su nombre", "Aviso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);//muestra un cuadro de texto con una alerta
                        txtNombre.Focus();//manda el cursor donde se va a seleccionar
                        txtNombre.SelectAll();//selecciona todo dentro de ese campo de texto
                    }
                    else
                    {

                        if (txtapellidos.Text == "" || txtapellidos.Text.Contains("%"))
                        {
                            MessageBox.Show("Ingrese sus Apellidos", "Aviso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            txtapellidos.Focus();
                            txtapellidos.SelectAll();
                        }
                        else
                        {


                            if (txtedad.Text == "" || txtedad.Text.Contains("%"))
                            {
                                MessageBox.Show("Ingrese su edad", "Aviso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                txtedad.Focus();
                                txtedad.Select();
                            }
                            else
                            {


                                if (txtnacionalidad.Text == "" || txtnacionalidad.Text.Contains("%"))
                                {
                                    MessageBox.Show("Ingrese su Nacionalidad", "Aviso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                    txtnacionalidad.Focus();
                                    txtnacionalidad.SelectAll();

                                }
                                else
                                {

                                    if (txtGenero.Text == "" || txtGenero.Text.Contains("%"))
                                    {
                                        MessageBox.Show("Ingrese su Genero", "Aviso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                        txtGenero.Focus();
                                        txtGenero.SelectAll();
                                    }
                                    else
                                    {


                                        if (txtciudad.Text == "" || txtciudad.Text.Contains("%"))
                                        {
                                            MessageBox.Show("Ingrese su ciudad", "Aviso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                            txtciudad.Focus();
                                            txtciudad.SelectAll();
                                        }
                                        else
                                        {

                                            if (txtestado.Text == "" || txtestado.Text.Contains("%"))
                                            {
                                                MessageBox.Show("Ingrese su estado", "Aviso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                                txtestado.Focus();
                                                txtestado.SelectAll();
                                            }
                                            else
                                            {

                                                if (txtuniversidad.Text == "" || txtuniversidad.Text.Contains("%"))
                                                {
                                                    MessageBox.Show("Ingrese su Universidad", "Aviso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                                    txtuniversidad.Focus();
                                                    txtuniversidad.SelectAll();
                                                }
                                                else
                                                {


                                                    if (txtcarrera.Text == "" || txtcarrera.Text.Contains("%"))
                                                    {
                                                        MessageBox.Show("Ingrese su Carrera", "Aviso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                                        txtcarrera.Focus();
                                                        txtcarrera.SelectAll();
                                                    }
                                                    else
                                                    {

                                                        if (txtsemestre.Text == "" || txtsemestre.Text.Contains("%"))
                                                        {
                                                            MessageBox.Show("Ingrese su Semestre", "Aviso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                                            txtsemestre.Focus();
                                                            txtsemestre.SelectAll();
                                                        }
                                                        else
                                                        {

                                                            if (txtdeporte.Text == "" || txtdeporte.Text.Contains("%"))
                                                            {
                                                                MessageBox.Show("Ingrese su Deporte Favorito", "Aviso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                                                txtdeporte.Focus();
                                                                txtdeporte.SelectAll();
                                                            }
                                                            else
                                                            {

                                                                if (txtmain.Text == "" || txtmain.Text.Contains("%"))
                                                                {
                                                                    MessageBox.Show("Ingrese su Main Favorito", "Aviso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                                                    txtmain.Focus();
                                                                    txtmain.SelectAll();
                                                                }
                                                                else
                                                                {


                                                                    if (txtJugador.Text == "" || txtdeporte.Text.Contains("%"))
                                                                    {
                                                                        MessageBox.Show("Ingrese su Jugador Favorito de futbol", "Aviso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                                                        txtJugador.Focus();
                                                                        txtJugador.SelectAll();
                                                                    }
                                                                    else
                                                                    {

                                                                        if (txtCivil.Text == "" || txtCivil.Text.Contains("%"))
                                                                        {
                                                                            MessageBox.Show("Ingrese su Estado Civil", "Aviso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                                                            txtCivil.Focus();
                                                                            txtCivil.SelectAll();
                                                                        }
                                                                        else
                                                                        {
                                                                            //guardamos todos los datos obtenidos en un string
                                                                            datos = $"$Comando$%Escritura%{txtcodigo.Text}%{txtNombre.Text}%{txtapellidos.Text}%{txtedad.Text}%{txtnacionalidad.Text}%{txtGenero.Text}%{txtciudad.Text}%{txtestado.Text}%{txtuniversidad.Text}%{txtcarrera.Text}%{txtsemestre.Text}%{txtdeporte.Text}%{txtmain.Text}%{txtJugador.Text}%{txtCivil.Text}";

                                                                            MessageBox.Show("Datos Guardados con Exito","Aviso del Sistema",MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                                                        
                                                                        SendRequest(datos);//enviamos la cadena de texto con todos los datos, por medio de este metodo
                                                                        SendRequest($"$Comando$%Desbloquear%{txtcodigo.Text}.txt");
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

            
        }

       // private void button1_Click(object sender, EventArgs e)//Boton MOdificar
       // {
            

       //     txtcodigo.Enabled = true; //Activa el txt
       //     txtNombre.Enabled = true; //Activa el txt
       //     txtapellidos.Enabled = true; //Activa el txt
       //     txtedad.Enabled = true; //Activa el txt
       //     txtnacionalidad.Enabled = true; //Activa el txt
       //     txtGenero.Enabled = true; //Activa el txt
       //     txtciudad.Enabled = true; //Activa el txt
       //     txtestado.Enabled = true; //Activa el txt
       //     txtuniversidad.Enabled = true; //Activa el txt
       //     txtcarrera.Enabled = true; //Activa el txt
       //     txtsemestre.Enabled = true; //Activa el txt
       //     txtdeporte.Enabled = true; //Activa el txt
       //     txtmain.Enabled = true; //Activa el txt
       //     txtJugador.Enabled = true; //Activa el txt
       //     txtCivil.Enabled = true; //Activa el txt


       //     //btnModificar.Text = "Guardar";

       //     //if (btnModificar.Text == "Guardar")
       //     //{



       //     //    if (txtcodigo.Text.Length < 9)
       //     //    {
       //     //        MessageBox.Show("Ingrese un codigo con el formato correcto", "Aviso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
       //     //        txtcodigo.Focus();
       //     //        txtcodigo.SelectAll();
       //     //    }
       //     //    else if (txtcodigo.Text == "")
       //     //    {
       //     //        MessageBox.Show("Ingrese su Codigo", "Aviso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
       //     //        txtcodigo.Focus();
       //     //        txtcodigo.SelectAll();
       //     //    }
       //     //    else
       //     //    {
       //     //        // bool esNumero;
       //     //        // esNumero = int.TryParse(txtcodigo.Text, out codigo);

       //     //        if (txtNombre.Text == "")
       //     //        {
       //     //            MessageBox.Show("Ingrese su nombre", "Aviso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
       //     //            txtNombre.Focus();
       //     //            txtNombre.SelectAll();
       //     //        }
       //     //        else
       //     //        {
       //     //            // nombre = txtNombre.Text;

       //     //            if (txtapellidos.Text == "")
       //     //            {
       //     //                MessageBox.Show("Ingrese sus Apellidos", "Aviso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
       //     //                txtapellidos.Focus();
       //     //                txtapellidos.SelectAll();
       //     //            }
       //     //            else
       //     //            {
       //     //                //apellidos = txtapellidos.Text;


       //     //                if (txtedad.Text == "")
       //     //                {
       //     //                    MessageBox.Show("Ingrese su edad", "Aviso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
       //     //                    txtedad.Focus();
       //     //                    txtedad.Select();
       //     //                }
       //     //                else
       //     //                {
       //     //                    // bool esnum;
       //     //                    // esnum = int.TryParse(txtedad.Text, out edad);


       //     //                    if (txtnacionalidad.Text == "")
       //     //                    {
       //     //                        MessageBox.Show("Ingrese su Nacionalidad", "Aviso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
       //     //                        txtnacionalidad.Focus();
       //     //                        txtnacionalidad.SelectAll();

       //     //                    }
       //     //                    else
       //     //                    {
       //     //                        //nacionalidad = txtnacionalidad.Text;

       //     //                        if (txtGebero.Text == "")
       //     //                        {
       //     //                            MessageBox.Show("Ingrese su Genero", "Aviso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
       //     //                            txtGebero.Focus();
       //     //                            txtGebero.SelectAll();
       //     //                        }
       //     //                        else
       //     //                        {
       //     //                            // genero = txtGebero.Text;


       //     //                            if (txtciudad.Text == "")
       //     //                            {
       //     //                                MessageBox.Show("Ingrese su ciudad", "Aviso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
       //     //                                txtciudad.Focus();
       //     //                                txtciudad.SelectAll();
       //     //                            }
       //     //                            else
       //     //                            {
       //     //                                //ciudad = txtciudad.Text;

       //     //                                if (txtestado.Text == "")
       //     //                                {
       //     //                                    MessageBox.Show("Ingrese su estado", "Aviso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
       //     //                                    txtestado.Focus();
       //     //                                    txtestado.SelectAll();
       //     //                                }
       //     //                                else
       //     //                                {
       //     //                                    //estado = txtestado.Text;

       //     //                                    if (txtuniversidad.Text == "")
       //     //                                    {
       //     //                                        MessageBox.Show("Ingrese su Universidad", "Aviso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
       //     //                                        txtuniversidad.Focus();
       //     //                                        txtuniversidad.SelectAll();
       //     //                                    }
       //     //                                    else
       //     //                                    {
       //     //                                        //universidad = txtuniversidad.Text;


       //     //                                        if (txtcarrera.Text == "")
       //     //                                        {
       //     //                                            MessageBox.Show("Ingrese su Carrera", "Aviso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
       //     //                                            txtcarrera.Focus();
       //     //                                            txtcarrera.SelectAll();
       //     //                                        }
       //     //                                        else
       //     //                                        {
       //     //                                            //carrera = txtcarrera.Text;

       //     //                                            if (txtsemestre.Text == "")
       //     //                                            {
       //     //                                                MessageBox.Show("Ingrese su Semestre", "Aviso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
       //     //                                                txtsemestre.Focus();
       //     //                                                txtsemestre.SelectAll();
       //     //                                            }
       //     //                                            else
       //     //                                            {
       //     //                                                //bool esNum;
       //     //                                                //esNum = int.TryParse(txtsemestre.Text, out semestres);

       //     //                                                if (txtdeporte.Text == "")
       //     //                                                {
       //     //                                                    MessageBox.Show("Ingrese su Deporte Favorito", "Aviso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
       //     //                                                    txtdeporte.Focus();
       //     //                                                    txtdeporte.SelectAll();
       //     //                                                }
       //     //                                                else
       //     //                                                {
       //     //                                                    // deporte = txtdeporte.Text;

       //     //                                                    if (txtmain.Text == "")
       //     //                                                    {
       //     //                                                        MessageBox.Show("Ingrese su Main Favorito", "Aviso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
       //     //                                                        txtmain.Focus();
       //     //                                                        txtmain.SelectAll();
       //     //                                                    }
       //     //                                                    else
       //     //                                                    {
       //     //                                                        //main = txtmain.Text;

       //     //                                                        if (txtJugador.Text == "")
       //     //                                                        {
       //     //                                                            MessageBox.Show("Ingrese su Jugador Favorito de futbol", "Aviso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
       //     //                                                            txtJugador.Focus();
       //     //                                                            txtJugador.SelectAll();
       //     //                                                        }
       //     //                                                        else
       //     //                                                        {
       //     //                                                            //jugador = txtJugador.Text;

       //     //                                                            if (txtCivil.Text == "")
       //     //                                                            {
       //     //                                                                MessageBox.Show("Ingrese su Estado Civil", "Aviso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
       //     //                                                                txtCivil.Focus();
       //     //                                                                txtCivil.SelectAll();
       //     //                                                            }
       //     //                                                            else
       //     //                                                            {
       //     //                                                                //estadocivil = txtCivil.Text;
       //     //                                                                datos = $"{txtcodigo.Text}%{txtNombre.Text}%{txtapellidos.Text}%{txtedad.Text}%{txtnacionalidad.Text}%{txtGebero.Text}%{txtciudad.Text}%{txtestado.Text}%{txtuniversidad.Text}%{txtcarrera.Text}%{txtsemestre.Text}%{txtdeporte.Text}%{txtmain.Text}%{txtJugador.Text}%{txtCivil}";

       //     //                                                                MessageBox.Show("Datos Modificados con Exito", "Aviso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);


       //     //                                                            }
       //     //                                                        }
       //     //                                                    }

       //     //                                                }
       //     //                                            }
       //     //                                        }
       //     //                                    }


       //     //                                }
       //     //                            }

       //     //                        }
       //     //                    }
       //     //                }
       //     //            }
       //     //        }

       //     //    }
       //     //}
       //     //btnModificar.Text = "Guardar";            

       

       //}

       

        //private void btnEliminar_Click(object sender, EventArgs e)
        //{
        //    //una variable llamada pregunta, donde sacamos un mensaje 
        //    var pregunta= MessageBox.Show("¿Desea Eliminar los Datos?", "Aviso del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            
        //    //si pregunta es igual a la opcion seleccionada si
        //    if (pregunta.Equals(DialogResult.Yes))
        //    {
        //        txtcodigo.Clear();//limpiamos los textBox
        //        txtNombre.Clear();
        //        txtapellidos.Clear();
        //        txtedad.Clear();
        //        txtnacionalidad.Clear();
        //        txtGenero.Clear();
        //        txtciudad.Clear();
        //        txtestado.Clear();
        //        txtuniversidad.Clear();
        //        txtcarrera.Clear();
        //        txtsemestre.Clear();
        //        txtdeporte.Clear();
        //        txtmain.Clear();
        //        txtJugador.Clear();
        //        txtCivil.Clear();

        //        //retacamos todo a la variable tipo string, siendo el caso que ya estaria vacio
        //        datos = $"{txtcodigo.Text}%{txtNombre.Text}%{txtapellidos.Text}%{txtedad.Text}%{txtnacionalidad.Text}%{txtGenero.Text}%{txtciudad.Text}%{txtestado.Text}%{txtuniversidad.Text}%{txtcarrera.Text}%{txtsemestre.Text}%{txtdeporte.Text}%{txtmain.Text}%{txtJugador.Text}%{txtCivil.Text}";
        //    }
            

        //}
    }
            
}
