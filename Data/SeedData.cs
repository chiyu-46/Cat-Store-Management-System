using Microsoft.EntityFrameworkCore;
using CatStore.Models;
using Microsoft.AspNetCore.Identity;

namespace CatStore.Data;

/// <summary>
/// 用于在数据库表内无数据时，填充初始数据。
/// </summary>
public static class SeedData
{
    /// <summary>
    /// 应在程序初始化时调用，填充应用业务初始数据。
    /// </summary>
    /// <param name="serviceProvider">用于从依赖注入容器获取数据库上下文。</param>
    /// <exception cref="NullReferenceException">当数据库上下文或某数据表为空时，触发空引用异常。</exception>
    public static void AppDataInitialize(IServiceProvider serviceProvider)
    {
        // 从依赖注入容器获取数据库上下文。
        using var context = new CatStoreContext(
            serviceProvider.GetRequiredService<DbContextOptions<CatStoreContext>>());

        // 判断数据库上下文或数据表是否为空
        if (context?.CatInfo == null)
        {
            throw new NullReferenceException(
                "Null CatStoreContext or DbSet");
        }

        // 是否填充过数据的标志
        bool isChanged = false;

        List<CatBreed> catBreeds = [];

        // 如果数据库表 CatBreed 中没有数据，则填充以下数据
        if (!context.CatBreed.Any())
        {
            isChanged = true;
            catBreeds.AddRange(
                new CatBreed()
                {
                    BreedName = "美国短毛猫"
                },
                new CatBreed()
                {
                    BreedName = "英国短毛猫"
                },
                new CatBreed()
                {
                    BreedName = "中国狸花猫"
                },
                new CatBreed()
                {
                    BreedName = "缅因猫"
                },
                new CatBreed()
                {
                    BreedName = "波斯猫"
                },
                new CatBreed()
                {
                    BreedName = "布偶猫"
                });
            context.CatBreed.AddRange(catBreeds);
        }

        // 如果数据库表 CatInfo 中没有数据，则填充以下数据
        if (!context.CatInfo.Any())
        {
            isChanged = true;
            context.CatInfo.AddRange(
                new CatInfo()
                {
                    Name = "小白",
                    Gender = true,
                    CatBreed = catBreeds[0],
                    Birthday = new DateOnly(2022, 4, 12),
                    CatState = CatState.ForSale,
                },
                new CatInfo()
                {
                    Name = "小花",
                    Gender = true,
                    CatBreed = catBreeds[2],
                    Birthday = new DateOnly(2025, 2, 12),
                    CatState = CatState.ForSale,
                },
                new CatInfo()
                {
                    Name = "球球",
                    Gender = false,
                    CatBreed = catBreeds[5],
                    Birthday = new DateOnly(2022, 10, 6),
                    CatState = CatState.ForSale,
                },
                new CatInfo()
                {
                    Name = "香子兰",
                    Gender = true,
                    CatBreed = catBreeds[4],
                    Birthday = new DateOnly(2024, 3, 2),
                    CatState = CatState.ForSale,
                },
                new CatInfo()
                {
                    Name = "巧克力",
                    Gender = true,
                    CatBreed = catBreeds[4],
                    Birthday = new DateOnly(2022, 5, 1),
                    CatState = CatState.ForSale,
                });
        }

        // 如果数据库表 Commodity 中没有数据，则填充以下数据
        if (!context.Commodity.Any())
        {
            isChanged = true;
            context.Commodity.AddRange(
                new Commodity()
                {
                    Brand = "皇家 (Royal Canin)",
                    CommodityType = CommodityType.CatFood,
                    Name = "皇家室内成猫粮",
                    QuantityInStock = 50,
                    Unit = "袋",
                },
                new Commodity()
                {
                    Brand = "希尔斯 (Hill's)",
                    CommodityType = CommodityType.CatFood,
                    Name = "希尔斯科学配方幼猫粮",
                    QuantityInStock = 30,
                    Unit = "袋",
                },
                new Commodity()
                {
                    Brand = "喵达 (Meow Mix)",
                    CommodityType = CommodityType.CatSnack,
                    Name = "喵达三文鱼味猫条",
                    QuantityInStock = 100,
                    Unit = "包",
                },
                new Commodity()
                {
                    Brand = "宠确幸 (Pet House)",
                    CommodityType = CommodityType.CatToy,
                    Name = "宠确幸逗猫棒",
                    QuantityInStock = 20,
                    Unit = "个",
                },
                new Commodity()
                {
                    Brand = "福来恩 (Frontline)",
                    CommodityType = CommodityType.CatMedicine,
                    Name = "福来恩体外驱虫滴剂",
                    QuantityInStock = 15,
                    Unit = "盒",
                },
                new Commodity()
                {
                    Brand = "渴望 (Orijen)",
                    CommodityType = CommodityType.CatFood,
                    Name = "渴望六种鱼全猫粮",
                    QuantityInStock = 25,
                    Unit = "袋",
                },
                new Commodity()
                {
                    Brand = "猫乐适 (Catit)",
                    CommodityType = CommodityType.CatToy,
                    Name = "猫乐适电动逗猫球",
                    QuantityInStock = 12,
                    Unit = "个",
                },
                new Commodity()
                {
                    Brand = "绿十字 (Vet's Best)",
                    CommodityType = CommodityType.CatMedicine,
                    Name = "绿十字猫草片",
                    QuantityInStock = 40,
                    Unit = "瓶",
                },
                new Commodity()
                {
                    Brand = "顽皮 (Wanpy)",
                    CommodityType = CommodityType.CatSnack,
                    Name = "顽皮鸡肉味猫饼干",
                    QuantityInStock = 80,
                    Unit = "罐",
                },
                new Commodity()
                {
                    Brand = "小佩 (PETKIT)",
                    CommodityType = CommodityType.CatToy,
                    Name = "小佩智能饮水机",
                    QuantityInStock = 10,
                    Unit = "台",
                });
        }
        
        // 如果数据库表 Appointment 中没有数据，则填充以下数据
        if (!context.Appointment.Any())
        {
            isChanged = true;
            context.Appointment.AddRange(
                new Appointment()
                {
                    CustomerName = "张先生",
                    ArrivalDateTime = DateTime.Now + TimeSpan.FromMinutes(30),
                    Remark = "看猫"
                },
                new Appointment()
                {
                    CustomerName = "王女士",
                    ArrivalDateTime = DateTime.Now + TimeSpan.FromMinutes(60),
                    Remark = "买猫粮"
                },
                new Appointment()
                {
                    CustomerName = "李先生",
                    ArrivalDateTime = DateTime.Now + TimeSpan.FromMinutes(90),
                    Remark = "买逗猫棒"
                },
                new Appointment()
                {
                    CustomerName = "廖大爷",
                    ArrivalDateTime = DateTime.Now + TimeSpan.FromMinutes(120),
                    Remark = "看猫"
                });
        }

        // 若填充过任何数据，则保存更改
        if (isChanged)
        {
            context.SaveChanges();
        }
    }

    /// <summary>
    /// 应在程序初始化时调用，填充个人身份验证相关初始数据。
    /// </summary>
    /// <param name="serviceProvider">用于从依赖注入容器获取数据库上下文。</param>
    /// <exception cref="NullReferenceException">当数据库上下文或某数据表为空时，触发空引用异常。</exception>
    public static async Task IndividualAuthenticationInitialize(IServiceProvider serviceProvider)
    {
        using var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        IdentityResult result;

        // 若数据库中不存在 Role，则填充数据
        if (!roleManager.Roles.Any())
        {
            result = await roleManager.CreateAsync(new IdentityRole("admin"));
            CheckResult("创建角色admin");
            result = await roleManager.CreateAsync(new IdentityRole("salesman"));
            CheckResult("创建角色salesman");
        }

        using var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

        // 若数据库中不存在用户，则填充数据
        if (!userManager.Users.Any())
        {
            var adminUser = new ApplicationUser()
            {
                UserName = "xiaohe",
                Nickname = "小何",
                PhoneNumber = "13423232323",
                PhoneNumberConfirmed = true
            };
            result = await userManager.CreateAsync(adminUser, "Aa.123456");
            CheckResult("创建用户1");
            result = await userManager.AddToRoleAsync(adminUser, "admin");
            CheckResult("用户1配置角色");

            var salesmanUser = new ApplicationUser()
            {
                UserName = "xiaoshi",
                Nickname = "小石",
                PhoneNumber = "18923232323",
                PhoneNumberConfirmed = true
            };
            result = await userManager.CreateAsync(salesmanUser, "Aa.123456");
            CheckResult("创建用户2");
            result = await userManager.AddToRoleAsync(salesmanUser, "salesman");
            CheckResult("用户2配置角色");
        }

        return;

        void CheckResult(string taskDescription)
        {
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    Console.WriteLine($"{taskDescription}时发生异常，错误码{error.Code}：{error.Description}");
                }
            }
        }
    }
}