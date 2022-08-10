using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CommonUtility.DatabaseEntity
{
    public partial class DigitalBookDBContext : DbContext
    {
        public DigitalBookDBContext()
        {
        }

        public DigitalBookDBContext(DbContextOptions<DigitalBookDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Author> Authors { get; set; } = null!;
        public virtual DbSet<Book> Books { get; set; } = null!;
        public virtual DbSet<Payment> Payments { get; set; } = null!;

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//                optionsBuilder.UseSqlServer("Data Source=CTSDOTNET648;Initial Catalog=DigitalBookDB;User ID=sa;Password=pass@word1");
//            }
//        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>(entity =>
            {
                entity.HasIndex(e => e.AuthorName, "UQ__Authors__9B43168737A7B3E7")
                    .IsUnique();

                entity.Property(e => e.AuthorId).HasColumnName("Author_Id");

                entity.Property(e => e.AuthorName)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("Author_Name");

                entity.Property(e => e.AuthorPassword)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Author_Password");
            });

            modelBuilder.Entity<Book>(entity =>
            {
                entity.HasIndex(e => e.Title, "UQ__Books__2CB664DC685786C7")
                    .IsUnique();

                entity.Property(e => e.BookId).HasColumnName("Book_Id");

                entity.Property(e => e.AuthorName)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("Author_Name");

                entity.Property(e => e.Category)
                    .HasMaxLength(55)
                    .IsUnicode(false);

                entity.Property(e => e.Content).HasColumnType("ntext");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Created_Date");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Modified_Date");

                entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.PublishedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Published_Date");

                entity.Property(e => e.Publisher)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Title)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.HasOne(d => d.AuthorNameNavigation)
                    .WithMany(p => p.Books)
                    .HasPrincipalKey(p => p.AuthorName)
                    .HasForeignKey(d => d.AuthorName)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Books__Author_Na__286302EC");
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.HasIndex(e => e.BuyerName, "UQ__Payments__59C2BAD8582A1722")
                    .IsUnique();

                entity.Property(e => e.PaymentId).HasColumnName("Payment_Id");

                entity.Property(e => e.BookId).HasColumnName("Book_id");

                entity.Property(e => e.BuyerEmailId)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Buyer_Email_Id");

                entity.Property(e => e.BuyerName)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Buyer_Name");

                entity.Property(e => e.PaymentDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Payment_Date");

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.Payments)
                    .HasForeignKey(d => d.BookId)
                    .HasConstraintName("FK__Payments__Book_i__2C3393D0");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
