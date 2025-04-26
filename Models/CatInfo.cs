using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CatStore.Models;

/// <summary>
/// 猫咪信息模型类。
/// </summary>
[Index(nameof(Name), IsUnique = true)]
public class CatInfo
{
    public int Id { get; set; }

    [Required]
    [Display(Name = "名字")]
    [StringLength(10, MinimumLength = 2)]
    public string? Name { get; set; }

    /// <summary>
    /// 性别。true为雌性。
    /// </summary>
    [Required]
    [Display(Name = "性别")]
    public bool Gender { get; set; }
    
    // 外键关联到 CatBreed 模型
    [ForeignKey("CatBreed")]
    public int CatBreedId { get; set; }

    // 导航属性
    public virtual CatBreed CatBreed { get; set; } = default!;

    [Display(Name = "出生日")] public DateOnly Birthday { get; set; }

    [Required]
    [Display(Name = "状态")]
    [EnumDataType(typeof(CatState))]
    public CatState CatState { get; set; }

    /// <summary>
    /// 带有时间的出生日。（为解决 MudDatePicker 不支持 DateOnly 而存在，不属于数据库字段。）
    /// </summary>
    [NotMapped]
    public DateTime? BirthdayWithTime
    {
        get => Birthday.ToDateTime(TimeOnly.MinValue);
        set
        {
            if (value != null)
            {
                Birthday = DateOnly.FromDateTime((DateTime)value);
            }
        }
    }

    /// <summary>
    /// 此猫咪关联的订单。
    /// </summary>
    public virtual ICollection<CatOrder> CatOrders { get; set; } = new List<CatOrder>();

    /// <summary>
    /// 用于表单中 Birthday 字段的验证器，确保出生日不会晚于今天。
    /// </summary>
    /// <param name="dateTime">用户在 MudDatePicker 中输入的出生日新值。</param>
    /// <returns>如果验证未通过，则返回错误信息列表。</returns>
    public static IEnumerable<string> ValidateBirthday(DateTime? dateTime)
    {
        if (dateTime != null)
        {
            if (dateTime > DateTime.Now)
            {
                yield return "出生日不可能超过今天！";
            }
        }
    }

    /// <summary>
    /// 将数据库中存储的 bool 类型的性别，转化成自然语言的性别描述。
    /// </summary>
    /// <param name="gender">来自数据库的性别原始数据。</param>
    /// <returns>自然语言的性别描述。</returns>
    public static string ConvertGenderToNaturalLanguage(bool gender) => gender ? "♀" : "♂";

    /// <summary>
    /// 将数据库中存储的 CatState 类型的状态，转化成自然语言的状态描述。
    /// </summary>
    /// <param name="catState">来自数据库的状态原始数据。</param>
    /// <returns>自然语言的状态描述。</returns>
    public static string ConvertCatStateToNaturalLanguage(CatState catState)
    {
        return catState switch
        {
            CatState.ForSale => "待售",
            CatState.Sold => "已售",
            _ => throw new ArgumentOutOfRangeException(nameof(catState), catState, null)
        };
    }

    /// <summary>
    /// 得到本实例代表的猫咪用自然语言描述的性别。
    /// </summary>
    /// <returns>用自然语言描述的性别。</returns>
    public string GetNaturalGender()
    {
        return ConvertGenderToNaturalLanguage(Gender);
    }

    /// <summary>
    /// 得到本实例代表的猫咪用自然语言描述的状态。
    /// </summary>
    /// <returns>用自然语言描述的状态。</returns>
    public string GetNaturalCatState()
    {
        return ConvertCatStateToNaturalLanguage(CatState);
    }
}

/// <summary>
/// 猫咪的状态。
/// </summary>
public enum CatState
{
    /// <summary>
    /// 待售。
    /// </summary>
    ForSale,

    /// <summary>
    /// 已售。
    /// </summary>
    Sold,
}