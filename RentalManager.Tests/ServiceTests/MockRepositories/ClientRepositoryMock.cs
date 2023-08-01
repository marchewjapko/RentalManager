using RentalManager.Core.Domain;
using RentalManager.Core.Repositories;

namespace RentalManager.Tests.ServiceTests.MockRepositories;

public class ClientRepositoryMock : IClientRepository
{
    public Task<Client> AddAsync(Client client)
    {
        return Task.FromResult(client);
    }

    public Task<Client> GetAsync(int id)
    {
        var newClient = new Client
        {
            Id = 1,
            Name = "Test Name",
            Surname = "Test Surname",
            City = "Test City",
            Street = "Test street",
            IdCard = "ABC 123456",
            PhoneNumber = "123 456 789"
        };
        return Task.FromResult(newClient);
    }

    public Task DeleteAsync(int id)
    {
        return Task.CompletedTask;
    }

    public Task<Client> UpdateAsync(Client client, int id)
    {
        var newClient = new Client
        {
            Id = 1,
            Name = "Update Test Name",
            Surname = "Test Surname",
            City = "Test City",
            Street = "Test street",
            IdCard = "ABC 123456",
            PhoneNumber = "123 456 789"
        };
        return Task.FromResult(newClient);
    }

    public Task<IEnumerable<Client>> BrowseAllAsync(string? name = null, string? surname = null,
        string? phoneNumber = null, string? email = null,
        string? idCard = null, string? city = null, string? street = null, DateTime? from = null, DateTime? to = null)
    {
        var newClient1 = new Client
        {
            Id = 1,
            Name = "Test Name",
            Surname = "Test Surname",
            City = "Test City",
            Street = "Test street",
            IdCard = "ABC 123456",
            PhoneNumber = "123 456 789"
        };
        var newClient2 = new Client
        {
            Id = 2,
            Name = "Test Name 2",
            Surname = "Test Surname 2",
            City = "Test City 2",
            Street = "Test street 2",
            IdCard = "ABC 123456",
            PhoneNumber = "123 456 789"
        };
        IEnumerable<Client> result = new List<Client> { newClient1, newClient2 };
        return Task.FromResult(result);
    }
}