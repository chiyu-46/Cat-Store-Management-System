using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CatStore.Models;

// 配置 OrderItem 和 PurchaseOrderItem 的共同基类 OrderItemBase
// 使用 TPC（每个具体类型一张表）策略
// 即 OrderItem 和 PurchaseOrderItem 分别拥有一张表，分别拥有基类的全部属性
// 注意：TPC 要求同一基类的所有派生类的实体的主键值不同。
// 注意：SQL Server 可以自动处理上面的要求，但 SQLite 不行。详见：https://learn.microsoft.com/en-us/ef/core/modeling/inheritance
// 所以，SQLite 需要配合主键类型 GUID 使用 TPC。

public abstract class OrderItemBase
{
    /// <summary>
    /// 订单项ID，作为主键。
    /// </summary>
    [Key]
    public Guid ItemId { get; set; }
    
    /// <summary>
    /// 商品ID，来自`Commodity`类的`Id`属性，作为外键。
    /// </summary>
    [Required]
    [ForeignKey(nameof(Commodity))]
    public int CommodityId { get; set; }

    /// <summary>
    /// 关联的商品实体。
    /// </summary>
    public virtual Commodity Commodity { get; set; } = default!;

    /// <summary>
    /// 顾客购买的数量。
    /// </summary>
    [Required]
    public int Quantity { get; set; }
}