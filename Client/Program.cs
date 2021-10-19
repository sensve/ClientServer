using System;
using System.Text;
using System.Net.Sockets;
using System.Threading;

namespace Client
{
    class Program
    {
        private const int PORT_NO = 5000;
        private const string SERVER_IP = "127.0.0.1";
        public delegate void ClientHandlePacketData(byte[] data, int bytesRead);

        static void Main(string[] args)
        {
            TCPClient client = new TCPClient(SERVER_IP, PORT_NO);
            for(int i=0; i<1000; i++)
            {
                if (client.Connected())
                    break;
                Thread.Sleep(1);
            }
            if (!client.Connected())
            {
                Console.WriteLine("[client]: Failed to establish connection!");
            }

            string testStr = "hello";
            Console.WriteLine("[client]: Sending hello to server:");
    
            foreach (char c in testStr)
            {
                Console.WriteLine("[client]: {0}", c.ToString());
                client.SendMessage(c.ToString());
                Thread.Sleep(100);
            }
            Console.WriteLine("[client]: Write something to send to the server:");
            while (true)
            {
                var message = Console.ReadKey().KeyChar.ToString();
                Console.WriteLine();
                client.SendMessage(message);
            }
        }
    }

   /// <summary>
    /// Implements a simple TCP client which connects to a specified server and
    /// raises C# events when data is received from the server
    /// </summary>
   class TCPClient
    {
        #region private members 	
        private TcpClient socketConnection=null;
        private Thread clientReceiveThread;
        private string _hostname { get; set; }
        private int _port { get; set; }
        #endregion
       
        // Use this for initialization 	

        public TCPClient(string host, int port)
        {
            _port = port;
            _hostname = host;
            ConnectToTcpServer();
        }
        /// <summary> 	
        /// Setup socket connection. 	
        /// </summary> 	
        private void ConnectToTcpServer()
        {
            try
            {
                clientReceiveThread = new Thread(new ThreadStart(ListenForData));
                clientReceiveThread.IsBackground = true;
                clientReceiveThread.Start();
            }
            catch (Exception e)
            {
                Console.WriteLine("[client]: On client connect exception " + e);
            }
        }

        public bool Connected()
        {
            return socketConnection == null ? false : true;
        }
        /// <summary> 	
        /// Runs in background clientReceiveThread; Listens for incomming data. 	
        /// </summary>     
        private void ListenForData()
        {
            try
            {
                socketConnection = new TcpClient(_hostname, _port);
                Byte[] bytes = new Byte[1024];
                while (true)
                {
                    // Get a stream object for reading 				
                    using (NetworkStream stream = socketConnection.GetStream())
                    {
                        int length;
                        // Read incomming stream into byte arrary. 					
                        while ((length = stream.Read(bytes, 0, bytes.Length)) != 0)
                        {
                            var incommingData = new byte[length];
                            Array.Copy(bytes, 0, incommingData, 0, length);
                            // Convert byte array to string message. 						
                            string serverMessage = Encoding.ASCII.GetString(incommingData);
                            Console.WriteLine("[client]: received message from server: {0}", serverMessage);
                        }
                    }
                }
            }
            catch (Exception socketException)
            {
                Console.WriteLine("[client]: Socket exception: " + socketException);
            }
        }
        /// <summary> 	
        /// Send message to server using socket connection. 	
        /// </summary> 	
        public void SendMessage(string message)
        {
            if (socketConnection == null)
            {
                Console.WriteLine("[client]: Enable to send, connection does not exist!");
                return;
            }
            try
            {
                // Get a stream object for writing. 			
                NetworkStream stream = socketConnection.GetStream();
                if (stream.CanWrite)
                {
                    // Convert string message to byte array.                 
                    byte[] clientMessageAsByteArray = Encoding.ASCII.GetBytes(message);
                    // Write byte array to socketConnection stream.                 
                    stream.Write(clientMessageAsByteArray, 0, clientMessageAsByteArray.Length);
                    Console.WriteLine("[client]: sending {0} to server.", message);
                }
            }
            catch (SocketException socketException)
            {
                Console.WriteLine("[client]: Socket exception: " + socketException);
            }
        }

        //Doesn't work with cmd - error CS1056: Unexpected character '$'
        //need to comment out 
        /*
#if DEBUG
        public void ClientLog(string message)
        {

            Console.WriteLine($"[client] {message}");
        }

#endif
        */
        
    }
}
