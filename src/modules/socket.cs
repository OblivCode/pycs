using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Timers;
using static pycs.pycs;

namespace pycs.modules
{
    public class socket
    {
        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }
        public class Connection
        {
            private Socket socket;
            private Timer timer1;
            public event EmptyHandler OnDisconnect;
            public Connection(Socket socket)
            { 
                this.socket = socket;
               var timer1 = new Timer()
                {
                    AutoReset = True,
                    Interval = 100,
                    Enabled = true
                };
                timer1.Elapsed += check_connection;
                timer1.Start();
            }

            private void check_connection(object sender, EventArgs args)
            {
                if (!socket.Connected)
                {
                    timer1.Stop();
                    OnDisconnect.Invoke();
                    socket.Dispose();
                    socket = null;
                }
            }
            /// <exception cref="ConnectionError"/>
            public byte[] recv(int buf_size)
            {
                if (!socket.Connected)
                    throw new ConnectionError("Connection closed", ConnectionError.Type.BrokenPipeError);
                byte[] buf = new byte[buf_size];
                socket.Receive(buf);
                return buf;
            }
            /// <exception cref="ConnectionError"  />
            public Connection sendall(string message)
            {
                try { sendall(bytearray(message)); }
                catch(ConnectionError ce) { throw ce; }
                return this;
            }
            /// <exception cref="ConnectionError" />
            public Connection sendall(byte[] bytes)
            {
                if (!socket.Connected)
                    throw new ConnectionError("Connection closed", ConnectionError.Type.BrokenPipeError);
                socket.Send(bytes);
                return this;
            }
            public void close() => this.socket.Close();
        }

        public class UDP
        {
            private UdpClient client;
           
            public UDP(AddressFamily family) {
                client = new UdpClient(family);
            }
            /// <exception cref="ConnectionError" />
            public UDP connect((string, int) endpoint)
            {
                try { connect(endpoint.Item1, endpoint.Item2); }
                catch (ConnectionError ce) {  throw ce; }
                return this;
            }
            /// <exception cref="ConnectionError" />
            public UDP connect(string host, int port)
            {
                try { client.Connect(host, port); }
                catch (Exception e) { throw new ConnectionError(e.Message, ConnectionError.Type.BrokenPipeError, e); };
                return this;
            }
            /// <exception cref="ConnectionError" />
            public UDP sendall(string message)
            {
                try { sendall(bytearray(message)); }
                catch (ConnectionError ce) {  throw ce; }
                return this;
            }
            /// <exception cref="ConnectionError" />
            public UDP sendall(byte[]bytes)
            {
                try { client.Send(bytes, bytes.Length); }
                catch (Exception e) { throw new ConnectionError(e.Message, ConnectionError.Type.BrokenPipeError, e); }
                return this;
            }

            public byte[] recv(string host, int port)
            {
                host = host == "localhost" ? "127.0.0.1" : host;
                IPEndPoint endpoint = new IPEndPoint(IPAddress.Parse(host), port);
                return client.Receive(ref endpoint);
            }
        }

        public class TCP
        {
            private TcpClient client;
            private NetworkStream stream { get { return client != null ? client.GetStream() : null; } }
            private TcpListener listener;
            private AddressFamily AF;
            public TCP(AddressFamily family = AF_INET) => AF = family;
            //server-------------
            /// <exception cref="ConnectionError"
            public TCP bind((string, int) endpoint)
            {
                listener = endpoint.Item1 == "localhost" ? new TcpListener(IPAddress.Parse("127.0.0.1"), endpoint.Item2)
                    : new TcpListener(IPAddress.Parse(endpoint.Item1), endpoint.Item2);
                return this;
            }
            public TCP listen()
            {
                listener.Start();
                return this;
            }
            public TCP listen(int backlog)
            {
                listener.Start(backlog);
                return this;
            }
            public (Connection, string) accept()
            {
                if (listener == null)
                    throw new ConnectionError("Socket not binded", ConnectionError.Type.BrokenPipeError);
                var sock = listener.AcceptSocket();
                return (new Connection(sock), sock.RemoteEndPoint.ToString());
            }
            public async Task<(Connection, string)> acceptasync()
            {
                if (listener == null)
                    throw new ConnectionError("Socket not binded", ConnectionError.Type.BrokenPipeError);
                var sock = await listener.AcceptSocketAsync();
                return (new Connection(sock), sock.RemoteEndPoint.ToString());
            }
            //client------------
            /// <exception cref="ConnectionError" />
            public TCP connect((string, int) endpoint)
            {
                try { connect(endpoint.Item1, endpoint.Item2); }
                catch(ConnectionError ex) { throw ex; }
                return this;
            }

            ///  <exception cref="ConnectionError" />
            public TCP connect(string addr, int port)
            {
                try
                {
                    client = new TcpClient(AF);
                    client.Connect(addr, port);
                }
                catch (Exception ex)
                {
                    print(ex.Message);
                    throw new ConnectionError(ex.Message, ConnectionError.Type.ConnectionRefusedError, ex);
                }
                return this;
            }
            //------------sendall--------------
            /// <exception cref="ConnectionError" />
            public TCP sendall(string message)
            {
                byte[] buffer = bytearray(message);
                
                try { sendall(buffer); }
                catch (ConnectionError ce) { throw ce; }
                return this;
            }
            /// <exception cref="ConnectionError" />
            public TCP sendall(byte[] bytes)
            {
                if (!client.Connected || client == null)
                    throw new ConnectionError("Socket not connected", ConnectionError.Type.BrokenPipeError);
                stream.Write(bytes, 0, bytes.Length);
                return this;
            }
            /// <exception cref="ConnectionError" />
            public async Task<TCP> sendall_async(string message)
            {
                byte[] buffer = bytearray(message);

                try { await sendall_async(buffer); }
                catch (ConnectionError ce) { throw ce; }
                return this;
            }
            /// <exception cref="ConnectionError" />
            public async Task<TCP> sendall_async(byte[] bytes)
            {
                if (!client.Connected || client == null)
                    throw new ConnectionError("Socket not connected", ConnectionError.Type.BrokenPipeError);
                await stream.WriteAsync(bytes, 0, bytes.Length);
                return this;
            }
            //---------------------------
            /// <exception cref="ConnectionError" />
            public byte[] recv(int buf_size)
            {
                if(client == null || !client.Connected)
                    throw new ConnectionError("Socket not connected", ConnectionError.Type.BrokenPipeError);
                byte[] buffer = new byte[buf_size];
                stream.Read(buffer, 0, buf_size);
                return buffer;
            }
            public async Task<byte[]> recv_async(int buf_size)
            {
                if (client == null || !client.Connected)
                    throw new ConnectionError("Socket not connected", ConnectionError.Type.BrokenPipeError);
                byte[] buffer = new byte[buf_size];
                await stream.ReadAsync(buffer, 0, buf_size);
                return buffer;
            }

            public void close()
            {
                if (client.Connected)
                    client.Close();
                if(listener.Server.Connected)
                    listener.Stop();
                
            }
        }
       
        //consts
        public const AddressFamily AF_UNIX = AddressFamily.Unix;
        public const AddressFamily AF_INET = AddressFamily.InterNetwork;
        public const AddressFamily AF_INET6 = AddressFamily.InterNetworkV6;

        public const SocketType SOCK_STREAM = SocketType.Stream;
        public const SocketType SOCK_DGRAM = SocketType.Dgram;
        public const SocketType SOCK_RAW = SocketType.Raw;
        public const SocketType SOCK_RDM = SocketType.Rdm;
        public const SocketType SOCK_SEQPACKET = SocketType.Seqpacket;



    }

}
