using System.ComponentModel.DataAnnotations;
using CatStore.Data;
using Microsoft.EntityFrameworkCore;

namespace CatStore.Models;

/// <summary>
/// 猫咪品种模型类。
/// </summary>
[Index(nameof(BreedName), IsUnique = true)]
public class CatBreed
{
    public int Id { get; set; }

    [Required]
    [Display(Name = "品种名字")]
    [StringLength(10, MinimumLength = 1)]
    public string? BreedName { get; set; }

    [Display(Name = "备注")]
    [StringLength(50)]
    public string? Remark { get; set; }

    /// <summary>
    /// 此猫咪品种关联的猫咪。
    /// </summary>
    public virtual ICollection<CatInfo> Cats { get; set; } = new List<CatInfo>();

    /// <summary>
    /// 搜索名字中包含 value 的品种信息，应用在 MudBlazor 的自动完成文本框中。
    /// </summary>
    /// <param name="dbContext">数据库上下文。</param>
    /// <param name="value">品种名包含的字符。</param>
    /// <param name="token">取消令牌。</param>
    /// <returns>所有符合条件的品种信息。</returns>
    public static async Task<IEnumerable<CatBreed>> SearchCatBreed(
        CatStoreContext dbContext,
        string? value,
        CancellationToken token)
    {
        if (string.IsNullOrEmpty(value))
        {
            return await dbContext.CatBreed
                .Take(10)
                .ToListAsync(token);
        }

        return await dbContext.CatBreed
            .Where(c => c.BreedName!.Contains(value))
            .Take(10)
            .ToListAsync(token);
    }
}