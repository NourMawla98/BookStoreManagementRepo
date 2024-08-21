using AutoMapper;
using BookStoreManagement.Domain.DTOs;
using BookStoreManagement.Domain.Models;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        ///Hun fi eena mapping directly cuz 3na sharing lal samme names of attributes,which is a good apprach
        //example usage:var authorDto = mapper.Map<AuthorDto>(author); yaane lmfroud hk

        // Mapping for Book
        CreateMap<Book, BookDto>()
            .ReverseMap();

        #region Mapping for Author

        CreateMap<Author, AddAuthorDTO>()
            .ReverseMap();

        CreateMap<Author, GetAuthorDTO>()
            .ReverseMap();

        #endregion Mapping for Author

        // Mapping for BookPublisher
        CreateMap<BookPublisher, AddBookPublisherDTO>()
            .ReverseMap();

        // Mapping for Publisher
        CreateMap<Publisher, PublisherDto>()
            .ReverseMap();
    }
}