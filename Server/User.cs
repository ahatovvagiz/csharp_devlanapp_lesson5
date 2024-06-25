namespace Server
{
    public class User
    {
        public User()
        {

        }
        public User(string fullname)
        { 
            FullName = fullname;
        }
        public virtual List<Message>? MessagesSend { get; set; } = new List<Message>();
        public virtual List<Message>? MessagesReceived { get; set; } = new List<Message>();
        public int Id_User { get; set; }
        public string FullName { get; set; }
    }
}
