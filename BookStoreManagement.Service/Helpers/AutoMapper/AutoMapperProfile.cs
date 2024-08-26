using AutoMapper;
using BookStoreManagement.Domain.DTOs;
using BookStoreManagement.Domain.Models;

namespace BookStoreManagement.Service.Helpers.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // Mapping for Book
            CreateMap<Book, AddBookDTO>()
                .ReverseMap();

            CreateMap<Book, GetPublisherBookDTO>()
                .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => src.Author.Name)); // Map AuthorName
                                                                                                 //.ForMember(dest => dest.PublisherNames, opt => opt.MapFrom(src => src.Publishers.Select(bp => bp.Publisher.Name).ToList())); // Map PublisherNames

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
                .ForMember(dto => dto.Books, opt => opt.MapFrom(publisher => publisher.PublishedBooks)).ReverseMap(); // Map books or related entities

            CreateMap<GetPublisherBookDTO, BookPublisher>()
                .ReverseMap()
                .ForMember(dto => dto.Title, opt => opt.MapFrom(bookPublisher => bookPublisher.Book.Title))
                .ForMember(dto => dto.Genre, opt => opt.MapFrom(bookPublisher => bookPublisher.Book.Genre.ToString()))
                .ForMember(dto => dto.AuthorId, opt => opt.MapFrom(bookPublisher => bookPublisher.Book.AuthorId))
                .ForMember(dto => dto.AuthorName, opt => opt.MapFrom(bookPublisher => bookPublisher.Book.Author.Name));

            #endregion Mapping for Publisher

            // Mapping for BookPublisher
            CreateMap<BookPublisher, AddBookPublisherDTO>()
                .ReverseMap();

            CreateMap<Publisher, AddBookPublisherDTO>()
                .ReverseMap();
        }
    }
}