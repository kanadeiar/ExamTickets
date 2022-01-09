namespace ExamTickets.Dal.Data;

public static class IdentitySeedTestData
{
    public static async void SeedTestData(IServiceProvider provider, IConfiguration configuration, ILogger logger)
    {
        provider = provider.CreateScope().ServiceProvider;
        await using var context = new IdentityContext(provider.GetRequiredService<DbContextOptions<IdentityContext>>());
        if (context == null || context.Users == null)
        {
            logger.LogError("Null IdentityContext");
            throw new ArgumentNullException("Null IdentityContext");
        }
        var pendingMigrations = await context.Database.GetPendingMigrationsAsync();
        if (pendingMigrations.Any())
        {
            logger.LogInformation($"Applying migrations Identity: {string.Join(",", pendingMigrations)}");
            await context.Database.MigrateAsync();
        }
        if (context.Users.Any())
        {
            logger.LogInformation("Identity database contains data - database init with test data is not required");
            return;
        }
        logger.LogInformation("Begin writing test data to database CorporationContext ...");

        #region Identity

        UserManager<User> userManager = provider.GetRequiredService<UserManager<User>>();
        RoleManager<Role> roleManager = provider.GetRequiredService<RoleManager<Role>>();


        if (await roleManager.FindByNameAsync(TestData.Admin.Rolename) is null)
        {
            await roleManager.CreateAsync(new Role { Name = TestData.Admin.Rolename, Description = "Администраторы" });
        }
        if (await roleManager.FindByNameAsync(TestData.User.Rolename) is null)
        {
            await roleManager.CreateAsync(new Role { Name = TestData.User.Rolename, Description = "Пользователи" });
        }
        if (await userManager.FindByNameAsync(TestData.Admin.Username) is null)
        {
            var adminUser = new User
            {
                SurName = "Админов",
                FirstName = "Админ",
                Patronymic = "Админович",
                Birthday = DateTime.Today.AddYears(-22),
                UserName = TestData.Admin.Username,
                Email = TestData.Admin.Email,
            };
            var result = await userManager.CreateAsync(adminUser, TestData.Admin.Password);
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(adminUser, TestData.Admin.Rolename);
                await userManager.AddToRoleAsync(adminUser, TestData.User.Rolename);
            }
            else
            {
                var errors = result.Errors.Select(e => e.Description).ToArray();
                logger.LogError("Учётная запись пользователя {0} не создана по причине: {1}", adminUser.UserName, string.Join(",", errors));
                throw new InvalidOperationException($"Ошибка при создании пользователя {adminUser.UserName}, список ошибок: {string.Join(",", errors)}");
            }
        }
        if (await userManager.FindByNameAsync(TestData.User.Username) is null)
        {
            var adminUser = new User
            {
                SurName = "Пользователев",
                FirstName = "Пользователь",
                Patronymic = "Пользователевич",
                Birthday = DateTime.Today.AddYears(-18),
                UserName = TestData.User.Username,
                Email = TestData.User.Email,
            };
            var result = await userManager.CreateAsync(adminUser, TestData.User.Password);
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(adminUser, TestData.User.Rolename);
            }
        }

        #endregion

        logger.LogInformation("Complete writing test data to database CorporationContext ...");
    }
}

