using System.ComponentModel.DataAnnotations;
using CatStore.Data;
using Microsoft.EntityFrameworkCore;

namespace CatStore.Models;

/// <summary>
/// 预约信息的模型类。
/// </summary>
public class Appointment
{
    public int Id { get; set; }

    [Required]
    [Display(Name = "顾客称呼")]
    [StringLength(10, MinimumLength = 1)]
    public string? CustomerName { get; set; }
    
    [Required]
    [Display(Name = "到达时间")]
    public DateTime ArrivalDateTime { get; set; }
    
    [Display(Name = "备注")]
    [StringLength(50, MinimumLength = 0)]
    public string? Remark { get; set; }
}