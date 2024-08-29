using AutoMapper;
using BookStoreManagement.Domain.DTOs;
using BookStoreManagement.Domain.Models;

namespace BookStoreManagement.Service.Helpers.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Book, AddBookDTO>()
           .ForMember(dest => dest.Publishers, opt => opt.MapFrom(src => src.Publishers)).ReverseMap();

            CreateMap<Book, EditBookDTO>()
           .ForMember(dest => dest.Publishers, opt => opt.Ignore()).ReverseMap();

            // Mapping for BookPublisher to AddPublisherDTO
            CreateMap<BookPublisher, AddPublisherDTO>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Publisher.Name))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Publisher.Location)).ReverseMap();
            // Mapping for Book

            CreateMap<Book, GetBookDTO>()
                .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.ToString())) // Assuming genre is an enum
                .ForMember(dest => dest.AuthorId, opt => opt.MapFrom(src => src.AuthorId))
                .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => src.Author.Name))
                .ForMember(dest => dest.Publishers, opt => opt.MapFrom(src => src.Publishers.Select(bp => bp.Publisher)))
                .ReverseMap();

            CreateMap<Book, GetPublisherBookDTO>()
               .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.ToString())) // Assuming BookGenreEnum needs to be mapped to string
               .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => src.Author.Name)).ReverseMap(); // Maps Author's Name to GetPublisherBookDTO

            // Mapping for AddPublisherDTO to Publisher, no need to map to BookPublisher directly
            CreateMap<Publisher, AddPublisherDTO>().ReverseMap();

            #region Mapping for Author

            CreateMap<Author, AddAuthorDTO>()
                .ReverseMap();

            CreateMap<Author, GetAuthorListDTO>()
                .ReverseMap();

            CreateMap<Author, GetAuthorDTO>()
                .ForMember(dest => dest.Books, opt => opt.MapFrom(src => src.Books)).ReverseMap(); // Map books

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

            #region Mapping for bookpublisher

            // Mapping for BookPublisher
            CreateMap<BookPublisher, BookPuplisherDTO>().ReverseMap();

            #endregion Mapping for bookpublisher

            #region mapping for purchase

            CreateMap<Purchase, GetPurchaseDTO>()
                .ReverseMap();

            #endregion mapping for purchase
        }
    }
}