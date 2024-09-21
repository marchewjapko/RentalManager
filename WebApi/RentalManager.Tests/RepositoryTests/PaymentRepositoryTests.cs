// using Bogus;
// using Microsoft.EntityFrameworkCore;
// using RentalManager.Core.Domain;
// using RentalManager.Global.Queries;
// using RentalManager.Infrastructure.ExceptionHandling.Exceptions;
// using RentalManager.Infrastructure.Repositories;
// using RentalManager.Infrastructure.Repositories.DbContext;
//
// namespace RentalManager.Tests.RepositoryTests;
//
// public class PaymentRepositoryTests
// {
//     private AppDbContext _appDbContext = null!;
//     private PaymentRepository _paymentRepository = null!;
//
//     private Client MockClient { get; set; }
//
//     private Equipment MockEquipment { get; set; }
//
//     private Agreement MockAgreement { get; set; }
//
//     private Payment MockPayment { get; set; }
//
//     [SetUp]
//     public void Setup()
//     {
//         MockClient = new Faker<Client>()
//             .RuleFor(x => x.Id, () => 1)
//             .RuleFor(x => x.FirstName, f => f.Name.FirstName())
//             .RuleFor(x => x.LastName, f => f.Name.LastName())
//             .RuleFor(x => x.City, f => f.Address.City())
//             .RuleFor(x => x.Street, f => f.Address.StreetName())
//             .RuleFor(x => x.IdCard, () => "ABC 123456")
//             .RuleFor(x => x.PhoneNumber, f => f.Phone.PhoneNumber("###-###-###"))
//             .RuleFor(x => x.CreatedBy, f => f.Random.Int())
//             .Generate();
//
//         MockEquipment = new Faker<Equipment>()
//             .RuleFor(x => x.Id, () => 1)
//             .RuleFor(x => x.Name, f => f.Commerce.ProductName())
//             .RuleFor(x => x.Price, f => f.Random.Int(1, 200))
//             .RuleFor(x => x.Agreements, () => new List<Agreement>())
//             .RuleFor(x => x.CreatedBy, f => f.Random.Int())
//             .Generate();
//
//         MockAgreement = new Faker<Agreement>()
//             .RuleFor(x => x.Id, () => 1)
//             .RuleFor(x => x.UserId, f => f.Random.Int(1, 100))
//             .RuleFor(x => x.ClientId, () => MockClient.Id)
//             .RuleFor(x => x.Client, () => MockClient)
//             .RuleFor(x => x.Comment, f => f.Lorem.Sentence())
//             .RuleFor(x => x.Deposit, f => f.Random.Int(1, 100))
//             .RuleFor(x => x.TransportFromPrice, () => null)
//             .RuleFor(x => x.TransportToPrice, f => f.Random.Int(1, 100))
//             .RuleFor(x => x.DateAdded, () => DateTime.Now)
//             .RuleFor(x => x.Equipments, () => new List<Equipment> { MockEquipment })
//             .RuleFor(x => x.CreatedBy, f => f.Random.Int())
//             .Generate();
//
//         MockPayment = new Faker<Payment>()
//             .RuleFor(x => x.Id, () => 1)
//             .RuleFor(x => x.AgreementId, () => MockAgreement.Id)
//             .RuleFor(x => x.Agreement, () => MockAgreement)
//             .RuleFor(x => x.Method, f => f.Random.Word())
//             .RuleFor(x => x.Amount, f => f.Random.Int())
//             .RuleFor(x => x.DateFrom, f => f.Date.Past())
//             .RuleFor(x => x.DateTo, f => f.Date.Future())
//             .RuleFor(x => x.CreatedBy, f => f.Random.Int())
//             .Generate();
//
//         var guid = Guid.NewGuid()
//             .ToString();
//
//         _appDbContext = new AppDbContext(new DbContextOptionsBuilder<AppDbContext>()
//             .UseInMemoryDatabase(guid)
//             .Options);
//
//         _paymentRepository = new PaymentRepository(_appDbContext);
//
//         _appDbContext.Clients.Add(MockClient);
//         _appDbContext.Equipments.Add(MockEquipment);
//         _appDbContext.Agreements.Add(MockAgreement);
//         _appDbContext.SaveChanges();
//
//         Assume.That(_appDbContext.Payments.Count(), Is.EqualTo(0));
//     }
//
//     [TearDown]
//     public void TearDown()
//     {
//         _appDbContext.Database.EnsureDeleted();
//         _appDbContext.Dispose();
//     }
//
//     [Test]
//     public async Task ShouldAdd()
//     {
//         // Act
//         await _paymentRepository.AddAsync(MockPayment);
//
//         // Assert
//         Assert.That(_appDbContext.Payments.Count(), Is.EqualTo(1));
//     }
//
//     [Test]
//     public async Task ShouldGet()
//     {
//         // Arrange
//         await _paymentRepository.AddAsync(MockPayment);
//         await _appDbContext.SaveChangesAsync();
//         Assume.That(_appDbContext.Payments.Count(), Is.EqualTo(1));
//
//         // Act
//         var result = await _paymentRepository.GetAsync(1);
//
//         // Assert
//         Assert.Multiple(() => {
//             Assert.That(result.Id, Is.EqualTo(MockPayment.Id));
//             Assert.That(result.AgreementId, Is.EqualTo(MockPayment.AgreementId));
//             Assert.That(result.Agreement, Is.EqualTo(MockPayment.Agreement));
//             Assert.That(result.Method, Is.EqualTo(MockPayment.Method));
//             Assert.That(result.Amount, Is.EqualTo(MockPayment.Amount));
//             Assert.That(result.DateFrom, Is.EqualTo(MockPayment.DateFrom));
//             Assert.That(result.DateTo, Is.EqualTo(MockPayment.DateTo));
//         });
//     }
//
//     [Test]
//     public void ShouldNotGet()
//     {
//         // Assert
//         Assert.ThrowsAsync<PaymentNotFoundException>(async () =>
//             await _paymentRepository.GetAsync(1));
//     }
//
//     [Test]
//     public async Task ShouldDelete()
//     {
//         // Arrange
//         _appDbContext.Payments.Add(MockPayment);
//         await _appDbContext.SaveChangesAsync();
//
//         Assume.That(_appDbContext.Payments.Count(), Is.EqualTo(1));
//
//         // Act
//         await _paymentRepository.DeleteAsync(1);
//
//         // Assert
//         Assert.That(_appDbContext.Payments.Count(), Is.EqualTo(0));
//     }
//
//     [Test]
//     public void ShouldNotDelete()
//     {
//         // Assert
//         Assert.ThrowsAsync<PaymentNotFoundException>(async () =>
//             await _paymentRepository.DeleteAsync(1));
//     }
//
//     [Test]
//     public async Task ShouldBrowseAll()
//     {
//         // Arrange
//         var newPayment = new Faker<Payment>()
//             .RuleFor(x => x.Id, () => 2)
//             .RuleFor(x => x.AgreementId, () => MockAgreement.Id)
//             .RuleFor(x => x.Agreement, () => MockAgreement)
//             .RuleFor(x => x.Method, f => f.Random.Word())
//             .RuleFor(x => x.Amount, f => f.Random.Int())
//             .RuleFor(x => x.DateFrom, f => f.Date.Past())
//             .RuleFor(x => x.DateTo, f => f.Date.Future())
//             .Generate();
//
//         _appDbContext.Payments.Add(MockPayment);
//         _appDbContext.Payments.Add(newPayment);
//         await _appDbContext.SaveChangesAsync();
//
//         Assume.That(_appDbContext.Payments.Count(), Is.EqualTo(2));
//
//         // Act
//         var result = await _paymentRepository.BrowseAllAsync(new QueryPayment());
//
//         // Assert
//         Assert.That(result.Count(), Is.EqualTo(2));
//     }
//
//     [Test]
//     public async Task ShouldFilter()
//     {
//         // Arrange
//         var newAgreement = new Faker<Agreement>()
//             .RuleFor(x => x.Id, () => 2)
//             .RuleFor(x => x.UserId, f => f.Random.Int(1, 100))
//             .RuleFor(x => x.ClientId, () => MockClient.Id)
//             .RuleFor(x => x.Client, () => MockClient)
//             .RuleFor(x => x.Comment, f => f.Lorem.Sentence())
//             .RuleFor(x => x.Deposit, f => f.Random.Int(1, 100))
//             .RuleFor(x => x.TransportFromPrice, () => null)
//             .RuleFor(x => x.TransportToPrice, f => f.Random.Int(1, 100))
//             .RuleFor(x => x.DateAdded, () => DateTime.Now)
//             .RuleFor(x => x.Equipments, () => new List<Equipment> { MockEquipment })
//             .RuleFor(x => x.Payments, () => new List<Payment>())
//             .Generate();
//
//         var newAgreementEntity = _appDbContext.Agreements.Add(newAgreement)
//             .Entity;
//
//         var newPayment = new Faker<Payment>()
//             .RuleFor(x => x.Id, () => 2)
//             .RuleFor(x => x.AgreementId, () => newAgreementEntity.Id)
//             .RuleFor(x => x.Agreement, () => newAgreementEntity)
//             .RuleFor(x => x.Method, f => f.Random.Word())
//             .RuleFor(x => x.Amount, f => f.Random.Int())
//             .RuleFor(x => x.DateFrom, f => f.Date.Past())
//             .RuleFor(x => x.DateTo, f => f.Date.Future())
//             .RuleFor(x => x.IsActive, () => false)
//             .Generate();
//
//         _appDbContext.Payments.Add(MockPayment);
//         _appDbContext.Payments.Add(newPayment);
//         await _appDbContext.SaveChangesAsync();
//         Assume.That(_appDbContext.Payments.Count(), Is.EqualTo(2));
//
//         var query1 = new QueryPayment
//         {
//             AgreementId = newAgreementEntity.Id,
//             OnlyActive = false
//         };
//
//         var query2 = new QueryPayment
//         {
//             OnlyActive = true
//         };
//
//         // Act
//         var result1 = (await _paymentRepository.BrowseAllAsync(query1)).ToList();
//         var result2 = (await _paymentRepository.BrowseAllAsync(query2)).ToList();
//
//         // Assert
//         Assert.Multiple(() => {
//             Assert.That(result1, Has.Count.EqualTo(1));
//             Assert.That(result1.First()
//                 .Method, Is.EqualTo(newPayment.Method));
//             Assert.That(result2, Has.Count.EqualTo(1));
//             Assert.That(result2.First()
//                 .Method, Is.EqualTo(MockPayment.Method));
//         });
//     }
//
//     [Test]
//     public async Task ShouldFilter_DateAddedBetween()
//     {
//         // Arrange
//         var dateSearchFrom = new DateTime(2020, 1, 1);
//         var dateSearchTo = new DateTime(2020, 1, 7);
//
//         var dateRangeFromInRange = new DateTime(2020, 1, 2);
//         var dateRangeToInRange = new DateTime(2020, 1, 5);
//
//         var dateRangeFromOutsideRange = new DateTime(2020, 1, 8);
//         var dateRangeToOutsideRange = new DateTime(2020, 1, 17);
//
//         var newPayment = new Faker<Payment>()
//             .RuleFor(x => x.Id, f => f.UniqueIndex)
//             .RuleFor(x => x.AgreementId, () => MockAgreement.Id)
//             .RuleFor(x => x.Agreement, () => MockAgreement)
//             .RuleFor(x => x.Method, f => f.Random.Word())
//             .RuleFor(x => x.Amount, f => f.Random.Int())
//             .RuleFor(x => x.DateFrom, f => dateRangeFromInRange)
//             .RuleFor(x => x.DateTo, () => dateRangeToInRange)
//             .RuleFor(x => x.IsActive, () => true)
//             .Generate(2);
//
//         MockPayment.DateFrom = dateRangeFromOutsideRange;
//         MockPayment.DateTo = dateRangeToOutsideRange;
//         _appDbContext.Payments.Add(MockPayment);
//         _appDbContext.Payments.AddRange(newPayment);
//         await _appDbContext.SaveChangesAsync();
//         Assume.That(_appDbContext.Payments.Count(), Is.EqualTo(3));
//
//         var query = new QueryPayment
//         {
//             ValidRangeFrom = dateSearchFrom,
//             ValidRangeTo = dateSearchTo
//         };
//
//         // Act
//         var result = (await _paymentRepository.BrowseAllAsync(query)).ToList();
//
//         // Assert
//         Assert.Multiple(() => {
//             Assert.That(result, Has.Count.EqualTo(2));
//             Assert.That(result.All(x => x.DateFrom == dateRangeFromInRange), Is.True);
//             Assert.That(result.All(x => x.DateTo == dateRangeToInRange), Is.True);
//         });
//     }
//
//     [Test]
//     public async Task ShouldUpdate()
//     {
//         // Arrange
//         var newPayment = new Faker<Payment>()
//             .RuleFor(x => x.Id, () => 1)
//             .RuleFor(x => x.AgreementId, () => MockAgreement.Id)
//             .RuleFor(x => x.Agreement, () => MockAgreement)
//             .RuleFor(x => x.Method, f => f.Random.Word())
//             .RuleFor(x => x.Amount, f => f.Random.Int())
//             .RuleFor(x => x.DateFrom, f => f.Date.Past())
//             .RuleFor(x => x.DateTo, f => f.Date.Future())
//             .RuleFor(x => x.IsActive, () => false)
//             .Generate();
//
//         _appDbContext.Payments.Add(newPayment);
//         await _appDbContext.SaveChangesAsync();
//         newPayment.Method = Guid.NewGuid()
//             .ToString();
//
//         Assume.That(_appDbContext.Payments.Count(), Is.EqualTo(1));
//
//         // Act
//         await _paymentRepository.UpdateAsync(newPayment, 1);
//
//         // Assert
//         var updatedPayment = _appDbContext.Payments.First();
//         Assert.That(updatedPayment.Method, Is.EqualTo(newPayment.Method));
//     }
//
//     [Test]
//     public void ShouldNotUpdate_NotFound()
//     {
//         // Assert
//         Assert.ThrowsAsync<PaymentNotFoundException>(async () =>
//             await _paymentRepository.UpdateAsync(new Payment(), 1));
//     }
//
//     [Test]
//     public async Task ShouldDeactivate()
//     {
//         // Arrange
//         var paymentEntity = _appDbContext.Payments.Add(MockPayment)
//             .Entity;
//         await _appDbContext.SaveChangesAsync();
//
//         Assume.That(_appDbContext.Payments.Count(), Is.EqualTo(1));
//
//         // Act
//         await _paymentRepository.Deactivate(paymentEntity.Id);
//
//         // Assert
//         var payment = _appDbContext.Payments.First();
//         Assert.That(payment.IsActive, Is.False);
//     }
//
//     [Test]
//     public void ShouldNotDeactivate_NotFound()
//     {
//         // Assert
//         Assert.ThrowsAsync<PaymentNotFoundException>(async () =>
//             await _paymentRepository.Deactivate(1));
//     }
// }