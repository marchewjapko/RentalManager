using Bogus;
using RentalManager.Core.Domain;
using RentalManager.Infrastructure.Extensions;

namespace RentalManager.Tests.ExtensionsTests;

public class ContextExtensionsTests
{
    [Test]
    public void ShouldFilterClients_FistNameEquals()
    {
        // Arrange
        var entities = new Faker<Client>().RuleFor(x => x.Id, f => f.UniqueIndex)
            .RuleFor(x => x.FirstName, f => f.Person.FirstName)
            .RuleFor(x => x.LastName, f => f.Person.LastName)
            .RuleFor(x => x.Email, f => f.Person.Email)
            .RuleFor(x => x.PhoneNumber, f => f.Phone.PhoneNumber("###-###-###"))
            .RuleFor(x => x.IdCard, f => f.Random.Word())
            .RuleFor(x => x.City, f => f.Address.City())
            .RuleFor(x => x.Street, f => f.Address.StreetAddress())
            .Generate(100)
            .AsQueryable();

        var firstName = entities.OrderBy(_ => Guid.NewGuid())
            .First()
            .FirstName;

        // Act
        var result = entities.Filter(x => x.FirstName, firstName, FilterOperand.Equals);

        // Assert
        Assert.That(result.Count(), Is.EqualTo(entities.Count(x => x.FirstName == firstName)));
    }

    [Test]
    public void ShouldFilterClients_FistNameContains()
    {
        // Arrange
        var entities = new Faker<Client>().RuleFor(x => x.Id, f => f.UniqueIndex)
            .RuleFor(x => x.FirstName, f => f.Person.FirstName)
            .RuleFor(x => x.LastName, f => f.Person.LastName)
            .RuleFor(x => x.Email, f => f.Person.Email)
            .RuleFor(x => x.PhoneNumber, f => f.Phone.PhoneNumber("###-###-###"))
            .RuleFor(x => x.IdCard, f => f.Random.Word())
            .RuleFor(x => x.City, f => f.Address.City())
            .RuleFor(x => x.Street, f => f.Address.StreetAddress())
            .Generate(100)
            .AsQueryable();

        var firstName = entities.OrderBy(_ => Guid.NewGuid())
            .First()
            .FirstName;

        // Act
        var result = entities.Filter(x => x.FirstName, firstName, FilterOperand.Contains);

        // Assert
        Assert.That(result.Count(), Is.EqualTo(entities.Count(x => x.FirstName.Contains(firstName))));
    }

    [Test]
    public void ShouldFilterPayments_AgreementIdEquals()
    {
        // Arrange
        var entities = new Faker<Payment>().RuleFor(x => x.Id, f => f.UniqueIndex)
            .RuleFor(x => x.AgreementId, f => f.Random.Int(0, 5))
            .RuleFor(x => x.Method, f => f.Random.Word())
            .RuleFor(x => x.Amount, f => f.Random.Int())
            .RuleFor(x => x.DateFrom, f => f.Date.Past())
            .RuleFor(x => x.DateTo, f => f.Date.Future())
            .Generate(100)
            .AsQueryable();

        var agreementId = entities.OrderBy(_ => Guid.NewGuid())
            .First()
            .AgreementId;

        // Act
        var result = entities.Filter(x => x.AgreementId, agreementId, FilterOperand.Equals);

        // Assert
        Assert.That(result.Count(), Is.EqualTo(entities.Count(x => x.AgreementId == agreementId)));
    }

    [Test]
    public void ShouldFilterEquipments_DateToGreaterThan()
    {
        // Arrange
        var entities = new Faker<Payment>().RuleFor(x => x.Id, f => f.UniqueIndex)
            .RuleFor(x => x.AgreementId, f => f.Random.Int(0, 5))
            .RuleFor(x => x.Method, f => f.Random.Word())
            .RuleFor(x => x.Amount, f => f.Random.Int())
            .RuleFor(x => x.DateFrom, f => f.Date.Past())
            .RuleFor(x => x.DateTo, f => f.Date.Future())
            .Generate(100)
            .AsQueryable();

        var randomDate = new Faker().Date.Past();

        // Act
        var result = entities.Filter(x => x.DateTo, randomDate, FilterOperand.GreaterThanOrEqualTo);

        // Assert
        Assert.That(result.Count(), Is.EqualTo(entities.Count(x => x.DateTo >= randomDate)));
    }

    [Test]
    public void ShouldFilterEquipments_DateFromLessThan()
    {
        // Arrange
        var entities = new Faker<Payment>().RuleFor(x => x.Id, f => f.UniqueIndex)
            .RuleFor(x => x.AgreementId, f => f.Random.Int(0, 5))
            .RuleFor(x => x.Method, f => f.Random.Word())
            .RuleFor(x => x.Amount, f => f.Random.Int())
            .RuleFor(x => x.DateFrom, f => f.Date.Past())
            .RuleFor(x => x.DateTo, f => f.Date.Future())
            .Generate(100)
            .AsQueryable();

        var randomDate = new Faker().Date.Future();

        // Act
        var result = entities.Filter(x => x.DateFrom, randomDate, FilterOperand.LessThanOrEqualTo);

        // Assert
        Assert.That(result.Count(), Is.EqualTo(entities.Count(x => x.DateFrom <= randomDate)));
    }

    [Test]
    public void ShouldNotFilter_NotAProperty()
    {
        // Arrange
        var entities = new Faker<TestEntity>().Generate(100)
            .AsQueryable();

        // Assert
        var exception = Assert.Throws<InvalidOperationException>(() =>
            entities.Filter(x => x.InvalidField, "", FilterOperand.LessThanOrEqualTo));
        Assert.That(exception.Message, Is.EqualTo("The specified member is not a writable property."));
    }
    
    [Test]
    public void ShouldNotSort_NotAProperty()
    {
        // Arrange
        var entities = new Faker<TestEntity>().Generate(100)
            .AsQueryable();

        // Assert
        var exception = Assert.Throws<InvalidOperationException>(() =>
            entities.Sort(x => x.InvalidField, SortOrder.Asc));
        Assert.That(exception.Message, Is.EqualTo("The specified member is not a writable property."));
    }

    private class TestEntity
    {
        public readonly string InvalidField = null!; // This is a field, not a property
    }
}