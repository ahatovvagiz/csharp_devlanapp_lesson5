using System.Text.Json;
using Microsoft.EntityFrameworkCore;

// Определение модели сообщения
namespace Server
{
    public class Message
    {
        public int Id_Message { get; set; }
        public bool IsRead { get; set; }
        public string Text { get; set; }
        public DateTime DateTime { get; set; }
        public int? SenderId { get; set; }
        public int? ReceiverId { get; set; }
        public virtual User Sender { get; set; }
        public virtual User Receiver { get; set; }
        public override string ToString()
        {
            return $"{this.DateTime} получено сообщение {this.Text}";
        }
    }
}