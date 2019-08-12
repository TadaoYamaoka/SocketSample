using System;
using System.IO;
using System.Collections.Generic;
using System.Net.Sockets;

namespace SocketSample
{
    class A {
        public int a;
        public int[] b;

        public A(int a, int[] b)
        {
            this.a = a;
            this.b = b;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            TcpClient tcp = new TcpClient("127.0.0.1", 50007);
            NetworkStream ns = tcp.GetStream();

            for (int n = 0; n < 2; n++)
            {
                // write
                A a = new A(n, new int[] { 1, 2, 3 });

                var ms = new MemoryStream();
                int size = 4 + 4 + 4 * a.b.Length;
                ms.Write(BitConverter.GetBytes(size), 0, 4);
                ms.Write(BitConverter.GetBytes(a.a), 0, 4);
                ms.Write(BitConverter.GetBytes(a.b.Length), 0, 4);
                foreach (var v in a.b)
                {
                    ms.Write(BitConverter.GetBytes(v), 0, 4);
                }
                ns.Write(ms.ToArray(), 0, 4 + size);

                // read
                var res = new List<int>();

                byte[] int_bytes = new byte[4];
                ns.Read(int_bytes, 0, 4);
                size = BitConverter.ToInt32(int_bytes, 0);
                byte[] buffer = new byte[4 * size];
                ns.Read(buffer, 0, 4 * size);
                for (int i = 0; i < size; i++)
                {
                    res.Add(BitConverter.ToInt32(buffer, 4 * i));
                }
                Console.WriteLine(size);
                Console.Write("[");
                foreach (var v in res)
                {
                    Console.Write(v);
                    Console.Write(",");
                }
                Console.WriteLine("]");
            }

            ns.Close();
            tcp.Close();
        }
    }
}
