using AutoMapper;
using BookStoreManagement.Domain.Models;
using BookStoreManagement.Domain.DTOs;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        ///Hun fi eena mapping directly cuz 3na sharing lal samme names of attributes,which is a good apprach
        //example usage:var authorDto = mapper.Map<AuthorDto>(author); yaane lmfroud hk
       

        // Mapping for Book
        CreateMap<Book, BookDto>()
            .ReverseMap();

        // Mapping for Author
        CreateMap<Author, AuthorDto>()
            .ReverseMap();

        // Mapping for BookPublisher
        CreateMap<BookPublisher, BookPublisherDto>()
            .ReverseMap();

        // Mapping for Publisher
        CreateMap<Publisher, PublisherDto>()
            .ReverseMap();
    }
}
