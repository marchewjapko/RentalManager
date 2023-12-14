using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RentalManager.Core.Domain;
using RentalManager.Core.Repositories;
using RentalManager.Infrastructure.Exceptions;
using RentalManager.Infrastructure.Repositories.DbContext;
using RentalManager.Infrastructure.Requests;

namespace RentalManager.Infrastructure.Repositories;

public class UserRepository(AppDbContext appDbContext) : IUserRepository
{
    public async Task<User> AddAsync(User user)
    {
        try
        {
            appDbContext.Users.Add(user);
            await appDbContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new Exception("Unable to add user\n" + ex.Message);
        }

        return await Task.FromResult(user);
    }

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

    public async Task<IEnumerable<User>> BrowseAllAsync(string? name = null,
        DateTime? from = null,
        DateTime? to = null)
    {
        var result = appDbContext.Users.AsQueryable();
        if (name != null)
        {
            result = result.Where(x => x.Name.Contains(name));
        }

        if (from != null)
        {
            result = result.Where(x => x.DateAdded.Date > from.Value.Date);
        }

        if (to != null)
        {
            result = result.Where(x => x.DateAdded.Date < to.Value.Date);
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
        userToUpdate.Gender = user.Gender;
        await appDbContext.SaveChangesAsync();

        return await Task.FromResult(userToUpdate);
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

        user.PasswordHash = hasher.HashPassword(null, resetPasswordRequest.NewPassword);
        await appDbContext.SaveChangesAsync();
    }
}