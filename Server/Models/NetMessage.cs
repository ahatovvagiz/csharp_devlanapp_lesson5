using System.Text.Json;
using Microsoft.EntityFrameworkCore;

// Определение модели сообщения
namespace Server
{
    public enum Command
    {
        Register,
        Message,
        Confirmation
    }
    public class NetMessage
    {
        public int Id { get; set; }
        public bool IsRead { get; set; }
        public string? Text { get; set; }
        public DateTime DateTime { get; set; }
        public string? SenderFullName { get; set; }
        public string? ReceiverFullName { get; set; }

        public Command Command { get; set; }
        public string SerializeMessageToJson()
        {
            return JsonSerializer.Serialize(this);
        }

        public static NetMessage? DeserializeFromJson(string message) => JsonSerializer.Deserialize<NetMessage>(message);

        public void Print()
        {
            Console.WriteLine(ToString());
        }
        public override string ToString()
        {
            return $"{this.DateTime} получено сообщение {this.Text}  от  {this.SenderFullName}";
        }

    }
}