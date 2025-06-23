using AutoMapper;
using AbpSolution1.Books;

namespace AbpSolution1;

public class AbpSolution1ApplicationAutoMapperProfile : Profile
{
    public AbpSolution1ApplicationAutoMapperProfile()
    {
        CreateMap<Book, BookDto>();
        CreateMap<CreateUpdateBookDto, Book>();
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */
    }
}
