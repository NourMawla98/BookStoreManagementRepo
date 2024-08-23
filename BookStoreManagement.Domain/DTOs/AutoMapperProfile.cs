using AutoMapper;
using BookStoreManagement.Domain.DTOs;
using BookStoreManagement.Domain.Models;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        // Mapping for Book
        CreateMap<Book, AddBookDTO>()
            .ReverseMap();

        CreateMap<Book, GetBookDTO>()
            .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => src.Author.Name)) // Map AuthorName
            .ForMember(dest => dest.PublisherNames, opt => opt.MapFrom(src => src.Publishers.Select(bp => bp.Publisher.Name).ToList())); // Map PublisherNames

        // Add other mappings as needed

        #region Mapping for Author

        CreateMap<Author, AddAuthorDTO>()
            .ReverseMap();

        CreateMap<Author, GetAuthorListDTO>()
            .ReverseMap();

        CreateMap<Author, GetAuthorDTO>()
            .ForMember(dest => dest.Books, opt => opt.MapFrom(src => src.Books)); // Map books

        #endregion Mapping for Author

        #region Mapping for Publisher

        CreateMap<Publisher, AddPublisherDTO>()
            .ReverseMap();

        CreateMap<Publisher, GetPublisherListDTO>()
            .ReverseMap();

        CreateMap<Publisher, GetPublisherDTO>()
            .ForMember(dest => dest.Books, opt => opt.MapFrom(src => src.PublishedBooks)); // Map books or related entities

        #endregion Mapping for Publisher

        // Mapping for BookPublisher
        CreateMap<BookPublisher, AddBookPublisherDTO>()
            .ReverseMap();

        CreateMap<Publisher, AddBookPublisherDTO>()
            .ReverseMap();
    }
}
