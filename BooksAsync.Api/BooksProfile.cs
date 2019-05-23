using AutoMapper;
using System.Collections;
using System.Collections.Generic;

namespace BooksAsync.api
{
    public class BooksProfile : Profile
    {
        public BooksProfile()
        {
            CreateMap<Api.Entities.Book, Api.Models.Book>()
                .ForMember(dest => dest.Author, opt => opt.MapFrom(src => 
                    $"{src.Author.FirstName} {src.Author.LastName}"));

            CreateMap<Api.Models.BookForCreation, Api.Entities.Book>();

            CreateMap<Api.Entities.Book, Api.Models.BookWithCovers>()
                .ForMember(dest => dest.Author, opt => opt.MapFrom(src =>
                    $"{src.Author.FirstName} {src.Author.LastName}"));
            CreateMap<IEnumerable<Api.ExternalModels.BookCover>, Api.Models.BookWithCovers>()
                .ForMember(dest => dest.bookCovers, opt => opt.MapFrom(src=> src));
        }
    }
}
