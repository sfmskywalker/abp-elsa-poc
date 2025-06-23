using AutoMapper;
using AbpSolution1.Books;

namespace AbpSolution1.Web;

public class AbpSolution1WebAutoMapperProfile : Profile
{
    public AbpSolution1WebAutoMapperProfile()
    {
        CreateMap<BookDto, CreateUpdateBookDto>();
        
        //Define your object mappings here, for the Web project
    }
}
