using RentalManager.Core.Domain;
using RentalManager.Global.Queries;
using RentalManager.Global.Requests;

namespace RentalManager.Core.Repositories;

public interface IUserRepository
{
    Task<User> GetAsync(int id);
    Task DeleteAsync(int id);
    Task UpdateAsync(User user, int id);

    Task<IEnumerable<User>> BrowseAllAsync(QueryUser queryUser);

    Task ResetPassword(ResetPasswordRequest resetPasswordRequest);
}