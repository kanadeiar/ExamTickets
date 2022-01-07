using Microsoft.AspNetCore.Mvc.Rendering;

namespace ExamTickets.Controllers;

[Authorize]
public class AccountController : Controller
{
    private readonly UserManager<User> _UserManager;
    private readonly RoleManager<Role> _RoleManager;
    private readonly SignInManager<User> _SignInManager;
    private readonly IdentityContext _Context;
    public AccountController(UserManager<User> userManager, RoleManager<Role> roleManager, SignInManager<User> signInManager, IdentityContext context)
    {
        _UserManager = userManager;
        _RoleManager = roleManager;
        _SignInManager = signInManager;
        _Context = context;
    }

    [AllowAnonymous]
    public async Task<IActionResult> Index()
    {
        var model = new IndexWebModel();
        model.Cookie = Request.Cookies[".AspNetCore.Identity.Application"];
        if (User.Identity.IsAuthenticated)
        {
            model.User = await _UserManager.FindByNameAsync(User.Identity.Name);
            var roles = await _UserManager.GetRolesAsync(model.User);
            model.UserRoleNames = _RoleManager.Roles.Where(r => roles.Contains(r.Name)).Select(r => r.Description);
        }
        return View(model);
    }

    [AllowAnonymous]
    public async Task<IActionResult> Register()
    {
        return View(new RegisterWebModel());
    }
    [HttpPost, ValidateAntiForgeryToken, AllowAnonymous]
    public async Task<IActionResult> Register(RegisterWebModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }
        var user = new User
        {
            SurName = model.SurName,
            FirstName = model.FirstName,
            Patronymic = model.Patronymic,
            UserName = model.UserName,
            Email = model.Email,
            Birthday = model.Birthday,
        };
        var result = await _UserManager.CreateAsync(user, model.Password);
        if (result.Succeeded)
        {
            await _UserManager.AddToRoleAsync(user, "users");
            await _SignInManager.SignInAsync(user, false);
            return RedirectToAction("Index", "Home");
        }

        var errors = result.Errors.Select(e => IdentityErrorCodes.GetDescription(e.Code)).ToArray();
        foreach (var error in errors)
        {
            ModelState.AddModelError("", error);
        }
        return View(model);
    }

    [AllowAnonymous]
    public IActionResult Login(string returnUrl) => View(new LoginWebModel { ReturnUrl = returnUrl });

    [HttpPost, ValidateAntiForgeryToken, AllowAnonymous]
    public async Task<IActionResult> Login(LoginWebModel model)
    {
        if (!ModelState.IsValid)
            return View(model);
        var result = await _SignInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, false);
        if (result.Succeeded)
        {
            return LocalRedirect(model.ReturnUrl ?? "/");
        }
        ModelState.AddModelError("", "Ошибка в имени пользователя, либо в пароле при входе в систему");
        return View();
    }

    public async Task<IActionResult> Logout(string returnUrl)
    {
        var username = User.Identity!.Name;
        await _SignInManager.SignOutAsync();
        return LocalRedirect(returnUrl ?? "/");
    }
  
    [AllowAnonymous]
    public IActionResult AccessDenied()
    {
        return View();
    }

    /// <summary> Вебмодель сведения о пользователе </summary>
    public class IndexWebModel
    {
        /// <summary> Куки пользователя </summary>
        public string Cookie { get; set; }
        /// <summary> Сведения о пользовате </summary>
        public User User { get; set; }
        /// <summary> Роли пользователя </summary>
        public IEnumerable<string> UserRoleNames { get; set; } = Enumerable.Empty<string>();
    }

    /// <summary> Веб модель регистрации </summary>
    public class RegisterWebModel
    {
        [Required(ErrorMessage = "Фамилия обязательна для пользователя")]
        [StringLength(200, MinimumLength = 3, ErrorMessage = "Фамилия должна быть длинной от 3 до 200 символов")]
        [Display(Name = "Фамилия пользователя")]
        public string SurName { get; set; }

        [Required(ErrorMessage = "Имя обязательно для пользователя")]
        [StringLength(200, MinimumLength = 3, ErrorMessage = "Имя должно быть длинной от 3 до 200 символов")]
        [Display(Name = "Имя пользователя")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Отчество обязательно для пользователя")]
        [StringLength(200, MinimumLength = 3, ErrorMessage = "Отчество должно быть длинной от 3 до 200 символов")]
        [Display(Name = "Отчество пользователя")]
        public string Patronymic { get; set; }

        [Required(ErrorMessage = "Нужно обязательно ввести свой адрес электронной почты")]
        [EmailAddress(ErrorMessage = "Нужно ввести корректный адрес своей электронной почты")]
        [Display(Name = "Адрес электронной почты E-mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Дата рождения обязательна для ввода")]
        [Display(Name = "День рождения пользователя")]
        public DateTime Birthday { get; set; } = DateTime.Today.AddYears(-18);

        [Required(ErrorMessage = "Нужно обязательно ввести логин пользователя")]
        [Display(Name = "Логин пользователя")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Нужно обязательно придумать и ввести какой-либо пароль")]
        [Display(Name = "Пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Нужно обязательно ввести подтверждение пароля")]
        [Display(Name = "Подтверждение пароля")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Пароли не совпадают")]
        public string PasswordConfirm { get; set; }
    }

    /// <summary> Веб модель входа в систему </summary>
    public class LoginWebModel
    {
        [Required(ErrorMessage = "Нужно обязательно ввести логин пользователя")]
        [Display(Name = "Логин пользователя")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Нужно обязательно ввести свой пароль")]
        [Display(Name = "Пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Запомнить меня")]
        public bool RememberMe { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string ReturnUrl { get; set; }
    }
}

