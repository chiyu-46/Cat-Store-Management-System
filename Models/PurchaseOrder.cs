using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CatStore.Models;

/// <summary>
/// 商品进货订单模型类。
/// </summary>
public class PurchaseOrder
{
    /// <summary>
    /// 订单号，作为主键。
    /// </summary>
    [Key]
    public int OrderId { get; set; }

    [Required]
    [Phone]
    [Display(Name = "供货商电话")]
    [StringLength(11)]
    public string? SupplierPhone { get; set; }
    
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
    public virtual ICollection<PurchaseOrderItem> PurchaseOrderItems { get; set; } = new List<PurchaseOrderItem>();
}

/// <summary>
/// 进货订单项模型类，每个订单项表示订单中的一种商品以及进货的数量。
/// </summary>
public class PurchaseOrderItem : OrderItemBase
{
    /// <summary>
    /// 所属订单的ID，作为外键。
    /// </summary>
    [Required]
    [ForeignKey(nameof(PurchaseOrder))]
    public int OrderId { get; set; }

    /// <summary>
    /// 关联的订单实体。
    /// </summary>
    public virtual PurchaseOrder PurchaseOrder { get; set; } = default!;
}