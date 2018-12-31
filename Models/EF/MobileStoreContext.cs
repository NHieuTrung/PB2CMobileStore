namespace Models.EF
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class MobileStoreContext : DbContext
    {
        public MobileStoreContext()
            : base("name=MobileStoreContext")
        {
        }

        public virtual DbSet<AddressMember> AddressMembers { get; set; }
        public virtual DbSet<BrandType> BrandTypes { get; set; }
        public virtual DbSet<GenderType> GenderTypes { get; set; }
        public virtual DbSet<Import> Imports { get; set; }
        public virtual DbSet<ImportItem> ImportItems { get; set; }
        public virtual DbSet<Member> Members { get; set; }
        public virtual DbSet<MemberType> MemberTypes { get; set; }
        public virtual DbSet<PaymentType> PaymentTypes { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductAccessory> ProductAccessories { get; set; }
        public virtual DbSet<ProductPortableDevice> ProductPortableDevices { get; set; }
        public virtual DbSet<ProductType> ProductTypes { get; set; }
        public virtual DbSet<Promotion> Promotions { get; set; }
        public virtual DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public virtual DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
        public virtual DbSet<Store> Stores { get; set; }
        public virtual DbSet<StoreImei> StoreImeis { get; set; }
        public virtual DbSet<Vendor> Vendors { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AddressMember>()
                .HasMany(e => e.ShoppingCarts)
                .WithRequired(e => e.AddressMember)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<BrandType>()
                .HasMany(e => e.Products)
                .WithRequired(e => e.BrandType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ImportItem>()
                .Property(e => e.Price)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Member>()
                .Property(e => e.MemberEmail)
                .IsUnicode(false);

            modelBuilder.Entity<Member>()
                .Property(e => e.PhoneNumber)
                .IsUnicode(false);

            modelBuilder.Entity<Member>()
                .Property(e => e.FileImage)
                .IsUnicode(false);

            modelBuilder.Entity<Member>()
                .Property(e => e.MemberGoogleId)
                .IsUnicode(false);

            modelBuilder.Entity<Member>()
                .Property(e => e.MemberFacebookId)
                .IsUnicode(false);

            modelBuilder.Entity<Member>()
                .HasMany(e => e.Imports)
                .WithOptional(e => e.Member)
                .HasForeignKey(e => e.EmployeeId);

            modelBuilder.Entity<Member>()
                .HasMany(e => e.ShoppingCarts)
                .WithRequired(e => e.Member)
                .HasForeignKey(e => e.CustomerId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Member>()
                .HasMany(e => e.ShoppingCarts1)
                .WithOptional(e => e.Member1)
                .HasForeignKey(e => e.EmployeeId);

            modelBuilder.Entity<MemberType>()
                .HasMany(e => e.Members)
                .WithRequired(e => e.MemberType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PaymentType>()
                .HasMany(e => e.ShoppingCarts)
                .WithRequired(e => e.PaymentType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.FileImage)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.ProductAccessories)
                .WithRequired(e => e.Product)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.ProductPortableDevices)
                .WithRequired(e => e.Product)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ProductAccessory>()
                .Property(e => e.Color)
                .IsUnicode(false);

            modelBuilder.Entity<ProductAccessory>()
                .Property(e => e.Price)
                .HasPrecision(19, 4);

            modelBuilder.Entity<ProductAccessory>()
                .Property(e => e.ImageFile)
                .IsUnicode(false);

            modelBuilder.Entity<ProductAccessory>()
                .HasMany(e => e.ImportItems)
                .WithOptional(e => e.ProductAccessory)
                .HasForeignKey(e => e.ProductAccessoriesId);

            modelBuilder.Entity<ProductPortableDevice>()
                .Property(e => e.Color)
                .IsUnicode(false);

            modelBuilder.Entity<ProductPortableDevice>()
                .Property(e => e.Price)
                .HasPrecision(19, 4);

            modelBuilder.Entity<ProductPortableDevice>()
                .Property(e => e.FileImage)
                .IsUnicode(false);

            modelBuilder.Entity<ProductPortableDevice>()
                .HasMany(e => e.StoreImeis)
                .WithRequired(e => e.ProductPortableDevice)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ProductType>()
                .HasMany(e => e.Products)
                .WithRequired(e => e.ProductType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Promotion>()
                .Property(e => e.PromotionCode)
                .IsUnicode(false);

            modelBuilder.Entity<ShoppingCart>()
                .HasMany(e => e.ShoppingCartItems)
                .WithRequired(e => e.ShoppingCart)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ShoppingCartItem>()
                .Property(e => e.Price)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Store>()
                .Property(e => e.PhoneStore)
                .IsUnicode(false);

            modelBuilder.Entity<Store>()
                .Property(e => e.EmailStore)
                .IsUnicode(false);

            modelBuilder.Entity<StoreImei>()
                .Property(e => e.ImeiPhone)
                .IsUnicode(false);

            modelBuilder.Entity<Vendor>()
                .Property(e => e.PhoneNumber)
                .IsUnicode(false);

            modelBuilder.Entity<Vendor>()
                .Property(e => e.Email)
                .IsUnicode(false);
        }
    }
}
