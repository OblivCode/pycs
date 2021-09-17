using static pycs.pycs;
using pycs.modules;

namespace my_app {
    class Program {
        static void client()
        {
            var sock = new socket.TCP(socket.AF_INET);
            var endpoint = ("localhost", 90);
            sock.connect(endpoint);

            sock.sendall("Hello");
            string data = repr(sock.recv(1024));
            sock.sendall(data);
            data = repr(sock.recv(1024));
            print(data);
        }

        static void server()
        {
            var sock = new socket.TCP(socket.AF_INET);
            var endpoint = ("localhost", 90);
            sock.bind(endpoint);
            sock.listen();
            print("listening on " + 80);
            var (client, addr) = sock.accept();
            print("Connected by " + addr);

            string data = repr(client.recv(1024));
            print(data);

            client.sendall("World");

            data = repr(client.recv(1024));
            print(data);

            client.close();

        }
    }
}