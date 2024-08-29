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

            CreateMap<Book, GetBookDTO>()

                .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.ToString())) // Assuming genre is an enum
                .ForMember(dest => dest.AuthorId, opt => opt.MapFrom(src => src.AuthorId))
                .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => src.Author.Name))
                .ForMember(dest => dest.Publishers, opt => opt.MapFrom(src => src.Publishers.Select(bp => bp.Publisher)))
                .ReverseMap();

            CreateMap<Book, GetPublisherBookDTO>()
               .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.ToString())) // Assuming BookGenreEnum needs to be mapped to string
               .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => src.Author.Name)).ReverseMap(); // Maps Author's Name to GetPublisherBookDTO


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
            CreateMap<BookPublisher, AddBookPublisherDTO>()
                .ReverseMap();

            CreateMap<Publisher, AddBookPublisherDTO>()
                .ReverseMap();

            #endregion Mapping for bookpublisher

            #region mapping for purchase
            CreateMap<Purchase, AddPurchaseDTO>().ReverseMap();

            CreateMap<Purchase, GetPurchaseDTO>()
                .ReverseMap();

            #endregion mapping for purchase

        }
    }
}