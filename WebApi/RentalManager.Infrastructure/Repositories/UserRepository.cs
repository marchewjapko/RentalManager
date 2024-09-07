using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RentalManager.Core.Domain;
using RentalManager.Core.Repositories;
using RentalManager.Global.Queries;
using RentalManager.Global.Requests;
using RentalManager.Infrastructure.Exceptions;
using RentalManager.Infrastructure.Repositories.DbContext;

namespace RentalManager.Infrastructure.Repositories;

public class UserRepository(AppDbContext appDbContext) : IUserRepository
{
    public async Task DeleteAsync(int id)
    {
        var result = await appDbContext.Users.FirstOrDefaultAsync(x => x.Id == id);

        if (result == null)
        {
            throw new UserNotFoundException(id);
        }

        appDbContext.Users.Remove(result);
        await appDbContext.SaveChangesAsync();
    }

    public async Task<User> GetAsync(int id)
    {
        var result = await Task.FromResult(appDbContext.Users.FirstOrDefault(x => x.Id == id));

        if (result == null)
        {
            throw new UserNotFoundException(id);
        }

        return result;
    }

    public async Task<IEnumerable<User>> BrowseAllAsync(QueryUser queryUser)
    {
        var result = appDbContext.Users.AsQueryable();

        if (queryUser.Name != null)
        {
            result = result.Where(x => x.Name.Contains(queryUser.Name));
        }

        if (queryUser.From != null)
        {
            result = result.Where(x => x.CreatedTs.Date > queryUser.From.Value.Date);
        }

        if (queryUser.To != null)
        {
            result = result.Where(x => x.CreatedTs.Date < queryUser.To.Value.Date);
        }

        return await Task.FromResult(result.AsEnumerable());
    }

    public async Task<User> UpdateAsync(User user, int id)
    {
        var userToUpdate = appDbContext.Users.FirstOrDefault(x => x.Id == id);

        if (userToUpdate == null)
        {
            throw new UserNotFoundException(id);
        }

        userToUpdate.Name = user.Name;
        userToUpdate.Surname = user.Surname;
        userToUpdate.Image = user.Image;
        userToUpdate.UpdatedTs = DateTime.Now;
        await appDbContext.SaveChangesAsync();

        return userToUpdate;
    }

    public async Task ResetPassword(ResetPasswordRequest resetPasswordRequest)
    {
        var hasher = new PasswordHasher<User>();
        var user =
            await appDbContext.Users.FirstOrDefaultAsync(x =>
                x.UserName == resetPasswordRequest.UserName);
        if (user is null)
        {
            throw new PasswordChangeFailedException(resetPasswordRequest.UserName);
        }

        user.PasswordHash = hasher.HashPassword(null!, resetPasswordRequest.NewPassword);
        user.PasswordValidTo = DateTime.Now.AddMinutes(5);
        await appDbContext.SaveChangesAsync();
    }

    public async Task ClearPasswordExpiration(User user)
    {
        user.PasswordValidTo = null;
        await appDbContext.SaveChangesAsync();
    }
}