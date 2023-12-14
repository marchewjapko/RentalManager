using RentalManager.Core.Domain;
using RentalManager.Infrastructure.Requests;

namespace RentalManager.Core.Repositories;

public interface IUserRepository
{
    Task<User> AddAsync(User user);
    Task<User> GetAsync(int id);
    Task DeleteAsync(int id);
    Task<User> UpdateAsync(User user, int id);

    Task<IEnumerable<User>> BrowseAllAsync(string? name = null,
        DateTime? from = null,
        DateTime? to = null);

    Task ResetPassword(ResetPasswordRequest resetPasswordRequest);
}