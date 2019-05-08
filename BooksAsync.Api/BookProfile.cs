using AutoMapper;

namespace BooksAsync.api
{
    public class BooksProfile : Profile
    {
        public BooksProfile()
        {
            CreateMap<Api.Entities.Book, Api.Models.Book>()
                .ForMember(dest => dest.Author, opt => opt.MapFrom(src => $"{src.Author.FirstName} {src.Author.LastName}"));
        }
    }
}