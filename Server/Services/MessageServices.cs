// Класс для работы с сообщениями
using Microsoft.EntityFrameworkCore;
using Server.Models;

namespace Server.Services
{
    public class MessageService
    {
        public List<Message> GetUnreadMessages()
        {
            using (var db = new MessageContext())
            {
                // Получение всех непрочитанных сообщений
                var unreadMessages = db.Messages.Where(m => !m.IsRead).ToList();
                // Отметка сообщений как прочитанных
                //unreadMessages.ForEach(m => m.IsRead = false);
                //db.SaveChanges();
                return unreadMessages;
            }
        }

        public void PrintUnreadMessages()
        {
            List<Message> unreadMessages = GetUnreadMessages();

            foreach (Message message in unreadMessages)
            {
                Console.WriteLine(message.ToString());
            }
        }

        public void PullMessages()
        {
            using (var context = new MessageContext())
            {
                User marina = new User("Марина");
                User irina = new User("Ирина");
                var messages = new List<Message>
                {
                    new Message { IsRead = false, Text = "Привет, как дела?", Sender = marina, Receiver = irina },
                    new Message { IsRead = false, Text = "Встречаемся завтра?", Sender = irina, Receiver = marina },
                    // Добавьте другие сообщения здесь
                };

                context.Messages.AddRange(messages);
                try
                {
                    context.SaveChanges();
                }
                catch (InvalidOperationException e)
                {
                    Console.WriteLine("Ошибка соединения с бд: " + e.Message);
                    throw;
                }
                catch (DbUpdateException e)
                {
                    Console.WriteLine("Ошибка сохранения изменений в бд: " + e.Message);
                    throw;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Неизвестная ошибка: " + e.Message);
                    throw;
                }
            }
        }
    }
}