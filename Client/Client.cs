using Server;
using System.Net.Sockets;
using System.Net;
using System.Text;

namespace Client
{
    class Client
    {
        public static void SentMessage(string From, string ip)
        {
            UdpClient udpClient = new UdpClient();
            IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Parse(ip), 12345);

            while (true)
            {
                string messageText;
                do
                {
                    // Console.Clear();
                    Console.WriteLine("Введите сообщение (для выхода введите \"Exit\", для получения списка непрочитанных сообщений \"Get\"): ");
                    messageText = Console.ReadLine();
                     
                    if (messageText.ToLower() == "exit")
                    {
                        Console.WriteLine("Завершение работы клиента.");

                        udpClient.Close();
                        return;
                    }
                    else if (messageText.ToLower() == "get")
                    {
                        Console.WriteLine("Получение списка непрочитанных сообщений!");

                        MessageService messageservice = new MessageService();

                        messageservice.PrintUnreadMessages();

                        udpClient.Close();
                        return;
                    }
                    else if (messageText.ToLower() == "pull")
                    {
                        Console.WriteLine("Запись данных в бд!");

                        MessageService messageservice = new MessageService();

                        messageservice.PullMessages();

                        udpClient.Close();
                        return;
                    }
                }
                while (string.IsNullOrEmpty(messageText));
                NetMessage message = new NetMessage() { Text = messageText, DateTime = DateTime.Now };
                string json = message.SerializeMessageToJson();

                byte[] data = Encoding.UTF8.GetBytes(json);
                udpClient.Send(data, data.Length, ipEndPoint);

                // Получение подтверждения от сервера
                byte[] receivedBytes = udpClient.Receive(ref ipEndPoint);
                string confirmationMessage = Encoding.UTF8.GetString(receivedBytes);
                Console.WriteLine($"Подтверждение от сервера: {confirmationMessage}");

            }

        }
    }
}