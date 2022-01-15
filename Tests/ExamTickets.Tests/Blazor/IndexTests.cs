namespace ExamTickets.Tests.Blazor;

[TestClass]
public class IndexTests
{
    private Random _rnd = new ();

    [TestMethod]
    public void Index_Init_ShouldCorrect()
    {
        using var context = new Bunit.TestContext();
        context.Services.AddDbContext<ExamTicketsContext>(options =>
        {
            options.UseInMemoryDatabase(nameof(ExamTicketsContext) + $"{_rnd.Next(10000)}");
        });

        var component = context.RenderComponent<ExamTickets.Blazor.Index>();
        Assert.AreEqual(1, component.RenderCount);

        component.Render();

        Assert.AreEqual(2, component.RenderCount);
        Assert
            .IsTrue(component.Markup.Contains("<p>Выберите группу экзаменов:</p>"));
    }
}

