using RentalManager.Infrastructure.Commands;
using RentalManager.Infrastructure.Services;
using RentalManager.Tests.ServiceTests.MockRepositories;

namespace RentalManager.Tests.ServiceTests;

public class ClientServiceTests
{
    private readonly ClientService _clientService;

    public ClientServiceTests()
    {
        _clientService = new ClientService(new ClientRepositoryMock());
    }

    [Test]
    public async Task ShouldAdd()
    {
        var newClient = new CreateClient
        {
            Name = "Test Name",
            Surname = "Test Surname",
            City = "Test City",
            Street = "Test street",
            IdCard = "ABC 123456",
            PhoneNumber = "123 456 789"
        };
        var result = await _clientService.AddAsync(newClient);
        Assert.That(result.Name, Is.EqualTo("Test Name"));
    }

    [Test]
    public async Task ShouldDelete()
    {
        await _clientService.DeleteAsync(0);
    }

    [Test]
    public async Task ShouldUpdate()
    {
        var newClient = new UpdateClient
        {
            Name = "Test Name",
            Surname = "Test Surname",
            City = "Test City",
            Street = "Test street",
            IdCard = "ABC 123456",
            PhoneNumber = "123 456 789"
        };
        var result = await _clientService.UpdateAsync(newClient, -1);
        Assert.That(result.Name, Is.EqualTo("Update Test Name"));
    }

    [Test]
    public async Task ShouldBrowseAllClients()
    {
        var result = await _clientService.BrowseAllAsync();
        Assert.That(result.Count(), Is.EqualTo(2));
    }
}