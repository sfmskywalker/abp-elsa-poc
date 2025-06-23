using System;
using System.Threading.Tasks;
using AbpSolution1.Books;
using Elsa.Extensions;
using Elsa.Workflows;
using Elsa.Workflows.Activities.Flowchart.Attributes;
using Elsa.Workflows.Attributes;
using Elsa.Workflows.Models;
using JetBrains.Annotations;
using Volo.Abp.Domain.Repositories;

namespace AbpSolution1.Activities;

[Activity(
    Namespace = "ABP",
    Category = "Books",
    DisplayName = "Get Book",
    Description = "Gets an existing book from the system."
)]
[FlowNode("Found", "Not Found")]
[UsedImplicitly]
public class GetBook : CodeActivity<Book>
{
    [Input(Description = "The ID of the book to get.")] public Input<Guid> BookId { get; set; } = null!;

    protected override async ValueTask ExecuteAsync(ActivityExecutionContext context)
    {
        var repository = context.GetRequiredService<IRepository<Book, Guid>>();
        var id = BookId.GetOrDefault(context);

        if (id == Guid.Empty)
        {
            await context.CompleteActivityWithOutcomesAsync("Not Found");
            return;
        }

        var book = await repository.FindAsync(id);

        if (book == null)
        {
            await context.CompleteActivityWithOutcomesAsync("Not Found");
            return;
        }

        context.SetResult(book);
        await context.CompleteActivityWithOutcomesAsync("Found");
    }
}