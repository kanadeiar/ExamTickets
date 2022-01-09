namespace ExamTickets.Dal.Data;

public class ExamTicketsContext : DbContext
{
    public DbSet<Group> Groups { get; set; }
    public DbSet<Examen> Examens { get; set; }
    public DbSet<Ticket> Tickets { get; set; }
    public DbSet<Question> Questions { get; set; }
    public DbSet<QuestionOption> QuestionOptions { get; set; }
    public DbSet<ExamenResult> ExamenResults { get; set; }
    public DbSet<ResultQuestion> ResultQuestions { get; set; }
    public DbSet<ResultQuestionOption> ResultQuestionOptions { get; set; }

    public ExamTicketsContext(DbContextOptions<ExamTicketsContext> options) : base(options)
    { }
}

