using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CatStore.Models;

/// <summary>
/// 商品信息模型类。
/// </summary>
[Index(nameof(Name), IsUnique = true)]
public class Commodity
{
    public int Id { get; set; }
    
    [Required]
    [Display(Name = "品牌")]
    [StringLength(50, MinimumLength = 1)]
    public string? Brand{ get; set; }

    [Required]
    [Display(Name = "类型")]
    [EnumDataType(typeof(CommodityType))]
    public CommodityType CommodityType{ get; set; }
    
    [Required]
    [Display(Name = "商品名")]
    [StringLength(50, MinimumLength = 1)]
    public string? Name{ get; set; }
    
    [Required]
    [Display(Name = "库存数量")]
    public int QuantityInStock{ get; set; }
    
    [Required]
    [Display(Name = "单位")]
    [StringLength(10, MinimumLength = 1)]
    public string? Unit{ get; set; }
    
    /// <summary>
    /// 将数据库中存储的 CommodityType 类型的品种，转化成自然语言的品种描述。
    /// </summary>
    /// <param name="commodityType">来自数据库的品种原始数据。</param>
    /// <returns>自然语言的品种描述。</returns>
    public static string ConvertCommodityTypeToNaturalLanguage(CommodityType commodityType)
    {
        return commodityType switch
        {
            CommodityType.CatFood => "猫粮",
            CommodityType.CatSnack => "猫零食",
            CommodityType.CatToy => "猫玩具",
            CommodityType.CatMedicine => "猫用药物",
            _ => throw new ArgumentOutOfRangeException(nameof(CommodityType), commodityType, null)
        };
    }
    
    /// <summary>
    /// 本商品用自然语言描述的商品种类（只读）。
    /// </summary>
    [NotMapped]
    public string NaturalCommodityType => ConvertCommodityTypeToNaturalLanguage(CommodityType);
    
    /// <summary>
    /// 订单项集合，代表该商品关联的所有订单项。
    /// </summary>
    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    /// <summary>
    /// 进货订单项集合，代表该商品关联的所有进货订单项。
    /// </summary>
    public virtual ICollection<PurchaseOrderItem> PurchaseOrderItems { get; set; } = new List<PurchaseOrderItem>();

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

    public override bool Equals(object? obj)
    {
        if (obj is Commodity o)
        {
            return this.Id == o.Id;
        }

        return false;
    }
}

/// <summary>
/// 定义商品种类的枚举。
/// </summary>
public enum CommodityType
{
    /// <summary>
    /// 猫粮。
    /// </summary>
    CatFood,
    /// <summary>
    /// 猫零食。
    /// </summary>
    CatSnack,
    /// <summary>
    /// 猫用药物。
    /// </summary>
    CatMedicine,
    /// <summary>
    /// 猫玩具。
    /// </summary>
    CatToy,
}
