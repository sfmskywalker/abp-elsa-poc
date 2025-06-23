using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using AbpSolution1.Permissions;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using System.Linq.Dynamic.Core;
using Volo.Abp.EventBus.Local;

namespace AbpSolution1.Books;

[Authorize(AbpSolution1Permissions.Books.Default)]
public class BookAppService(IRepository<Book, Guid> repository, ILocalEventBus localEventBus) : ApplicationService, IBookAppService
{
    public async Task<BookDto> GetAsync(Guid id)
    {
        var book = await repository.GetAsync(id);
        return ObjectMapper.Map<Book, BookDto>(book);
    }

    public async Task<PagedResultDto<BookDto>> GetListAsync(PagedAndSortedResultRequestDto input)
    {
        var queryable = await repository.GetQueryableAsync();
        var query = queryable
            .OrderBy(input.Sorting.IsNullOrWhiteSpace() ? "Name" : input.Sorting)
            .Skip(input.SkipCount)
            .Take(input.MaxResultCount);

        var books = await AsyncExecuter.ToListAsync(query);
        var totalCount = await AsyncExecuter.CountAsync(queryable);

        return new PagedResultDto<BookDto>(
            totalCount,
            ObjectMapper.Map<List<Book>, List<BookDto>>(books)
        );
    }

    [Authorize(AbpSolution1Permissions.Books.Create)]
    public async Task<BookDto> CreateAsync(CreateUpdateBookDto input)
    {
        var book = ObjectMapper.Map<CreateUpdateBookDto, Book>(input);
        await repository.InsertAsync(book);
        var bookDto = ObjectMapper.Map<Book, BookDto>(book);
        await localEventBus.PublishAsync(new BookCreatedEvent(bookDto));
        return ObjectMapper.Map<Book, BookDto>(book);
    }

    [Authorize(AbpSolution1Permissions.Books.Edit)]
    public async Task<BookDto> UpdateAsync(Guid id, CreateUpdateBookDto input)
    {
        var book = await repository.GetAsync(id);
        ObjectMapper.Map(input, book);
        await repository.UpdateAsync(book);
        return ObjectMapper.Map<Book, BookDto>(book);
    }

    [Authorize(AbpSolution1Permissions.Books.Delete)]
    public async Task DeleteAsync(Guid id)
    {
        await repository.DeleteAsync(id);
    }
}
