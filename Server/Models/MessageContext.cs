using Microsoft.EntityFrameworkCore;

namespace Server.Models
{
    // Контекст базы данных
    public class MessageContext : DbContext
    {
        public DbSet<Message> Messages { get; set; }
        public DbSet<User> Users { get; set; }

        public MessageContext()
        {

        }

        public MessageContext(DbSet<Message> messages, DbSet<User> users)
        {
            Messages = messages;
            Users = users;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Database=lesson5;Username=postgres;Password=example").UseLazyLoadingProxies();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("Users");

                entity.HasKey(x => x.Id_User).HasName("Id_User");
                entity.HasIndex(x => x.FullName).IsUnique();

                //entity.Property(e => e.Id).HasColumnName("Id");
                entity.Property(e => e.FullName).HasColumnName("FullName").HasMaxLength(255);
            }
            );

            modelBuilder.Entity<Message>(entity =>
            {
                entity.ToTable("Messages");

                entity.HasKey(x => x.Id_Message).HasName("Id_Message");

                //entity.Property(e => e.Id).HasColumnName("Id");
                entity.Property(e => e.Text).HasColumnName("Text");
                entity.Property(e => e.IsRead).HasColumnName("IsRead");
                entity.Property(e => e.DateTime).HasColumnName("DateTime");
                //entity.Property(e => e.Sender).HasColumnName("Sender");
                //entity.Property(e => e.Receiver).HasColumnName("Receiver");

                entity.HasOne(x => x.Sender).WithMany(m => m.MessagesSend).HasForeignKey(x => x.SenderId).HasConstraintName("messageSendFK");
                entity.HasOne(x => x.Receiver).WithMany(m => m.MessagesReceived).HasForeignKey(x => x.ReceiverId).HasConstraintName("messageReceivedFK");
            }
            );
        }
    }
}
