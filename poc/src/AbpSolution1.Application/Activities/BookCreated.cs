using System.Threading.Tasks;
using AbpSolution1.Books;
using AbpSolution1.Stimuli;
using Elsa.Expressions.Models;
using Elsa.Extensions;
using Elsa.Workflows;
using Elsa.Workflows.Attributes;
using Elsa.Workflows.Models;
using JetBrains.Annotations;

namespace AbpSolution1.Activities;

[Activity(
    Namespace = "ABP",
    Category = "Books",
    DisplayName = "Book Created",
    Description = "Triggered when a new book is created in the system."
)]
[UsedImplicitly]
public class BookCreated : Trigger<BookDto>
{
    [Input(Description = "The type of book to listen for.")] public Input<BookType?> BookType { get; set; } = null!;

    protected override async ValueTask ExecuteAsync(ActivityExecutionContext context)
    {
        if (context.IsTriggerOfWorkflow())
        {
            await ExecuteInternalAsync(context);
            return;
        }
        
        var stimulus = CreateStimulus(context.ExpressionExecutionContext);
        context.CreateBookmark(stimulus, ExecuteInternalAsync, includeActivityInstanceId: false);
    }

    protected override object GetTriggerPayload(TriggerIndexingContext context)
    {
        return CreateStimulus(context.ExpressionExecutionContext);
    }

    private async ValueTask ExecuteInternalAsync(ActivityExecutionContext context)
    {
        var book = context.GetWorkflowInput<BookDto>();
        context.SetResult(book);
        await context.CompleteActivityAsync();
    }
    
    private BookCreatedStimulus CreateStimulus(ExpressionExecutionContext context)
    {
        var bookType = BookType.GetOrDefault(context);
        return new(bookType);
    }
}