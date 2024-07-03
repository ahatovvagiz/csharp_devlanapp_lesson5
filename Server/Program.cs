using System;
using Server.Services;

namespace Server
{
    class Program
    {
        static async Task Main(string[] args)
        {
            CancellationTokenSource cts = new CancellationTokenSource();

            Task task = Task.Run(() => NetServer.Server("Hello", cts));

            Console.WriteLine("Для выхода нажмите любую клавишу...");
            Console.ReadKey();
            cts.Cancel();

            await task;
            //MessageService messageService = new MessageService();
            //messageService.PrintUnreadMessages();
        }
        
    }
}