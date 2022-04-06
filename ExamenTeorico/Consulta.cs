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
        private static readonly Socket ClientSocket = new Socket
            (AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        private const int PORT = 100;

        Panel panel;
        public Consulta(string datos)
        {
            InitializeComponent();
            
        }
        public void iniciar()
        {
            ConnectToServer();
            SendRequest("$Comando$%ActualizarCB");
            RequestLoop(cbxListado, dgvShow);

        }
        private static void ConnectToServer()
        {
            int attempts = 0;

            while (!ClientSocket.Connected)
            {
                try
                {
                    attempts++;
                    //Console.WriteLine("Connection attempt " + attempts);
                    // Change IPAddress.Loopback to a remote IP to connect to a remote host.
                    ClientSocket.Connect(IPAddress.Loopback, PORT);
                }
                catch (SocketException)
                {
                    //Console.Clear();
                }
            }

            //Console.Clear();
            //Console.WriteLine("Connected");
        }

        private static void RequestLoop(ComboBox cb, DataGridView dgvShow)
        {
            //Console.WriteLine(@"<Type ""exit"" to properly disconnect client>");

            while (true)
            {
                //SendRequest();
                ReceiveResponse(cb, dgvShow);
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

        private static void ReceiveResponse(ComboBox cb, DataGridView dgvShow)
        {
            var buffer = new byte[2048];
            int received = ClientSocket.Receive(buffer, SocketFlags.None);
            if (received == 0) return;
            var data = new byte[received];
            Array.Copy(buffer, data, received);
            string text = Encoding.ASCII.GetString(data);
            //Console.WriteLine(text);

            string[] conceptos = text.ToString().Split('$');
            switch (conceptos[0])
            {
                case "BROADCAST":
                    switch (conceptos[1])
                    {
                        case "COMBOBOX":
                            for (int i = 2; i < conceptos.Length - 1; i++)
                            {

                                cb.Invoke((MethodInvoker)(() => cb.Items.Add(conceptos[i])));
                            }
                            break;
                        case "DatosTXT":
                            using (DataTable dtT = new DataTable())
                            {
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
                                string[] valores = text.ToString().Split('%');

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

                                dgvShow.Invoke((MethodInvoker)(() => dgvShow.DataSource = dtT));

                            };
                            break;
                        default:
                            break;
                    }
                    break;
                default:
                    
                    break;
            }

        }



        public Consulta(string datos, FlowLayoutPanel p)
        {
            InitializeComponent();
            Thread thread1 = new Thread(iniciar);
            thread1.Start();
            dgvShow.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
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

        private void cbxListado_SelectedIndexChanged(object sender, EventArgs e)
        {
            SendRequest($"$Comando$%datosDelTXT1%{cbxListado.SelectedItem.ToString()}");
        }
    }
    
}
