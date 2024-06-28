using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Server.Abstract
{
    internal interface IMessageSource
    {
        void SendMessage(string message);
        NetMessage Receive(IPEndPoint ep);
    }
}
