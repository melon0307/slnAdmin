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
        public virtual DbSet<AnswerTableDetail> AnswerTableDetails { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Coffee> Coffees { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Constellation> Constellations { get; set; }
        public virtual DbSet<Continent> Continents { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<Coupon> Coupons { get; set; }
        public virtual DbSet<CouponDetail> CouponDetails { get; set; }
        public virtual DbSet<Introduce> Introduces { get; set; }
        public virtual DbSet<IntroducePhoto> IntroducePhotos { get; set; }
        public virtual DbSet<Member> Members { get; set; }
        public virtual DbSet<MyLike> MyLikes { get; set; }
        public virtual DbSet<News> News { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<OrderState> OrderStates { get; set; }
        public virtual DbSet<Package> Packages { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }
        public virtual DbSet<Photo> Photos { get; set; }
        public virtual DbSet<PhotoDetail> PhotoDetails { get; set; }
        public virtual DbSet<Process> Processes { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Qquestionnaire> Qquestionnaires { get; set; }
        public virtual DbSet<QuestionTable> QuestionTables { get; set; }
        public virtual DbSet<QuestionTableDetail> QuestionTableDetails { get; set; }
        public virtual DbSet<Roasting> Roastings { get; set; }
        public virtual DbSet<ShoppingCar> ShoppingCars { get; set; }
        public virtual DbSet<ShoppingCarDetail> ShoppingCarDetails { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=Coffee;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Chinese_Taiwan_Stroke_CI_AS");

            modelBuilder.Entity<Admin>(entity =>
            {
                entity.ToTable("Admin");

                entity.Property(e => e.AdminId).HasColumnName("AdminID");

                entity.Property(e => e.AdminName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.AdminPassword)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<AnswerTableDetail>(entity =>
            {
                entity.HasKey(e => e.AnswerTableDetailsId);

                entity.Property(e => e.AnswerTableDetailsId).HasColumnName("AnswerTableDetailsID");

                entity.Property(e => e.Q1).HasMaxLength(50);

                entity.Property(e => e.Q2).HasMaxLength(50);

                entity.Property(e => e.Q3).HasMaxLength(50);

                entity.Property(e => e.Q4).HasMaxLength(50);

                entity.Property(e => e.Q5).HasMaxLength(50);

                entity.Property(e => e.Q6).HasMaxLength(50);

                entity.Property(e => e.Q7).HasMaxLength(50);

                entity.Property(e => e.QuestionTableDetailsId).HasColumnName("QuestionTableDetailsID");

                entity.HasOne(d => d.QuestionTableDetails)
                    .WithMany(p => p.AnswerTableDetails)
                    .HasForeignKey(d => d.QuestionTableDetailsId)
                    .HasConstraintName("FK_AnswerTableDetails_QuestionTableDetails");
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

                entity.Property(e => e.ConstellationId).HasColumnName("ConstellationID");

                entity.Property(e => e.CountryId).HasColumnName("CountryID");

                entity.Property(e => e.PackageId).HasColumnName("PackageID");

                entity.Property(e => e.ProcessId).HasColumnName("ProcessID");

                entity.Property(e => e.RoastingId).HasColumnName("RoastingID");

                entity.HasOne(d => d.Constellation)
                    .WithMany(p => p.Coffees)
                    .HasForeignKey(d => d.ConstellationId)
                    .HasConstraintName("FK_Coffee_Constellation");

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

                entity.Property(e => e.ConstellationName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ConstellationPhoto).HasColumnType("image");

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

            modelBuilder.Entity<Introduce>(entity =>
            {
                entity.HasKey(e => e.IntroducesId);

                entity.Property(e => e.IntroducesId).HasColumnName("IntroducesID");

                entity.Property(e => e.IntroducesName).HasMaxLength(50);
            });

            modelBuilder.Entity<IntroducePhoto>(entity =>
            {
                entity.HasKey(e => e.IntroducePhotosId);

                entity.Property(e => e.IntroducePhotosId).HasColumnName("IntroducePhotosID");

                entity.Property(e => e.IntroduceId).HasColumnName("IntroduceID");

                entity.Property(e => e.IntroducePhoto1)
                    .HasColumnType("image")
                    .HasColumnName("IntroducePhoto");

                entity.HasOne(d => d.Introduce)
                    .WithMany(p => p.IntroducePhotos)
                    .HasForeignKey(d => d.IntroduceId)
                    .HasConstraintName("FK_IntroducePhotos_Introduces");
            });

            modelBuilder.Entity<Member>(entity =>
            {
                entity.Property(e => e.MemberId).HasColumnName("MemberID");

                entity.Property(e => e.MemberAddress)
                    .IsRequired()
                    .HasMaxLength(50);

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

                entity.Property(e => e.MemberPhoto).HasColumnType("image");

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

            modelBuilder.Entity<News>(entity =>
            {
                entity.Property(e => e.NewsId).HasColumnName("NewsID");

                entity.Property(e => e.NewsOverTime).HasColumnType("date");

                entity.Property(e => e.NewsStartTime).HasColumnType("date");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.CouponId).HasColumnName("CouponID");

                entity.Property(e => e.MemberId).HasColumnName("MemberID");

                entity.Property(e => e.OrderAddress)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.OrderDate).HasColumnType("date");

                entity.Property(e => e.OrderStateId).HasColumnName("OrderStateID");

                entity.Property(e => e.PaymentId).HasColumnName("PaymentID");

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

                entity.Property(e => e.Photo1)
                    .HasColumnType("image")
                    .HasColumnName("Photo");
            });

            modelBuilder.Entity<PhotoDetail>(entity =>
            {
                entity.HasKey(e => e.PhotoDetailsId)
                    .HasName("PK_PhotoDetails_1");

                entity.Property(e => e.PhotoDetailsId).HasColumnName("PhotoDetailsID");

                entity.Property(e => e.PhotoId).HasColumnName("PhotoID");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.HasOne(d => d.Photo)
                    .WithMany(p => p.PhotoDetails)
                    .HasForeignKey(d => d.PhotoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PhotoDetails_Photos");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.PhotoDetails)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PhotoDetails_Products1");
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

            modelBuilder.Entity<Qquestionnaire>(entity =>
            {
                entity.ToTable("Qquestionnaire");

                entity.Property(e => e.QquestionnaireId).HasColumnName("QquestionnaireID");

                entity.Property(e => e.CouponId).HasColumnName("CouponID");

                entity.Property(e => e.MemberId).HasColumnName("MemberID");

                entity.Property(e => e.QuestionTableId).HasColumnName("QuestionTableID");

                entity.HasOne(d => d.Coupon)
                    .WithMany(p => p.Qquestionnaires)
                    .HasForeignKey(d => d.CouponId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Qquestionnaire_Coupon");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.Qquestionnaires)
                    .HasForeignKey(d => d.MemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Qquestionnaire_Members");

                entity.HasOne(d => d.QuestionTable)
                    .WithMany(p => p.Qquestionnaires)
                    .HasForeignKey(d => d.QuestionTableId)
                    .HasConstraintName("FK_Qquestionnaire_QuestionTable");
            });

            modelBuilder.Entity<QuestionTable>(entity =>
            {
                entity.ToTable("QuestionTable");

                entity.Property(e => e.QuestionTableId).HasColumnName("QuestionTableID");

                entity.Property(e => e.QuestionTableDetailsId).HasColumnName("QuestionTableDetailsID");

                entity.Property(e => e.QuestionTableName).HasMaxLength(50);

                entity.HasOne(d => d.QuestionTableDetails)
                    .WithMany(p => p.QuestionTables)
                    .HasForeignKey(d => d.QuestionTableDetailsId)
                    .HasConstraintName("FK_QuestionTable_QuestionTableDetails");
            });

            modelBuilder.Entity<QuestionTableDetail>(entity =>
            {
                entity.HasKey(e => e.QuestionTableDetailsId);

                entity.Property(e => e.QuestionTableDetailsId)
                    .ValueGeneratedNever()
                    .HasColumnName("QuestionTableDetailsID");

                entity.Property(e => e.A11)
                    .HasMaxLength(50)
                    .HasColumnName("A1-1");

                entity.Property(e => e.A12)
                    .HasMaxLength(50)
                    .HasColumnName("A1-2");

                entity.Property(e => e.A13)
                    .HasMaxLength(50)
                    .HasColumnName("A1-3");

                entity.Property(e => e.A14)
                    .HasMaxLength(50)
                    .HasColumnName("A1-4");

                entity.Property(e => e.A15)
                    .HasMaxLength(50)
                    .HasColumnName("A1-5");

                entity.Property(e => e.A21)
                    .HasMaxLength(50)
                    .HasColumnName("A2-1");

                entity.Property(e => e.A22)
                    .HasMaxLength(50)
                    .HasColumnName("A2-2");

                entity.Property(e => e.A23)
                    .HasMaxLength(50)
                    .HasColumnName("A2-3");

                entity.Property(e => e.A24)
                    .HasMaxLength(50)
                    .HasColumnName("A2-4");

                entity.Property(e => e.A25)
                    .HasMaxLength(50)
                    .HasColumnName("A2-5");

                entity.Property(e => e.A31)
                    .HasMaxLength(50)
                    .HasColumnName("A3-1");

                entity.Property(e => e.A32)
                    .HasMaxLength(50)
                    .HasColumnName("A3-2");

                entity.Property(e => e.A33)
                    .HasMaxLength(50)
                    .HasColumnName("A3-3");

                entity.Property(e => e.A34)
                    .HasMaxLength(50)
                    .HasColumnName("A3-4");

                entity.Property(e => e.A35)
                    .HasMaxLength(50)
                    .HasColumnName("A3-5");

                entity.Property(e => e.A36)
                    .HasMaxLength(50)
                    .HasColumnName("A3-6");

                entity.Property(e => e.A37)
                    .HasMaxLength(50)
                    .HasColumnName("A3-7");

                entity.Property(e => e.A41)
                    .HasMaxLength(50)
                    .HasColumnName("A4-1");

                entity.Property(e => e.A42)
                    .HasMaxLength(50)
                    .HasColumnName("A4-2");

                entity.Property(e => e.A43)
                    .HasMaxLength(50)
                    .HasColumnName("A4-3");

                entity.Property(e => e.A44)
                    .HasMaxLength(50)
                    .HasColumnName("A4-4");

                entity.Property(e => e.A51)
                    .HasMaxLength(50)
                    .HasColumnName("A5-1");

                entity.Property(e => e.A52)
                    .HasMaxLength(50)
                    .HasColumnName("A5-2");

                entity.Property(e => e.A53)
                    .HasMaxLength(50)
                    .HasColumnName("A5-3");

                entity.Property(e => e.A54)
                    .HasMaxLength(50)
                    .HasColumnName("A5-4");

                entity.Property(e => e.A61)
                    .HasMaxLength(50)
                    .HasColumnName("A6-1");

                entity.Property(e => e.A62)
                    .HasMaxLength(50)
                    .HasColumnName("A6-2");

                entity.Property(e => e.A63)
                    .HasMaxLength(50)
                    .HasColumnName("A6-3");

                entity.Property(e => e.A64)
                    .HasMaxLength(50)
                    .HasColumnName("A6-4");

                entity.Property(e => e.A65)
                    .HasMaxLength(50)
                    .HasColumnName("A6-5");

                entity.Property(e => e.A71)
                    .HasMaxLength(50)
                    .HasColumnName("A7-1");

                entity.Property(e => e.A72)
                    .HasMaxLength(50)
                    .HasColumnName("A7-2");

                entity.Property(e => e.A73)
                    .HasMaxLength(50)
                    .HasColumnName("A7-3");

                entity.Property(e => e.A74)
                    .HasMaxLength(50)
                    .HasColumnName("A7-4");

                entity.Property(e => e.A75)
                    .HasMaxLength(50)
                    .HasColumnName("A7-5");

                entity.Property(e => e.Q1).HasMaxLength(50);

                entity.Property(e => e.Q2).HasMaxLength(50);

                entity.Property(e => e.Q3).HasMaxLength(50);

                entity.Property(e => e.Q4).HasMaxLength(50);

                entity.Property(e => e.Q5).HasMaxLength(50);

                entity.Property(e => e.Q6).HasMaxLength(50);

                entity.Property(e => e.Q7).HasMaxLength(50);
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
