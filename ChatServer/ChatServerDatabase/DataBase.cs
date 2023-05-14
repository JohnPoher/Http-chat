using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace ChatServerDatabase
{
    public partial class DataBase : DbContext
    {
        public DataBase()
            : base("name=DataBase")
        {
        }

        public virtual DbSet<ChannelMessages> ChannelMessages { get; set; }
        public virtual DbSet<Channels> Channels { get; set; }
        public virtual DbSet<UserBlocks> UserBlocks { get; set; }
        public virtual DbSet<UserContacts> UserContacts { get; set; }
        public virtual DbSet<UserMessages> UserMessages { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Channels>()
                .HasMany(e => e.ChannelMessages)
                .WithRequired(e => e.Channels)
                .HasForeignKey(e => e.Channel)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Users>()
                .HasMany(e => e.ChannelMessages)
                .WithRequired(e => e.Users)
                .HasForeignKey(e => e.UserFrom)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Users>()
                .HasOptional(e => e.UserBlocks)
                .WithRequired(e => e.Users);

            modelBuilder.Entity<Users>()
                .HasOptional(e => e.UserContacts)
                .WithRequired(e => e.Users);

            modelBuilder.Entity<Users>()
                .HasOptional(e => e.UserMessages)
                .WithRequired(e => e.Users);

            modelBuilder.Entity<Users>()
                .HasMany(e => e.UserMessages1)
                .WithRequired(e => e.Users1)
                .HasForeignKey(e => e.UserTo)
                .WillCascadeOnDelete(false);
        }
    }
}
