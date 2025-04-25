using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace CatStore.Data;

/// <summary>
/// 应用中的用户.
/// </summary>
/// <remarks>
/// 通过继承 IdentityUser(AspNetCore Identity 身份系统中的用户) 获得一些默认的字段,比如用户名,电子邮件等.
/// 如果有其他需要的字段,可自行添加. 
/// </remarks>
public class ApplicationUser : IdentityUser
{
    [Required]
    [StringLength(20, MinimumLength = 2, ErrorMessage = "昵称需要 2~20 字符")]
    public string? Nickname { get; set; }
}