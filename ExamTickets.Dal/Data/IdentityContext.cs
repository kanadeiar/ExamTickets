namespace ExamTickets.Dal.Data;

public class IdentityContext : IdentityDbContext<User, Role, string>
{
    public IdentityContext(DbContextOptions<IdentityContext> options) : base(options)
    { }
}

