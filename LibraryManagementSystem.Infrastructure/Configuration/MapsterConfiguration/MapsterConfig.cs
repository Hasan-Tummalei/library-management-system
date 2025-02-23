using LibraryManagementSystem.Domain.DTOs.Author;
using LibraryManagementSystem.Domain.DTOs.Book;
using LibraryManagementSystem.Domain.DTOs.Borrower;
using LibraryManagementSystem.Domain.DTOs.Loan;
using LibraryManagementSystem.Domain.DTOs.User;
using LibraryManagementSystem.Domain.Entities;
using Mapster;

namespace LibraryManagementSystem.Infrastructure.Configuration.MapsterConfiguration
{
    /// <summary>
    /// Configures the mappings for DTOs to Entities and vice versa using Mapster.
    /// </summary>
    public static class MapsterConfig
    {
        /// <summary>
        /// Configures the mapping rules between DTOs and Entities.
        /// </summary>
        public static void ConfigureMappings()
        {
            // Mapping configuration for User to UserGetResDto
            TypeAdapterConfig<User, UserGetResDto>.NewConfig()
                .Map(dest => dest.UserRole, src => src.UserRole)
                .Map(dest => dest.Username, src => src.Username)
                .Map(dest => dest.Id, src => src.Id);

            // Mapping configuration for UserRegisterReqDto to User
            TypeAdapterConfig<UserRegisterReqDto, User>.NewConfig()
                .Map(dest => dest.UserRole, src => src.UserRole)
                .Map(dest => dest.Username, src => src.Username)
                .Map(dest => dest.Password, src => src.Password);

            // Mapping configuration for User to UserRegisterResDto
            TypeAdapterConfig<User, UserRegisterResDto>.NewConfig()
                .Map(dest => dest.UserRole, src => src.UserRole)
                .Map(dest => dest.Username, src => src.Username);

            // Mapping configuration for User to UserUpdateResDto
            TypeAdapterConfig<User, UserUpdateResDto>.NewConfig()
                .Map(dest => dest.Role, src => src.UserRole)
                .Map(dest => dest.UserName, src => src.Username);

            // Mapping configuration for BookCreateDto to Book
            TypeAdapterConfig<BookCreateDto, Book>.NewConfig()
                .Map(dest => dest.Title, src => src.Title)
                .Map(dest => dest.ISBN, src => src.ISBN)
                .Map(dest => dest.PublishedDate, src => src.PublishedDate)
                .Ignore(dest => dest.Authors);

            // Mapping configuration for Book to BookResponseDto
            TypeAdapterConfig<Book, BookResponseDto>.NewConfig()
                .Map(dest => dest.Title, src => src.Title)
                .Map(dest => dest.ISBN, src => src.ISBN)
                .Map(dest => dest.PublishedDate, src => src.PublishedDate);

            // Mapping configuration for AuthorCreateDto to Author
            TypeAdapterConfig<AuthorCreateDto, Author>.NewConfig()
                .Map(dest => dest.Name, src => src.Name)
                .Map(dest => dest.Bio, src => src.Bio);

            // Mapping configuration for Author to AuthorResponseDto
            TypeAdapterConfig<Author, AuthorResponseDto>.NewConfig()
                .Map(dest => dest.Name, src => src.Name)
                .Map(dest => dest.Bio, src => src.Bio)
                .Map(dest => dest.UpdatedAt, src => src.UpdatedAt)
                .Map(dest => dest.CreatedAt, src => src.CreatedAt)
                .Map(dest => dest.Id, src => src.Id);

            // Mapping configuration for Borrower to BorrowerResponseDto
            TypeAdapterConfig<Borrower, BorrowerResponseDto>.NewConfig()
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.Name, src => src.Name)
                .Map(dest => dest.Email, src => src.Email)
                .Map(dest => dest.Phone, src => src.Phone)
                .Map(dest => dest.UserId, src => src.UserId)
                .Map(dest => dest.UpdatedAt, src => src.UpdatedAt)
                .Map(dest => dest.CreatedAt, src => src.CreatedAt);

            // Mapping configuration for BorrowerCreateDto to Borrower
            TypeAdapterConfig<BorrowerCreateDto, Borrower>.NewConfig()
                .Map(dest => dest.Name, src => src.Name)
                .Map(dest => dest.Email, src => src.Email)
                .Map(dest => dest.Phone, src => src.Phone)
                .Map(dest => dest.UserId, src => src.UserId);

            // Mapping configuration for Loan to LoanResponseDto
            TypeAdapterConfig<Loan, LoanResponseDto>.NewConfig()
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.BookId, src => src.BookId)
                .Map(dest => dest.BorrowerId, src => src.BorrowerId)
                .Map(dest => dest.LoanDate, src => src.LoanDate)
                .Map(dest => dest.ReturnDate, src => src.ReturnDate)
                .Map(dest => dest.UpdatedAt, src => src.UpdatedAt)
                .Map(dest => dest.CreatedAt, src => src.CreatedAt);

            // Mapping configuration for LoanCreateDto to Loan
            TypeAdapterConfig<LoanCreateDto, Loan>.NewConfig()
                .Map(dest => dest.BookId, src => src.BookId)
                .Map(dest => dest.BorrowerId, src => src.BorrowerId)
                .Map(dest => dest.LoanDate, src => src.LoanDate)
                .Map(dest => dest.ReturnDate, src => src.ReturnDate);
        }
    }
}
