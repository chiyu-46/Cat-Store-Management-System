using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CatStore.Models;

/// <summary>
/// 猫咪订单类。
/// </summary>
public class CatOrder
{
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
    
    [Required]
    [Display(Name = "金额")]
    [Column(TypeName = "decimal(18, 2)")]
    public decimal Amount { get; set; }

    // 外键关联到 CatInfo 模型
    [ForeignKey("CatInfo")]
    public int CatInfoId { get; set; }

    // 导航属性
    public virtual CatInfo CatInfo { get; set; } = default!;
}