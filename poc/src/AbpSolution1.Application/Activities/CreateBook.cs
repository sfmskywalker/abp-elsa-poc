using System;
using System.Threading.Tasks;
using AbpSolution1.Books;
using Elsa.Extensions;
using Elsa.Workflows;
using Elsa.Workflows.Attributes;
using Elsa.Workflows.Models;
using JetBrains.Annotations;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.EventBus.Local;
using Volo.Abp.ObjectMapping;

namespace AbpSolution1.Activities;

[Activity(
    Namespace = "ABP",
    Category = "Books",
    DisplayName = "Create Book",
    Description = "Creates a new book in the system."
)]
[UsedImplicitly]
public class CreateBook : CodeActivity<BookDto>
{
    [Input(Description = "The name of the book to create.")] public Input<string> BookName { get; set; } = null!;
    [Input(Description = "The type of the book to create.")] public Input<BookType> BookType { get; set; } = null!;
    [Input(Description = "The publish date of the book to create.")] public Input<DateTime> PublishDate { get; set; } = null!;
    [Input(Description = "The price of the book to create.")] public Input<float> Price { get; set; } = null!;

    protected override async ValueTask ExecuteAsync(ActivityExecutionContext context)
    {
        var bookService = context.GetRequiredService<IRepository<Book, Guid>>();
        var book = new Book
        {
            Name = BookName.Get(context),
            Type = BookType.Get(context),
            PublishDate = PublishDate.Get(context),
            Price = Price.Get(context)
        };
        var localEventBus = context.GetRequiredService<ILocalEventBus>();
        var createdBook = await bookService.InsertAsync(book);
        var mapper = context.GetRequiredService<IObjectMapper>();
        var bookDto = mapper.Map<Book, BookDto>(createdBook);
        await localEventBus.PublishAsync(new BookCreatedEvent(bookDto));
        context.SetResult(bookDto);
    }
}