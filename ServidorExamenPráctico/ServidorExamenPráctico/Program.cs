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
        private static readonly Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        private static readonly List<Socket> clientSockets = new List<Socket>();
        private const int BUFFER_SIZE = 2048;
        private const int PORT = 100;
        private static readonly byte[] buffer = new byte[BUFFER_SIZE];

        
        

        static void Main()
        {
            Console.Title = "Server";
            SetupServer();
            Console.ReadLine(); // When we press enter close everything
            CloseAllSockets();
            
        }

        private static void SetupServer()
        {
            Console.WriteLine("Setting up server...");
            serverSocket.Bind(new IPEndPoint(IPAddress.Any, PORT));
            serverSocket.Listen(0);
            serverSocket.BeginAccept(AcceptCallback, null);
            Console.WriteLine("Server setup complete");
        }

        /// <summary>
        /// Close all connected client (we do not need to shutdown the server socket as its connections
        /// are already closed with the clients).
        /// </summary>
        private static void CloseAllSockets()
        {
            foreach (Socket socket in clientSockets)
            {
                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
            }

            serverSocket.Close();
        }

        private static void AcceptCallback(IAsyncResult AR)
        {
            Socket socket;

            try
            {
                socket = serverSocket.EndAccept(AR);
            }
            catch (ObjectDisposedException) // I cannot seem to avoid this (on exit when properly closing sockets)
            {
                return;
            }

            clientSockets.Add(socket);
            socket.BeginReceive(buffer, 0, BUFFER_SIZE, SocketFlags.None, ReceiveCallback, socket);
            Console.WriteLine("Client connected, waiting for request...");
            serverSocket.BeginAccept(AcceptCallback, null);
        }

        private static void ReceiveCallback(IAsyncResult AR)
        {
            Socket current = (Socket)AR.AsyncState;
            int received;

            try
            {
                received = current.EndReceive(AR);
            }
            catch (SocketException)
            {
                Console.WriteLine("Client forcefully disconnected");
                // Don't shutdown because the socket may be disposed and its disconnected anyway.
                current.Close();
                clientSockets.Remove(current);
                return;
            }

            byte[] recBuf = new byte[received];
            Array.Copy(buffer, recBuf, received);
            string text = Encoding.ASCII.GetString(recBuf);
            Console.WriteLine("Received Text: " + text);
            string[] conceptos = text.ToString().Split('%');
            switch (conceptos[0])
            {
                case "$Comando$":
                    Console.WriteLine("Se ha realizado una consulta un comando");
                    switch (conceptos[1])
                    {
                        case "ActualizarCB":
                            var directorio = $@"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\DBEXAMENTEORICO\";
                            string[] allfiles = Directory.GetFiles(directorio, "*.*", SearchOption.AllDirectories);
                            string cadenaItems = "";
                            foreach (var item in allfiles)
                            {
                                cadenaItems = $"{Path.GetFileName(item)}${cadenaItems}";
                            }
                            string itemsCB = $"BROADCAST$COMBOBOX${cadenaItems}";
                            byte[] Items = Encoding.UTF8.GetBytes(itemsCB);
                            current.Send(Items);
                            break;
                        case "datosDelTXT1":
                            var archivoALeer = $@"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\DBEXAMENTEORICO\{conceptos[2]}";
                            var datosConComando = $"BROADCAST$DatosTXT$%{leerTXT(archivoALeer)}";
                            byte[] datosTXT = Encoding.UTF8.GetBytes(datosConComando);
                            current.Send(datosTXT);
                            break;
                        default:
                            break;
                    }

                    break;
                default:
                    Console.WriteLine("Se va a escribir en el server");
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
                        string input = text;
                        string[] valores = input.Split('%');
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
                                        valores[14]
                                        );
                        StringBuilder sb = new StringBuilder();

                        string[] columnNames = dtT.Columns.Cast<DataColumn>().
                                                          Select(column => column.ColumnName).
                                                          ToArray();
                        sb.AppendLine(string.Join("%", columnNames));

                        foreach (DataRow row in dtT.Rows)
                        {
                            string[] fields = row.ItemArray.Select(field => field.ToString()).
                                                            ToArray();
                            sb.AppendLine(string.Join("%", fields));
                        }
                        var directorio = $@"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\DBEXAMENTEORICO\";

                        if (!Directory.Exists(directorio))
                        {
                            Directory.CreateDirectory(directorio);
                        }
                        File.WriteAllText($@"{directorio}{conceptos[0]}.txt", sb.ToString(), Encoding.UTF8);
                        Console.WriteLine($@"Guardado satisfactoriamente en: {directorio} como {conceptos[0]}.txt");
                    };
                    byte[] data2 = Encoding.UTF8.GetBytes("Escritura realizada satisfactoriamente");
                    current.Send(data2);
                    break;

            }
            //switch (text)
            //{
            //    case "get time":
            //        Console.WriteLine("Text is a get time request");
            //        byte[] data = Encoding.ASCII.GetBytes(DateTime.Now.ToLongTimeString());
            //        current.Send(data);
            //        Console.WriteLine("Time sent to client");
            //        break;
            //    case "exit":
            //    current.Shutdown(SocketShutdown.Both);
            //        current.Close();
            //        clientSockets.Remove(current);
            //        Console.WriteLine("Client disconnected");
            //        return;
            //    default:
            //        Console.WriteLine("Text is an invalid request");
            //        byte[] data2 = Encoding.ASCII.GetBytes("Invalid request");
            //        current.Send(data2);
            //        Console.WriteLine("Warning Sent");
            //        break;
            //}

            //if (text.ToLower() == "get time") // Client requested time
            //{
            //    Console.WriteLine("Text is a get time request");
            //    byte[] data = Encoding.ASCII.GetBytes(DateTime.Now.ToLongTimeString());
            //    current.Send(data);
            //    Console.WriteLine("Time sent to client");
            //}
            //else if (text.ToLower() == "exit") // Client wants to exit gracefully
            //{
            //    // Always Shutdown before closing
            //    current.Shutdown(SocketShutdown.Both);
            //    current.Close();
            //    clientSockets.Remove(current);
            //    Console.WriteLine("Client disconnected");
            //    return;
            //}
            //else
            //{
            //    Console.WriteLine("Text is an invalid request");
            //    byte[] data = Encoding.ASCII.GetBytes("Invalid request");
            //    current.Send(data);
            //    Console.WriteLine("Warning Sent");
            //}

            current.BeginReceive(buffer, 0, BUFFER_SIZE, SocketFlags.None, ReceiveCallback, current);
        }
        private static string leerTXT(string directorio)
        {
            List<string> datos = new List<string>();
            using (StreamReader sr = new StreamReader(directorio))
            {

                string linea = string.Empty;
                while ((linea = sr.ReadLine()) != null)
                {
                    datos.Add(linea);
                }

            }
            return datos[1];
        }


    }
}
