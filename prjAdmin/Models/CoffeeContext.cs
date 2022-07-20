using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace prjAdmin.Models
{
    public partial class CoffeeContext : DbContext
    {
        public CoffeeContext()
        {
        }

        public CoffeeContext(DbContextOptions<CoffeeContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Admin> Admins { get; set; }
        public virtual DbSet<Article> Articles { get; set; }
        public virtual DbSet<Awesome> Awesomes { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Coffee> Coffees { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Constellation> Constellations { get; set; }
        public virtual DbSet<Continent> Continents { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<Coupon> Coupons { get; set; }
        public virtual DbSet<CouponDetail> CouponDetails { get; set; }
        public virtual DbSet<Member> Members { get; set; }
        public virtual DbSet<MyLike> MyLikes { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<OrderState> OrderStates { get; set; }
        public virtual DbSet<Package> Packages { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }
        public virtual DbSet<Photo> Photos { get; set; }
        public virtual DbSet<Process> Processes { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<QuestionTable> QuestionTables { get; set; }
        public virtual DbSet<Roasting> Roastings { get; set; }
        public virtual DbSet<ShoppingCar> ShoppingCars { get; set; }
        public virtual DbSet<ShoppingCarDetail> ShoppingCarDetails { get; set; }

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//                optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=Coffee;Integrated Security=True");
//            }
//        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Chinese_Taiwan_Stroke_CI_AS");

            modelBuilder.Entity<Admin>(entity =>
            {
                entity.ToTable("Admin");

                entity.Property(e => e.AdminId).HasColumnName("AdminID");

                entity.Property(e => e.AdminOk).HasColumnName("AdminOK");

                entity.Property(e => e.ArticleOk).HasColumnName("ArticleOK");

                entity.Property(e => e.Email).IsRequired();

                entity.Property(e => e.MemberOk).HasColumnName("MemberOK");

                entity.Property(e => e.OrderOk).HasColumnName("OrderOK");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ProductOk).HasColumnName("ProductOK");
            });

            modelBuilder.Entity<Article>(entity =>
            {
                entity.ToTable("Article");

                entity.Property(e => e.ArticleDate).HasColumnType("date");

                entity.Property(e => e.ArticleName).HasMaxLength(50);
            });

            modelBuilder.Entity<Awesome>(entity =>
            {
                entity.ToTable("Awesome");

                entity.Property(e => e.CommentId).HasColumnName("CommentID");

                entity.Property(e => e.MemberId).HasColumnName("MemberID");

                entity.HasOne(d => d.Comment)
                    .WithMany(p => p.Awesomes)
                    .HasForeignKey(d => d.CommentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Awesome_Comment");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.CategoriesName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Coffee>(entity =>
            {
                entity.HasKey(e => e.ProductId)
                    .HasName("PK_Coffee_1");

                entity.ToTable("Coffee");

                entity.Property(e => e.ProductId)
                    .ValueGeneratedNever()
                    .HasColumnName("ProductID");

                entity.Property(e => e.CoffeeId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("CoffeeID");

                entity.Property(e => e.CoffeeName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CountryId).HasColumnName("CountryID");

                entity.Property(e => e.PackageId).HasColumnName("PackageID");

                entity.Property(e => e.ProcessId).HasColumnName("ProcessID");

                entity.Property(e => e.RoastingId).HasColumnName("RoastingID");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Coffees)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Coffee_Country");

                entity.HasOne(d => d.Package)
                    .WithMany(p => p.Coffees)
                    .HasForeignKey(d => d.PackageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Coffee_Package");

                entity.HasOne(d => d.Process)
                    .WithMany(p => p.Coffees)
                    .HasForeignKey(d => d.ProcessId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Coffee_Process");

                entity.HasOne(d => d.Product)
                    .WithOne(p => p.Coffee)
                    .HasForeignKey<Coffee>(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Coffee_Products1");

                entity.HasOne(d => d.Roasting)
                    .WithMany(p => p.Coffees)
                    .HasForeignKey(d => d.RoastingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Coffee_Roasting1");
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.ToTable("Comment");

                entity.Property(e => e.CommentId).HasColumnName("CommentID");

                entity.Property(e => e.CommentDescription).HasMaxLength(50);

                entity.Property(e => e.CommentParentId)
                    .HasColumnName("CommentParentID")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.MemberId).HasColumnName("MemberID");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.MemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Comment_Members");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Comment_Orders");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Comment_Products");
            });

            modelBuilder.Entity<Constellation>(entity =>
            {
                entity.ToTable("Constellation");

                entity.Property(e => e.ConstellationId).HasColumnName("ConstellationID");

                entity.Property(e => e.ConstellationDate).HasMaxLength(50);

                entity.Property(e => e.ConstellationName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ConstellationProductId).HasColumnName("ConstellationProductID");
            });

            modelBuilder.Entity<Continent>(entity =>
            {
                entity.ToTable("Continent");

                entity.Property(e => e.ContinentId).HasColumnName("ContinentID");

                entity.Property(e => e.ContinentName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.ToTable("Country");

                entity.Property(e => e.CountryId).HasColumnName("CountryID");

                entity.Property(e => e.ContinentId).HasColumnName("ContinentID");

                entity.Property(e => e.CountryName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Continent)
                    .WithMany(p => p.Countries)
                    .HasForeignKey(d => d.ContinentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Country_Continent1");
            });

            modelBuilder.Entity<Coupon>(entity =>
            {
                entity.ToTable("Coupon");

                entity.Property(e => e.CouponId).HasColumnName("CouponID");

                entity.Property(e => e.CouponDeadline).HasColumnType("date");

                entity.Property(e => e.CouponName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CouponStartDate).HasColumnType("date");

                entity.Property(e => e.Money).HasColumnType("money");
            });

            modelBuilder.Entity<CouponDetail>(entity =>
            {
                entity.HasKey(e => e.CouponDetailsId);

                entity.ToTable("CouponDetail");

                entity.Property(e => e.CouponDetailsId).HasColumnName("CouponDetailsID");

                entity.Property(e => e.CouponId).HasColumnName("CouponID");

                entity.Property(e => e.MemberId).HasColumnName("MemberID");

                entity.HasOne(d => d.Coupon)
                    .WithMany(p => p.CouponDetails)
                    .HasForeignKey(d => d.CouponId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CouponDetail_Coupon");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.CouponDetails)
                    .HasForeignKey(d => d.MemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CouponDetail_Members");
            });

            modelBuilder.Entity<Member>(entity =>
            {
                entity.Property(e => e.MemberId).HasColumnName("MemberID");

                entity.Property(e => e.MemberAddress).HasMaxLength(50);

                entity.Property(e => e.MemberBirthDay).HasColumnType("date");

                entity.Property(e => e.MemberEmail)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("MemberEMail");

                entity.Property(e => e.MemberName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.MemberPassword)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.MemberPhone)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ShoppingCarId).HasColumnName("ShoppingCarID");
            });

            modelBuilder.Entity<MyLike>(entity =>
            {
                entity.HasKey(e => e.LikeId);

                entity.ToTable("MyLike");

                entity.Property(e => e.LikeId).HasColumnName("LikeID");

                entity.Property(e => e.MemberId).HasColumnName("MemberID");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.MyLikes)
                    .HasForeignKey(d => d.MemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MyLike_Members");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.MyLikes)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MyLike_Products");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.CouponId).HasColumnName("CouponID");

                entity.Property(e => e.Fee).HasColumnType("money");

                entity.Property(e => e.MemberId).HasColumnName("MemberID");

                entity.Property(e => e.OrderAddress)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.OrderDate).HasColumnType("date");

                entity.Property(e => e.OrderPhone)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.OrderReceiver)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.OrderStateId).HasColumnName("OrderStateID");

                entity.Property(e => e.PaymentId).HasColumnName("PaymentID");

                entity.Property(e => e.TradeNo)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Coupon)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CouponId)
                    .HasConstraintName("FK_Orders_Coupon");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.MemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Orders_Members");

                entity.HasOne(d => d.OrderState)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.OrderStateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Orders_OrderStates");

                entity.HasOne(d => d.Payment)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.PaymentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Orders_Payments");
            });

            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.HasKey(e => e.OrderDetailsId);

                entity.Property(e => e.OrderDetailsId).HasColumnName("OrderDetailsID");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderDetails_Orders1");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderDetails_Products1");
            });

            modelBuilder.Entity<OrderState>(entity =>
            {
                entity.Property(e => e.OrderStateId).HasColumnName("OrderStateID");

                entity.Property(e => e.OrderState1)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("OrderState");
            });

            modelBuilder.Entity<Package>(entity =>
            {
                entity.ToTable("Package");

                entity.Property(e => e.PackageId).HasColumnName("PackageID");

                entity.Property(e => e.PackageName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.Property(e => e.PaymentId).HasColumnName("PaymentID");

                entity.Property(e => e.Payment1)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("Payment");
            });

            modelBuilder.Entity<Photo>(entity =>
            {
                entity.Property(e => e.PhotoId).HasColumnName("PhotoID");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Photos)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_Photos_Products");
            });

            modelBuilder.Entity<Process>(entity =>
            {
                entity.ToTable("Process");

                entity.Property(e => e.ProcessId).HasColumnName("ProcessID");

                entity.Property(e => e.ProcessName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.CountryId).HasColumnName("CountryID");

                entity.Property(e => e.Price).HasColumnType("money");

                entity.Property(e => e.ProductName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Products_Categories");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.CountryId)
                    .HasConstraintName("FK_Products_Country");
            });

            modelBuilder.Entity<QuestionTable>(entity =>
            {
                entity.ToTable("QuestionTable");

                entity.Property(e => e.QuestionTableId).HasColumnName("QuestionTableID");

                entity.Property(e => e.QuestionTableDetailsId).HasColumnName("QuestionTableDetailsID");

                entity.Property(e => e.QuestionTableName).HasMaxLength(50);
            });

            modelBuilder.Entity<Roasting>(entity =>
            {
                entity.ToTable("Roasting");

                entity.Property(e => e.RoastingId).HasColumnName("RoastingID");

                entity.Property(e => e.RoastingName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<ShoppingCar>(entity =>
            {
                entity.HasKey(e => e.MemberId);

                entity.ToTable("ShoppingCar");

                entity.Property(e => e.MemberId)
                    .ValueGeneratedNever()
                    .HasColumnName("MemberID");

                entity.Property(e => e.ShoppinCarId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ShoppinCarID");

                entity.HasOne(d => d.Member)
                    .WithOne(p => p.ShoppingCar)
                    .HasForeignKey<ShoppingCar>(d => d.MemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ShoppingCar_Members");
            });

            modelBuilder.Entity<ShoppingCarDetail>(entity =>
            {
                entity.HasKey(e => e.ShoppingCarDetialsId);

                entity.ToTable("ShoppingCarDetail");

                entity.Property(e => e.ShoppingCarDetialsId).HasColumnName("ShoppingCarDetialsID");

                entity.Property(e => e.MemberId).HasColumnName("MemberID");

                entity.Property(e => e.Price).HasColumnType("money");

                entity.Property(e => e.ProductsId).HasColumnName("ProductsID");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.ShoppingCarDetails)
                    .HasForeignKey(d => d.MemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ShoppingCarDetail_ShoppingCar");

                entity.HasOne(d => d.Products)
                    .WithMany(p => p.ShoppingCarDetails)
                    .HasForeignKey(d => d.ProductsId)
                    .HasConstraintName("FK_ShoppingCarDetail_Products");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
