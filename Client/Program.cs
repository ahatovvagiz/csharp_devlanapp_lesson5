using System;
using System.Diagnostics.Eventing.Reader;
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
            if (args.Length == 0)
            {
                Client.SendMessage("client", "127.0.0.1");
            }
            else if (args.Length == 2)
            {
                Client.SendMessage(args[0], args[1]);
            }
        }

    }
}