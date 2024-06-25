using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Server;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Client.SentMessage("client", "127.0.0.1");
        }

    }
}