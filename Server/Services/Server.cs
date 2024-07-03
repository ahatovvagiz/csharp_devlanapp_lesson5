using System.Net.Sockets;
using System.Text;
using Microsoft.EntityFrameworkCore;
using NetMQ;
using NetMQ.Sockets;
using Server.Models;

namespace Server.Services
{
    // тут храним состояние сервера
    class ServerMemento
    {
        public ServerMemento(string message, CancellationTokenSource cancellationToken)
        {
            Message = message;
            CancellationToken = cancellationToken;
        }

        public string Message { get; private set; }
        public CancellationTokenSource CancellationToken { get; private set; }
    }

    // класс, который будет сохранять и восстанавливать состояние сервера
    class ServerCaretaker
    {
        public ServerMemento Memento { get; set; }
    }

    class NetServer
    {

        public ServerCaretaker caretaker = new ServerCaretaker();
        public string msg = string.Empty;
        public CancellationTokenSource cts = new CancellationTokenSource();

        public static async Task Server(string message, CancellationTokenSource cancellationToken)
        {
            using (var netMQServer = new ResponseSocket())
            {
                netMQServer.Bind("tcp://*:5556");

                Console.WriteLine("Сервер ждет сообщение от клиента. Нажмите любую клавишу для выхода.");

                while (!cancellationToken.IsCancellationRequested)
                {
                    //UdpReceiveResult result = await netMQServer.Receiv();
                    byte[] buffer = netMQServer.ReceiveFrameBytes();
                    //byte[] buffer = result.Buffer;
                    var messageText = Encoding.UTF8.GetString(buffer);

                    NetMessage clientmessage = NetMessage.DeserializeFromJson(messageText);
                    clientmessage.Print();

                    // Отправляем подтверждение клиенту
                    string confirmationMessage = "Сообщение успешно обработано на сервере";
                    byte[] confirmationBuffer = Encoding.UTF8.GetBytes(confirmationMessage);
                    
                    //await udpClient.SendAsync(confirmationBuffer, confirmationBuffer.Length, result.RemoteEndPoint);

                    if (clientmessage.Text.ToLower() == "exit")
                    {
                        cancellationToken.Cancel();
                        Console.WriteLine("Завершение работы...");
                    }
                }

                netMQServer.Close();
            }
            //UdpClient udpClient = new UdpClient(12345);
            //IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Any, 0);

            
        }
        // Сохранение состояния сервера
        public void SaveState(CancellationToken cancellationToken)
        {
            {
                caretaker.Memento = new ServerMemento("Состояние сервера сохранено", cts);
            }
        }
        public void RestoreState()
        {
            if (caretaker.Memento != null)
            {
                msg = caretaker.Memento.Message;
                cts = caretaker.Memento.CancellationToken; // Исправлено на cancellationToken
                Console.WriteLine("Состояние сервера восстановлено: " + msg);
            }
        }
    }
}