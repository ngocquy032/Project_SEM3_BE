using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Arts_be.Models;

public partial class EProjectContext : DbContext
{
    public EProjectContext()
    {
    }

    public EProjectContext(DbContextOptions<EProjectContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Blog> Blogs { get; set; }

    public virtual DbSet<BlogComment> BlogComments { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrdersDetail> OrdersDetails { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductImage> ProductImages { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=tcp:project-sem3.database.windows.net,1433;Initial Catalog=eProject;Persist Security Info=False;User ID=Project;Password=Dungdepzai123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Blog>(entity =>
        {
            entity.HasKey(e => e.BlogId).HasName("PK__Blog__C163EC10743B7662");

            entity.ToTable("Blog");

            entity.Property(e => e.BlogId).HasColumnName("Blog_id");
            entity.Property(e => e.Content).HasColumnType("text");
            entity.Property(e => e.Images).IsUnicode(false);
            entity.Property(e => e.NameCategory)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Name_Category");
            entity.Property(e => e.UserId).HasColumnName("User_id");

            entity.HasOne(d => d.User).WithMany(p => p.Blogs)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Blog__User_id__6A30C649");
        });

        modelBuilder.Entity<BlogComment>(entity =>
        {
            entity.HasKey(e => e.BlogCommentId).HasName("PK__Blog_Com__2F0E1F595BCADCF0");

            entity.ToTable("Blog_Comment");

            entity.Property(e => e.BlogCommentId).HasColumnName("Blog_Comment_ID");
            entity.Property(e => e.BlogId).HasColumnName("Blog_id");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Messages).HasColumnType("text");
            entity.Property(e => e.NameTitle)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Name_Title");
            entity.Property(e => e.UserId).HasColumnName("User_id");

            entity.HasOne(d => d.Blog).WithMany(p => p.BlogComments)
                .HasForeignKey(d => d.BlogId)
                .HasConstraintName("FK__Blog_Comm__Blog___6B24EA82");

            entity.HasOne(d => d.User).WithMany(p => p.BlogComments)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Blog_Comm__User___6C190EBB");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__Category__445A6543FCB836FD");

            entity.ToTable("Category");

            entity.Property(e => e.CategoryId).HasColumnName("Category_id");
            entity.Property(e => e.NameCategory)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Name_Category");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__Orders__F1FF8453CB5CA9C4");

            entity.Property(e => e.OrderId).HasColumnName("Order_id");
            entity.Property(e => e.Country).HasMaxLength(255);
            entity.Property(e => e.District).HasMaxLength(255);
            entity.Property(e => e.Email)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("First_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Last_Name");
            entity.Property(e => e.PaymentType)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Payment_type");
            entity.Property(e => e.Phone)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.StreetAdress)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("Street_Adress");
            entity.Property(e => e.Town).HasMaxLength(255);
            entity.Property(e => e.UserId).HasColumnName("User_id");

            entity.HasOne(d => d.User).WithMany(p => p.Orders)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Orders__User_id__6D0D32F4");
        });

        modelBuilder.Entity<OrdersDetail>(entity =>
        {
            entity.HasKey(e => e.OrderDetailsId).HasName("PK__Orders_D__F68B9B8ABF920C5C");

            entity.ToTable("Orders_Details");

            entity.Property(e => e.OrderDetailsId).HasColumnName("Order_Details_id");
            entity.Property(e => e.OrderId).HasColumnName("Order_id");
            entity.Property(e => e.OriginalPrice)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("Original_Price");
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.ProductId).HasColumnName("Product_id");
            entity.Property(e => e.Total).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Order).WithMany(p => p.OrdersDetails)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK__Orders_De__Order__6E01572D");

          /*  entity.HasOne(d => d.Product).WithMany(p => p.OrdersDetails)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__Orders_De__Produ__6EF57B66");*/
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__Product__9833FF9228181156");

            entity.ToTable("Product");

            entity.Property(e => e.ProductId).HasColumnName("Product_id");
            entity.Property(e => e.Availability)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.ProductCategoryId).HasColumnName("Product_category_id");
            entity.Property(e => e.ProductCode)
                .HasMaxLength(7)
                .IsUnicode(false)
                .HasColumnName("Product_code");
            entity.Property(e => e.Sale).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Tags)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Weight).HasColumnType("decimal(8, 2)");
        });

        modelBuilder.Entity<ProductImage>(entity =>
        {
            entity.HasKey(e => e.ProductImagesId).HasName("PK__Product___9A466DB08956967E");

            entity.ToTable("Product_images");

            entity.Property(e => e.ProductImagesId).HasColumnName("Product_images_id");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.Path)
                .IsUnicode(false)
                .HasColumnName("path");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__206A9DF8A2903AA5");

            entity.Property(e => e.UserId).HasColumnName("User_id");
            entity.Property(e => e.Avatar).IsUnicode(false);
            entity.Property(e => e.Country).HasMaxLength(255);
            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.District).HasMaxLength(255);
            entity.Property(e => e.Email)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.LastName).HasMaxLength(100);
            entity.Property(e => e.Level)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.Phone)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.PostcodeZip)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Postcode_zip");
            entity.Property(e => e.StreetAddress)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("Street_Address");
            entity.Property(e => e.Town).HasMaxLength(255);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        // Thực hiện các thay đổi cần thiết trước khi lưu
        // Ví dụ: Bạn có thể thêm logic validation hoặc manipulation ở đây

        // Gọi phương thức SaveChangesAsync từ lớp cha để lưu thay đổi vào cơ sở dữ liệu
        return base.SaveChangesAsync(cancellationToken);
    }


    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
