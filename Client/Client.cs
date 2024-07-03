using System.Net.Sockets;
using System.Net;
using System.Text;
using Server.Services;
using Server.Models;
using Server.Abstract;
using NetMQ.Sockets;
using NetMQ;

namespace Client
{
    public class Client : IMessageSourceClient
    {
        public void SendMessage(string From, string ip)
        {
            using (var netMQClient = new RequestSocket())
            {
                netMQClient.Connect(ip);

                while (true)
                {
                    string messageText;
                    do
                    {

                        // Console.Clear();
                        if (From == "TestUser")
                        {
                            messageText = "Тест";
                        }
                        else
                        {
                            messageText = Console.ReadLine();
                        }

                        if (messageText.ToLower() == "exit")
                        {
                            Console.WriteLine("Завершение работы клиента.");

                            netMQClient.Close();
                            return;
                        }
                        else if (messageText.ToLower() == "get")
                        {
                            Console.WriteLine("Получение списка непрочитанных сообщений!");

                            MessageService messageservice = new MessageService();

                            messageservice.PrintUnreadMessages();

                            netMQClient.Close();
                            return;
                        }
                        else if (messageText.ToLower() == "pull")
                        {
                            Console.WriteLine("Запись данных в бд!");

                            MessageService messageservice = new MessageService();

                            messageservice.PullMessages();

                            netMQClient.Close();
                            return;
                        }
                        else if (messageText.ToLower() == "test")
                        {
                            Console.WriteLine("Запись данных в бд!");

                            MessageService messageservice = new MessageService();

                            messageservice.PullMessages();

                            netMQClient.Close();
                            return;
                        }
                    }
                    while (string.IsNullOrEmpty(messageText));
                    NetMessage message = new NetMessage() { Text = messageText, DateTime = DateTime.Now, SenderFullName = From };
                    string json = message.SerializeMessageToJson();

                    byte[] data = Encoding.UTF8.GetBytes(json);
                    netMQClient.SendFrame(data);

                    var msg = netMQClient.ReceiveFrameString();

                    // Получение подтверждения от сервера

                    //byte[] receivedBytes = netMQClient.Receive(ref msg);
                    //string confirmationMessage = Encoding.UTF8.GetString(receivedBytes);
                    //Console.WriteLine($"Подтверждение от сервера: {confirmationMessage}");

                }
            }
            throw new NotImplementedException();
        }
    }
    //public void SendMessage(string From, string ip)
    //{
    //    using (var netMQClient = new RequestSocket())
    //    {
    //        netMQClient.Connect(ip);

    //        while (true)
    //        {
    //            string messageText;
    //            do
    //            {

    //                // Console.Clear();
    //                if (From == "TestUser")
    //                {
    //                    messageText = "Тест";
    //                }
    //                else
    //                {
    //                   messageText = Console.ReadLine();
    //                }

    //                if (messageText.ToLower() == "exit")
    //                {
    //                    Console.WriteLine("Завершение работы клиента.");

    //                    netMQClient.Close();
    //                    return;
    //                }
    //                else if (messageText.ToLower() == "get")
    //                {
    //                    Console.WriteLine("Получение списка непрочитанных сообщений!");

    //                    MessageService messageservice = new MessageService();

    //                    messageservice.PrintUnreadMessages();

    //                    netMQClient.Close();
    //                    return;
    //                }
    //                else if (messageText.ToLower() == "pull")
    //                {
    //                    Console.WriteLine("Запись данных в бд!");

    //                    MessageService messageservice = new MessageService();

    //                    messageservice.PullMessages();

    //                    netMQClient.Close();
    //                    return;
    //                }
    //                else if (messageText.ToLower() == "test")
    //                {
    //                    Console.WriteLine("Запись данных в бд!");

    //                    MessageService messageservice = new MessageService();

    //                    messageservice.PullMessages();

    //                    netMQClient.Close();
    //                    return;
    //                }
    //            }
    //            while (string.IsNullOrEmpty(messageText));
    //            NetMessage message = new NetMessage() { Text = messageText, DateTime = DateTime.Now, SenderFullName = From };
    //            string json = message.SerializeMessageToJson();

    //            byte[] data = Encoding.UTF8.GetBytes(json);                                                  
    //            netMQClient.SendFrame(data);

    //            var msg = netMQClient.ReceiveFrameString(); 

    //            // Получение подтверждения от сервера

    //            //byte[] receivedBytes = netMQClient.Receive(ref msg);
    //            //string confirmationMessage = Encoding.UTF8.GetString(receivedBytes);
    //            //Console.WriteLine($"Подтверждение от сервера: {confirmationMessage}");

    //        }
    //    }
    //    //UdpClient udpClient = new UdpClient();
    //    //IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Parse(ip), 12345);



    //}
}
