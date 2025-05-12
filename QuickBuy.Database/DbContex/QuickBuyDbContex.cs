using Microsoft.EntityFrameworkCore;
using QuickBuy.Database.Models;
using System.Collections.Generic;

namespace QuickBuy.Database.DbContext
{
    public class QuickBuyDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Item> Items { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set; }
        public virtual DbSet<Chat> Chats { get; set; }
        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<PickupDetails> PickupDetails { get; set; }

        public QuickBuyDbContext()
        {
        }

        public QuickBuyDbContext(DbContextOptions<QuickBuyDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=QuickBuyDb;Trusted_Connection=true;TrustServerCertificate=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .Property(u => u.Name)
                .HasMaxLength(30);

            modelBuilder.Entity<User>()
                .Property(u => u.Email)
                .HasMaxLength(50);

            modelBuilder.Entity<User>()
                .Property(u => u.PhoneNumber)
                .HasMaxLength(15);

            modelBuilder.Entity<Item>()
                .Property(i => i.Title)
                .HasMaxLength(100);

            modelBuilder.Entity<Item>()
                .Property(i => i.Description)
                .HasMaxLength(1000);

            modelBuilder.Entity<Item>()
                .Property(i => i.Price)
                .HasPrecision(18, 4);
            modelBuilder.Entity<Item>()
                .Property(i => i.City)
                .HasMaxLength(100);

            modelBuilder.Entity<Item>()
                .Property(i => i.Category)
                .HasMaxLength(100);

            modelBuilder.Entity<Item>()
                .HasOne(i => i.Seller)
                .WithMany(u => u.ItemsForSale)
                .HasForeignKey(i => i.SellerId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.Buyer)
                .WithMany(u => u.TransactionsAsBuyer)
                .HasForeignKey(t => t.BuyerId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.Seller)
                .WithMany(u => u.TransactionsAsSeller)
                .HasForeignKey(t => t.SellerId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.Item)
                .WithOne(i => i.Transaction)
                .HasForeignKey<Transaction>(t => t.ItemId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PickupDetails>()
                .HasOne(pd => pd.Transaction)
                .WithOne(t => t.PickupDetails)
                .HasForeignKey<PickupDetails>(pd => pd.TransactionId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Chat>()
                .HasOne(c => c.Item)
                .WithMany(i => i.Chats)
                .HasForeignKey(c => c.ItemId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Chat>()
                .HasOne(c => c.Buyer)
                .WithMany(u => u.ChatsAsBuyer)
                .HasForeignKey(c => c.BuyerId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Chat>()
                .HasOne(c => c.Seller)
                .WithMany(u => u.ChatsAsSeller)
                .HasForeignKey(c => c.SellerId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Message>()
                .HasOne(m => m.Chat)
                .WithMany(c => c.Messages)
                .HasForeignKey(m => m.ChatId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Message>()
                .HasOne(m => m.Sender)
                .WithMany(u => u.Messages)
                .HasForeignKey(m => m.SenderId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
