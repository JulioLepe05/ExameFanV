using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ServidorExamenPráctico
{
    class Program
    {
        //creamos el socket del servidor
        private static readonly Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        private static readonly List<Socket> clientSockets = new List<Socket>();//lista que  guarda los sockets, que son los de los clientes
        private const int BUFFER_SIZE = 2048;//le asignamos el tamaño del buffer
        private const int PORT = 100;//puerto
        private static readonly byte[] buffer = new byte[BUFFER_SIZE];//esta variable se crea para los mensajes




        static void Main()//constructor
        {
            Console.Title = "Server";
            SetupServer();
            Console.ReadLine(); // When we press enter close everything
            CloseAllSockets();

        }

        private static void SetupServer()
        {
            Console.WriteLine("Setting up server...");
            serverSocket.Bind(new IPEndPoint(IPAddress.Any, PORT));//enlazamos la ip del servidor y el puerto 
            serverSocket.Listen(0);//poneemos el servidor en escucha
            //Vamos a llamar sockets de manera asincrona, Al usar sockets asíncronos, un servidor puede escuchar las conexiones entrantes y, mientras tanto, hacer alguna otra lógica, en contraste con el socket síncrono cuando están escuchando, bloquean el hilo principal y la aplicación deja de responder y se congelará hasta que el cliente se conecte.
            serverSocket.BeginAccept(AcceptCallback, null);//mantenemos el servidor aceptando conexiones
            Console.WriteLine("Server setup complete");
        }

        /// <summary>
        /// Cerramos todos los clientes
        /// </summary>
        private static void CloseAllSockets()
        {
            foreach (Socket socket in clientSockets)//por cada socket en la lista de cliente socket
            {
                socket.Shutdown(SocketShutdown.Both);//apaga el socket y lo cierra
                socket.Close();
            }

            serverSocket.Close();//cierra el socket del servidor
        }

        private static void AcceptCallback(IAsyncResult AR)
        {
            //Creamos un método para aceptar los sockets asincronos, para poderlos almacenar en una lista.
            Socket socket;

            try
            {
                socket = serverSocket.EndAccept(AR);//Aquí vamos a guardar al cliente.
            }
            catch (ObjectDisposedException) // 
            {
                return;
            }

            clientSockets.Add(socket);//Añadimos a la lista
            socket.BeginReceive(buffer, 0, BUFFER_SIZE, SocketFlags.None, ReceiveCallback, socket);//Listener para obtener mensajes, en lugar de mensajes ahora.
            Console.WriteLine("Client connected, waiting for request...");
            serverSocket.BeginAccept(AcceptCallback, null);//Llamada así mismo para crear un loop y continuar operando hasta que se cierre el socket.
        }

        private static void ReceiveCallback(IAsyncResult AR)
        {
            //Método para recibir los mensajes
            Socket current = (Socket)AR.AsyncState;//Nos entrega el socket que mando a hacer la operación, en ocaciones solo queremos actualizar un cliente.
            int received;

            try
            {
                received = current.EndReceive(AR);//Guardamos los bytes recibidos.
            }
            catch (SocketException)
            {
                //Si la conexión se apaga abruptamente cerramos los sockets y liberamos la lista.
                Console.WriteLine("Client forcefully disconnected");
                // Don't shutdown because the socket may be disposed and its disconnected anyway.
                current.Close();
                clientSockets.Remove(current);
                return;
            }


            byte[] recBuf = new byte[received]; //Guardamos los bytes en tamaños como se recibieron.
            Array.Copy(buffer, recBuf, received);//En los tutoriales vimos que guardaban esto por si acaso, no estamos seguros de en que "caso" pero pues igual aquí esta haha.
            string text = Encoding.UTF8.GetString(recBuf);//Guardamos el buffer como string
            Console.WriteLine("Received Text: " + text);
            string[] conceptos = text.ToString().Split('%');//Hacemos split al string recibido para leer el mensaje adecuadamente, si hay que realizar alguna acción o registrar un nuevo cliente/actualizar.
            //El sistema que creamos toma en cuenta los valores en el string separados por los caracteres $ y % dependiendo del caso, por ejemplo, cuando queremos realizar algun comando como obtener los registros, enviamos '$Comando$%ActualizarCB', y con switchs hacemos una u otra operación que finalmente respondera al cliente con un string.
            switch (conceptos[0])
            {
                case "$Comando$":
                    Console.WriteLine("Se ha realizado una consulta tipo comando");
                    switch (conceptos[1])
                    {
                        case "ActualizarCB":
                            var directorio = $@"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\DBEXAMENTEORICO\";//Ruta donde guardamos los archivos.
                            string[] allfiles = Directory.GetFiles(directorio, "*.*", SearchOption.AllDirectories);//Obtenemos todos los archivos guardados.
                            string cadenaItems = "";
                            foreach (var item in allfiles)
                            {
                                cadenaItems = $"{Path.GetFileName(item)}${cadenaItems}";//Le damos el formato archivo.txt% a la cadena con la información para que se ajuste a nuestro sistema.
                            }
                            string itemsCB = $"BROADCAST$COMBOBOX${cadenaItems}";//Este es el mensaje que finalmente enviaremos a los clientes.
                            byte[] Items = Encoding.UTF8.GetBytes(itemsCB);
                            current.Send(Items);
                            break;
                        case "datosDelTXT1":
                            var archivoALeer = $@"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\DBEXAMENTEORICO\{conceptos[2]}";
                            var datosConComando = $"BROADCAST$DatosTXT$%{leerTXT(archivoALeer)}";
                            byte[] datosTXT = Encoding.UTF8.GetBytes(datosConComando);
                            foreach (object sok in clientSockets)//Por cada cliente en nuestra lista, vamos a actualizar el combobox con los archivos guardados.
                            {
                                Socket clienteEnTurno = (Socket)sok;
                                clienteEnTurno.Send(datosTXT);
                            }
                            break;
                        case "Escritura":
                            Console.WriteLine("Se va a escribir en el server");//si no traemos ningún comando entonces por defecto es una escritura.
                            using (DataTable dtT = new DataTable())//Guardamos los datos en un datatable, este código ya lo teniamos, lo usamos Andoni y Aarón para otro proyecto, con este mismo código podemos generar archivos CSV con infinitas lineas, creando un 'DataTable' el cual es como un DataFrame en python o un modelo de tabla en java????? guarda la info pero nada más en forma de datos. Claro que para hacer este proyecto no utilizamos el código a su 100%.
                            {
                                //Añadimos las columnas del datatable, al final ni las vamos a usar pero bueno haha.
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
                                string input = text;//Tomamos y hacemos split al mensaje original.
                                string[] valores = input.Split('%');
                                //Ahora traemos los datos que envio el cliente en un string separado por '%'
                                dtT.Rows.Add(
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
                                                valores[15],
                                                valores[16]
                                                );
                                StringBuilder sb = new StringBuilder();//Literalmente el nombre lo dice, es un constructor de strings.

                                //string[] columnNames = dtT.Columns.Cast<DataColumn>().
                                //                                  Select(column => column.ColumnName).
                                //                                  ToArray();
                                //sb.AppendLine(string.Join("%", columnNames));

                                //foreach (DataRow row in dtT.Rows)
                                //{
                                string[] fields = dtT.Rows[0].ItemArray.Select(field => field.ToString()).ToArray();//Objeto de celda en el datatable a string.
                                sb.AppendLine(string.Join("%", fields));//Juntamos todo en una sola linea con el formato adecuado.
                                                                        //}
                                var director = $@"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\DBEXAMENTEORICO\";

                                if (!Directory.Exists(director))//Si el directorio no existe lo crea.
                                {
                                    Directory.CreateDirectory(director);
                                }
                                File.WriteAllText($@"{director}{conceptos[3]}.txt", sb.ToString(), Encoding.UTF8);//Mandamos a crear el archivo.
                                Console.WriteLine($@"Guardado satisfactoriamente en: {director} como {conceptos[3]}.txt");
                            };
                            byte[] data2 = Encoding.UTF8.GetBytes("Escritura realizada satisfactoriamente");//Mensaje de exito.
                            current.Send(data2);
                            break;
                        default:
                            break;
                    }

                    break;
                case "exit"://Cerrar el socket.
                    current.Shutdown(SocketShutdown.Both);
                    current.Close();
                    clientSockets.Remove(current);
                    Console.WriteLine("Client disconnected");
                    return;
                    break;

                default:
                    return;
                    break;

            }

            current.BeginReceive(buffer, 0, BUFFER_SIZE, SocketFlags.None, ReceiveCallback, current);
        }
        private static string leerTXT(string directorio)
        {
            //Este método tampoco lo usamos a su 100%, pero nos permite leer un archivo como un array de strings, claro que solo vamos a leer una linea en este proyecto :P
            List<string> datos = new List<string>();
            using (StreamReader sr = new StreamReader(directorio))
            {

                string linea = string.Empty;
                while ((linea = sr.ReadLine()) != null)
                {
                    datos.Add(linea);
                }

            }
            return datos[0];
        }


    }
}
