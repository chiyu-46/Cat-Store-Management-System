using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CatStore.Models;

/// <summary>
/// 商品售出订单模型类。
/// </summary>
public class CommodityOrder
{
    /// <summary>
    /// 订单号，作为主键。
    /// </summary>
    [Key]
    public int OrderId { get; set; }

    [Required]
    [Phone]
    [Display(Name = "顾客电话")]
    [StringLength(11)]
    public string? CustomerPhone { get; set; }
    
    /// <summary>
    /// 交易时间。
    /// </summary>
    [Required]
    [Display(Name = "交易时间")]
    public DateTime TransactionTime { get; set; }

    /// <summary>
    /// 订单总金额。
    /// </summary>
    [Required]
    [Display(Name = "总金额")]
    [Column(TypeName = "decimal(18, 2)")]
    public decimal TotalAmount { get; set; }

    /// <summary>
    /// 订单项集合，代表该订单中的所有商品及其数量。
    /// </summary>
    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}

/// <summary>
/// 订单项模型类，每个订单项表示订单中的一种商品以及购买的数量。
/// </summary>
public class OrderItem : OrderItemBase
{
    /// <summary>
    /// 所属订单的ID，作为外键。
    /// </summary>
    [Required]
    [ForeignKey(nameof(CommodityOrder))]
    public int OrderId { get; set; }

    /// <summary>
    /// 关联的订单实体。
    /// </summary>
    public virtual CommodityOrder CommodityOrder { get; set; } = default!;
}