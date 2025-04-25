using System.ComponentModel.DataAnnotations;

namespace CatStore.Models;

/// <summary>
/// 用于个人身份认证中 User 和 Role 的 DTO 。
/// </summary>
public class UserDto
{
    public string? UserName { get; set; }
    
    public string? Nickname { get; set; }

    public string? PhoneNumber { get; set; }
    
    public string? Password { get; set; }

    public required List<string> Roles { get; set; } 

    public static Dictionary<string, string> RoleNameDictionary { get; } = new()
    {
        { "admin", "管理员" },
        { "salesman", "销售员" },
    };
}