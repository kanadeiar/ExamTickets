namespace ExamTickets.Controllers;

public class PreparationController : Controller
{
    private readonly ExamTicketsContext _Context;
    public PreparationController(ExamTicketsContext context)
    {
        _Context = context;
    }
    public IActionResult Index()
    {
        var models = _Context.Groups.Select(i => new GroupWebModel
        {
            Id = i.Id, 
            Order = i.Order, 
            Name = i.Name
        });
        return View(models);
    }
    public IActionResult Group(int id)
    {
        var models = _Context.Examens.Where(e => e.GroupId == id).Select(i => new ExamenWebModel
        {
            Id = i.Id,
            Order = i.Order,
            GroupId = i.GroupId,
            Name = i.Name,
        });
        ViewBag.GroupName = _Context.Groups.First(e => e.Id == id).Name;
        return View(models);
    }
    public IActionResult Examen(int id)
    {
        var models = _Context.Tickets.Where(e => e.ExamenId == id).Select(i => new TicketWebModel
        {
            Id = i.Id,
            ExamenId = i.ExamenId,
            TicketText = i.TicketText,
        });
        ViewBag.GroupId = _Context.Examens.First(e => e.Id == id).GroupId;
        ViewBag.GroupName = _Context.Examens.Include(i => i.Group).First(e => e.Id == id).Group.Name;
        ViewBag.ExamenName = _Context.Examens.First(e => e.Id == id).Name;
        return View(models);
    }
    public IActionResult Ticket(int id)
    {
        var models = _Context.Questions.Include(q => q.QuestionOptions).Where(e => e.TicketId == id).Select(i => new QuestionWebModel
        {
            TicketId = id,
            QuestionText = i.QuestionText,
            QuestionOptions = i.QuestionOptions.Select(o => new QuestionOptionWebModel
            {
                OptionText = o.OptionText,
                IsCorrect = o.IsCorrect,
            }),
        });
        ViewBag.ExamenId = _Context.Tickets.First(e => e.Id == id).ExamenId;
        ViewBag.ExamenName = _Context.Tickets.Include(i => i.Examen).First(e => e.Id == id).Examen.Name;
        ViewBag.TicketText = _Context.Tickets.First(e => e.Id == id).TicketText;
        return View(models);
    }


    /// <summary> Вебмодель группа </summary>
    public class GroupWebModel
    {
        public int Id { get; set; }
        public int Order { get; set; }
        /// <summary> Название группы экзаменов </summary>
        public string Name { get; set; }
    }
    /// <summary> Вебмодель экзамен </summary>
    public class ExamenWebModel
    {
        public int Id { get; set; }
        public int Order { get; set; }
        public int GroupId { get; set; }
        /// <summary> Название экзамена </summary>
        public string Name { get; set; }
    }
    /// <summary> Вебмодель билет </summary>
    public class TicketWebModel
    {
        public int Id { get; set; }
        public int ExamenId { get; set; }
        /// <summary> Билет </summary>
        public string TicketText { get; set; }
    }
    /// <summary> Вебмодель вопроса </summary>
    public class QuestionWebModel
    {
        public int TicketId { get; set; }
        public string QuestionText { get; set; }
        public IEnumerable<QuestionOptionWebModel> QuestionOptions { get; set; } =
            new List<QuestionOptionWebModel>();
    }
    /// <summary> Вебмодель варианта ответа на вопрос </summary>
    public class QuestionOptionWebModel
    {
        public string OptionText { get; set; }
        public bool IsCorrect { get; set; }
    }
}

