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
    
    public partial class Consulta : Form
    {
        private static readonly Socket ClientSocket = new Socket//creamos un nuevo socket y se configura el socket, como el tipo de socket, y el protocolo que va a usar
            (AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        private const int PORT = 100;//creamos una constante que utilizamos como puerto
        string ip2;//Ip que nos traemos de otros frames

        Panel panel;
        public Consulta(string datos)//constructor con datos nomás, ni idea de porque sigue aquí porque el código jamas entra por aquí o_0, 
        {
            InitializeComponent();
            
        }
        public void iniciar()//creamos un metodo iniciar
        {
            ConnectToServer();//donde mandamos llamaar el metodo para conectar al servidor
            SendRequest("$Comando$%ActualizarCB");//le estamos enviando un comando al servidor para que actualice el combo box y que muestre los archivos que tenmos guardados
            RequestLoop(cbxListado, dgvShow,dgv4Cols);//loop para recibir las respuestas para el combo box y la tabla

        }
        private static void ConnectToServer()//creamos un metodo para conectarnos al servidor
        {
            int attempts = 0;//inciamos una variable con un valor de 0

            while (!ClientSocket.Connected)//mientras el socket de cliente no este conectado
            {
                try
                {
                    attempts++;
                    //intentamos conectarlo al servidor
                    ClientSocket.Connect(IPAddress.Loopback, PORT);//pasamos la ip del servidor y el puerto
                    //IPaddress junto con el loopback serian equivalente al localhost
                }
                catch (SocketException)
                {
                    //Console.Clear();
                }
            }

            //Console.Clear();
            //Console.WriteLine("Connected");
        }

        private static void RequestLoop(ComboBox cb, DataGridView dgvShow, DataGridView dgv4cols)//creamos un metodo para que siempre este recibiendo la informacion
        {
            //Console.WriteLine(@"<Type ""exit"" to properly disconnect client>");

            while (true)
            {
                //SendRequest();
                ReceiveResponse(cb, dgvShow, dgv4cols);
            }
        }

        /// <summary>
        /// cerramos el socket
        /// </summary>
        private static void Exit()//cerramos el socket
        {
            SendString("exit"); //mandamos un string de exit
            ClientSocket.Shutdown(SocketShutdown.Both);//apagamos el socket
            ClientSocket.Close();
        }

        private static void SendRequest(string query)
        {
            //Console.Write("Send a request: ");
            //string request = Console.ReadLine();
            SendString(query);

            if (query.ToLower() == "exit")//convertimos lo que se encuentra en la variable query a minusculas y si es igual a exit, hacemos lo siguiente
            {
                Exit();//cerramos el socket
            }
        }

        /// <summary>
        /// Enviamos una cadena al servidor con una codificacion UTF8
        /// </summary>
        
        //buffer lo usamos para guardar informacion mientras lo enviamos de un lado o al otro
        private static void SendString(string text)//enviamos una string al server con una codificacion UTF8
        {
            //como enviamos esta cadena y no es un exit, entonces ejecutamos este metodo.


            byte[] buffer = Encoding.UTF8.GetBytes(text);//pasamos el string y lo guardamos e la variable buffer
            ClientSocket.Send(buffer, 0, buffer.Length, SocketFlags.None);//lo enviamos por el socket
        }

        private static void ReceiveResponse(ComboBox cb, DataGridView dgvShow, DataGridView dgv4cols)//recibimos la respuesta que se ira al cb y al data grid
        {
            var buffer = new byte[2048];//creamos una variable tipo buffer y le asignamos un valor para su tamaño
            int received = ClientSocket.Receive(buffer, SocketFlags.None);//cantidad de bytes recibidos en el mensaje
            if (received == 0) return;//si no hay bytes en el mensaje, se regresa al bloque de codigo anterior
            var data = new byte[received];//guardamos la info con el tamaño de datos que se hayan recibido
            Array.Copy(buffer, data, received);//guardado de info en un array por si se llega a necesitar
            string text = Encoding.UTF8.GetString(data);//buffer a string


            string[] conceptos = text.ToString().Split('$');//creamos una lista, guardamos la informacion recibida, con esta llavecita, nos va a separar 
            //los comandos de los textos
            switch (conceptos[0])//comparamos si en la posicion 0 del array esta la palabra broadcast
            {
                case "BROADCAST":
                    //si si, usaremos el switch de si en la posicion 1 esta la palabra combobox
                    switch (conceptos[1])
                    {
                        case "COMBOBOX":
                            for (int i = 2; i < conceptos.Length - 1; i++)//se hace un ciclo donde tomamos la longitud del array
                               //y donde añadimos los datos al combobox
                            {
                                //aqui añadimos al combobox los items que guardamos de la lista conceptos
                                cb.Invoke((MethodInvoker)(() => cb.Items.Add(conceptos[i])));
                            }
                            break;
                        case "DatosTXT"://en el caso de que el comando sea DatosTXT, añadiremos los datos a la tabla
                            using (DataTable dtT = new DataTable())//creamos una instancia para usar la tabla
                            {
                                //añadimos la columnas con su respectivo nombre
                                dtT.Columns.Add("Código");
                                dtT.Columns.Add("Nombre");
                                dtT.Columns.Add("Apellido");
                                dtT.Columns.Add("Edad");
                                dtT.Columns.Add("Nacionalidad");
                                dtT.Columns.Add("Genero");
                                dtT.Columns.Add("Ciudad");
                                dtT.Columns.Add("Estado");
                                dtT.Columns.Add("Universidad");
                                dtT.Columns.Add("Carrera");
                                dtT.Columns.Add("Semestre");
                                dtT.Columns.Add("Deporte favorito");
                                dtT.Columns.Add("Main de LoL");
                                dtT.Columns.Add("Jugador Favorito de fulvo");
                                dtT.Columns.Add("Estado civil");
                                string[] valores = text.ToString().Split('%');//creamos otro array donde separamos el array por medio del %

                                //añadimos cada valor y lo ponemos en cada columna respectivamente
                                dtT.Rows.Add(
                                    valores[1],
                                    valores[2],
                                    valores[3],
                                    valores[4],
                                    valores[5],
                                    valores[6],
                                    valores[7],
                                    valores[8],
                                    valores[9],
                                    valores[10],
                                    valores[11],
                                    valores[12],
                                    valores[13],
                                    valores[14],
                                    valores[15]
                                    );

                                //entablamos los datos los datos
                                dgvShow.Invoke((MethodInvoker)(() => dgvShow.DataSource = dtT));
                                dgv4cols.Invoke((MethodInvoker)(() => dgv4cols.Rows.Add(
                                    valores[1],
                                    valores[2],
                                    valores[3],
                                    valores[4]
                                    )));


                            };
                            //cerramos los casos y se detiene el switch
                            break;
                        case "ActualizarCB":
                            SendRequest("$Comando$%ActualizarCB");
                            break;
                        default:
                            break;
                    }
                    break;
                default:
                    
                    break;
            }

        }



        public Consulta(string ip, string datos, FlowLayoutPanel p)//constructor que requieres un string y un panel, lo usamos en el form principal
        {
            InitializeComponent();
            ip2 = ip;
            Thread thread1 = new Thread(iniciar);//creamos un hilo para que corra una conexión y consulte los datos.
            thread1.Start();//lo iniciamos
            dgvShow.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;//formato a la tabla para que las columnas se muestren machin
            dgv4Cols.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;//formato a la tabla para que las columnas se muestren machin
            panel = p;
        }



        private void Consulta_Load(object sender, EventArgs e)
        {

        }

        
        public void btnModificar_Click(object sender, EventArgs e)//evento del boton modificar al hacerle click
        {
            DialogResult dialogResult = MessageBox.Show("Seguro?", "Confirmar", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                if (!dgvShow.RowCount.Equals(0))
                {
                    var list = new List<string>(15);//variable lista con un array de 15 elemento
                    for (int i = 0; i < 15; i++)
                    {
                        //toma todos los valores de la tabla y los mete a una lista lista
                        list.Add(dgvShow.Rows[0].Cells[i].Value.ToString());
                    }


                    //cambiamos entre paneles, entre el panel de consulta y el de datos
                    this.Hide();//escondemos este frame
                    panel.Controls.Clear();//limpiamos el panel
                    Datos Frm = new Datos(ip2, list);//creamos una nueva instancia de la clase datos donde le pasamos la lista que acabamos de crear en el ciclo anterior
                    Frm.TopLevel = false;
                    panel.Controls.Add(Frm);//añadimos el frame al panel
                    Frm.Show();//lo mostramos

                    //Form1 frm = new Form1();
                    //this.Hide();
                    ////Frm.TopLevel = false;
                    ////Frm.TopLevel = false;
                    ////frm.panelPrincipal.Controls.Add();
                    //Frm.Show();

                    Frm.btnNuevo.Text = "Modificar";//cambiamos el nombre del boton
                }
            }
            else if (dialogResult == DialogResult.No)
            {
                
            }
            
            


        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Seguro?", "Confirmar", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                if (!dgvShow.RowCount.Equals(0))
                {
                    dgv4Cols.Rows.Clear();
                    SendRequest($"$Comando$%Eliminar%{cbxListado.SelectedItem.ToString()}");
                }
                cbxListado.Items.Clear();
            }
            else if (dialogResult == DialogResult.No)
            {

            }

        }

        private void cbxListado_SelectedIndexChanged(object sender, EventArgs e)
        {
            //solicita los datos al servidor del item seleccionado en el combo box
            SendRequest($"$Comando$%datosDelTXT1%{cbxListado.SelectedItem.ToString()}");
            dgv4Cols.Rows.Clear();//No queremos mostrar más de un dato así que lo limpiamos todo.
        }

        private void dgv4Cols_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex==4 && e.RowIndex==0)
            {
                DialogResult dialogResult = MessageBox.Show("Seguro?", "Confirmar", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    if (!dgvShow.RowCount.Equals(0))
                    {
                        var list = new List<string>(15);//variable lista con un array de 15 elemento
                        for (int i = 0; i < 15; i++)
                        {
                            //toma todos los valores de la tabla y los mete a una lista lista
                            list.Add(dgvShow.Rows[0].Cells[i].Value.ToString());
                        }


                        //cambiamos entre paneles, entre el panel de consulta y el de datos
                        this.Hide();//escondemos este frame
                        panel.Controls.Clear();//limpiamos el panel
                        Datos Frm = new Datos(ip2, list);//creamos una nueva instancia de la clase datos donde le pasamos la lista que acabamos de crear en el ciclo anterior
                        Frm.TopLevel = false;
                        panel.Controls.Add(Frm);//añadimos el frame al panel
                        Frm.Show();//lo mostramos

                        //Form1 frm = new Form1();
                        //this.Hide();
                        ////Frm.TopLevel = false;
                        ////Frm.TopLevel = false;
                        ////frm.panelPrincipal.Controls.Add();
                        //Frm.Show();

                        Frm.btnNuevo.Text = "Modificar";//cambiamos el nombre del boton
                    }
                }
                else if (dialogResult == DialogResult.No)
                {

                }
            }
            else if (e.ColumnIndex == 5 && e.RowIndex == 0)
            {
                DialogResult dialogResult = MessageBox.Show("Seguro?", "Confirmar", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    if (!dgvShow.RowCount.Equals(0))
                    {
                        dgv4Cols.Rows.Clear();
                        SendRequest($"$Comando$%Eliminar%{cbxListado.SelectedItem.ToString()}");
                    }
                    cbxListado.Items.Clear();
                }
                else if (dialogResult == DialogResult.No)
                {

                }
            }
        }
    }
    
}
