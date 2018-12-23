namespace Model.EF
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class OnlineShopDBContext : DbContext
    {
        public OnlineShopDBContext()
            : base("name=OnlineShopDBContext")
        {
        }

        public virtual DbSet<ChiTietDDH> ChiTietDDHs { get; set; }
        public virtual DbSet<Credential> Credentials { get; set; }
        public virtual DbSet<DanhMucSP> DanhMucSPs { get; set; }
        public virtual DbSet<DanhMucTinTuc> DanhMucTinTucs { get; set; }
        public virtual DbSet<DonDatHang> DonDatHangs { get; set; }
        public virtual DbSet<Foodter> Foodters { get; set; }
        public virtual DbSet<GioiThieu> GioiThieux { get; set; }
        public virtual DbSet<GopY> Gopies { get; set; }
        public virtual DbSet<LienHe> LienHes { get; set; }
        public virtual DbSet<Menu> Menus { get; set; }
        public virtual DbSet<MenuType> MenuTypes { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<SanPham> SanPhams { get; set; }
        public virtual DbSet<Slide> Slides { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }
        public virtual DbSet<TinTuc> TinTucs { get; set; }
        public virtual DbSet<TinTuc_Tag> TinTuc_Tag { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserGroup> UserGroups { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ChiTietDDH>()
                .Property(e => e.Price)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Credential>()
                .Property(e => e.UserGroupID)
                .IsUnicode(false);

            modelBuilder.Entity<Credential>()
                .Property(e => e.RoleID)
                .IsUnicode(false);

            modelBuilder.Entity<DanhMucSP>()
                .Property(e => e.MetaTitle)
                .IsUnicode(false);

            modelBuilder.Entity<DonDatHang>()
                .Property(e => e.ShipMobie)
                .IsUnicode(false);

            modelBuilder.Entity<DonDatHang>()
                .Property(e => e.ShipEmail)
                .IsUnicode(false);

            modelBuilder.Entity<Foodter>()
                .Property(e => e.FoodterID)
                .IsUnicode(false);

            modelBuilder.Entity<GioiThieu>()
                .Property(e => e.CreateBy)
                .IsUnicode(false);

            modelBuilder.Entity<Role>()
                .Property(e => e.RoleID)
                .IsUnicode(false);

            modelBuilder.Entity<SanPham>()
                .Property(e => e.Code)
                .IsUnicode(false);

            modelBuilder.Entity<SanPham>()
                .Property(e => e.Price)
                .HasPrecision(18, 0);

            modelBuilder.Entity<SanPham>()
                .Property(e => e.PromotionPrice)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Slide>()
                .Property(e => e.Image)
                .IsUnicode(false);

            modelBuilder.Entity<Slide>()
                .Property(e => e.Link)
                .IsUnicode(false);

            modelBuilder.Entity<Tag>()
                .Property(e => e.TagID)
                .IsUnicode(false);

            modelBuilder.Entity<TinTuc>()
                .Property(e => e.CreateBy)
                .IsUnicode(false);

            modelBuilder.Entity<TinTuc_Tag>()
                .Property(e => e.TagID)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.UserGroupID)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.UserName)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Phone)
                .IsUnicode(false);

            modelBuilder.Entity<UserGroup>()
                .Property(e => e.UserGroupID)
                .IsUnicode(false);
        }
    }
}
