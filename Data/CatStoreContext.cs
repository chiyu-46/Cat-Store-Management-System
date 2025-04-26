using Microsoft.EntityFrameworkCore;
using CatStore.Models;

namespace CatStore.Data
{
    public class CatStoreContext(DbContextOptions<CatStoreContext> options) : DbContext(options)
    {
        public DbSet<CatBreed> CatBreed { get; set; } = default!;
        public DbSet<CatInfo> CatInfo { get; set; } = default!;
        public DbSet<Commodity> Commodity { get; set; } = default!;
        public DbSet<CatOrder> CatOrder { get; set; } = default!;
        public DbSet<CommodityOrder> CommodityOrder { get; set; } = default!;
        public DbSet<OrderItem> OrderItem { get; set; } = default!;
        public DbSet<PurchaseOrder> PurchaseOrder { get; set; } = default!;
        public DbSet<PurchaseOrderItem> PurchaseOrderItem { get; set; } = default!;
        public DbSet<Appointment> Appointment { get; set; } = default!;

        // 懒加载的可能带来性能问题，放弃使用
        // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        // {
        //     base.OnConfiguring(optionsBuilder);
        //     // 为关联属性启用懒加载（关联属性必须为 virtual ）
        //     optionsBuilder.UseLazyLoadingProxies();
        // }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // 配置 CatBreed 和 CatInfo 的关系（猫咪种类与猫咪）
            modelBuilder.Entity<CatBreed>()
                .HasMany(e => e.Cats)
                .WithOne(e => e.CatBreed)
                .HasForeignKey(e => e.CatBreedId)
                .OnDelete(DeleteBehavior.Cascade);
            // 配置 CatOrder 和 CatInfo 的关系（猫咪订单与猫咪）
            modelBuilder.Entity<CatInfo>()
                .HasMany(e => e.CatOrders)
                .WithOne(e => e.CatInfo)
                .HasForeignKey(e => e.CatInfoId)
                .OnDelete(DeleteBehavior.Cascade);
            // 配置 CommodityOrder 和 OrderItem 的关系（商品订单与商品订单项）
            modelBuilder.Entity<CommodityOrder>()
                .HasMany(e => e.OrderItems)
                .WithOne(e => e.CommodityOrder)
                .HasForeignKey(e => e.OrderId)
                .OnDelete(DeleteBehavior.Cascade);
            // 配置 Commodity 和 OrderItem 的关系（商品与商品订单项）
            modelBuilder.Entity<Commodity>()
                .HasMany(e => e.OrderItems)
                .WithOne(e => e.Commodity)
                .HasForeignKey(e => e.CommodityId);
            // 配置 PurchaseOrder 和 PurchaseOrderItem 的关系（商品进货订单与商品进货订单项）
            modelBuilder.Entity<PurchaseOrder>()
                .HasMany(e => e.PurchaseOrderItems)
                .WithOne(e => e.PurchaseOrder)
                .HasForeignKey(e => e.OrderId)
                .OnDelete(DeleteBehavior.Cascade);
            // 配置 Commodity 和 PurchaseOrderItem 的关系（商品与商品进货订单项）
            modelBuilder.Entity<Commodity>()
                .HasMany(e => e.PurchaseOrderItems)
                .WithOne(e => e.Commodity)
                .HasForeignKey(e => e.CommodityId);
            // 配置 OrderItem 和 PurchaseOrderItem 的共同基类 OrderItemBase
            // 使用 TPC（每个具体类型一张表）策略
            // 即 OrderItem 和 PurchaseOrderItem 分别拥有一张表，分别拥有基类的全部属性
            // 注意：TPC 要求同一基类的所有派生类的实体的主键值不同。
            // 注意：SQL Server 可以自动处理上面的要求，但 SQLite 不行。详见：https://learn.microsoft.com/en-us/ef/core/modeling/inheritance
            // 所以，SQLite 需要配合主键类型 GUID 使用 TPC。
            modelBuilder.Entity<OrderItemBase>().UseTpcMappingStrategy();
        }
    }
}
