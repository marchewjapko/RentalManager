// using Bogus;
// using Microsoft.AspNetCore.Http;
// using RentalManager.Infrastructure.Commands.UserCommands;
// using RentalManager.Infrastructure.DTO;
// using RentalManager.Infrastructure.Validators;
//
// namespace RentalManager.Tests.ValidatorsTests;
//
// public class UserValidatorTests
// {
//     private readonly UserValidator _userValidator = new();
//
//     private static UserBaseCommand InitializeUser()
//     {
//         return new Faker<UserBaseCommand>()
//             .RuleFor(x => x.Name, () => "John")
//             .RuleFor(x => x.Surname, f => f.Name.LastName())
//             .RuleFor(x => x.Gender, () => GenderDto.Man)
//             .Generate();
//     }
//
//     [Test]
//     public async Task ValidateUser_Success()
//     {
//         // arrange
//         var user = InitializeUser();
//
//         // act
//         var result = await _userValidator.ValidateAsync(user);
//
//         //assert
//         Assert.That(result.IsValid);
//     }
//
//     #region NameRules
//
//     [Test]
//     public async Task ValidateName_Success_Two_Names()
//     {
//         // arrange
//         var user = InitializeUser();
//         user.Name = "John Jack";
//
//         // act
//         var result = await _userValidator.ValidateAsync(user);
//
//         //assert
//         Assert.That(result.IsValid);
//     }
//
//     [Test]
//     public async Task ValidateName_Failure_Invalid_Character()
//     {
//         // arrange
//         var user = InitializeUser();
//         user.Name = "John;";
//
//         // act
//         var result = await _userValidator.ValidateAsync(user);
//
//         //assert
//         Assert.Multiple(() => {
//             Assert.That(!result.IsValid);
//             Assert.That(result.Errors, Has.Count.EqualTo(1));
//             Assert.That(result.Errors[0].ErrorCode, Is.EqualTo("RegularExpressionValidator"));
//         });
//     }
//
//     [Test]
//     public async Task ValidateName_Failure_Null()
//     {
//         // arrange
//         var user = InitializeUser();
//         user.Name = null!;
//
//         // act
//         var result = await _userValidator.ValidateAsync(user);
//
//         //assert
//         Assert.Multiple(() => {
//             Assert.That(!result.IsValid);
//             Assert.That(result.Errors, Has.Count.EqualTo(1));
//             Assert.That(result.Errors[0].ErrorCode, Is.EqualTo("NotEmptyValidator"));
//         });
//     }
//
//     [Test]
//     public async Task ValidateName_Failure_Too_Long()
//     {
//         // arrange
//         var user = InitializeUser();
//         user.Name = new string('A', 101);
//
//         // act
//         var result = await _userValidator.ValidateAsync(user);
//
//         // assert
//         Assert.Multiple(() => {
//             Assert.That(!result.IsValid);
//             Assert.That(result.Errors, Has.Count.EqualTo(1));
//             Assert.That(result.Errors[0].ErrorCode, Is.EqualTo("MaximumLengthValidator"));
//         });
//     }
//
//     #endregion
//
//     #region SurnameRules
//
//     [Test]
//     public async Task ValidateSurname_Success_Two_Surnames()
//     {
//         // arrange
//         var user = InitializeUser();
//         user.Surname = "Levinson Brown";
//
//         // act
//         var result = await _userValidator.ValidateAsync(user);
//
//         //assert
//         Assert.That(result.IsValid);
//     }
//
//     [Test]
//     public async Task ValidateSurname_Failure_Invalid_Character()
//     {
//         // arrange
//         var user = InitializeUser();
//         user.Surname = "Brown;";
//
//         // act
//         var result = await _userValidator.ValidateAsync(user);
//
//         //assert
//         Assert.Multiple(() => {
//             Assert.That(!result.IsValid);
//             Assert.That(result.Errors, Has.Count.EqualTo(1));
//             Assert.That(result.Errors[0].ErrorCode, Is.EqualTo("RegularExpressionValidator"));
//         });
//     }
//
//     [Test]
//     public async Task ValidateSurname_Failure_Null()
//     {
//         // arrange
//         var user = InitializeUser();
//         user.Surname = null!;
//
//         // act
//         var result = await _userValidator.ValidateAsync(user);
//
//         //assert
//         Assert.Multiple(() => {
//             Assert.That(!result.IsValid);
//             Assert.That(result.Errors, Has.Count.EqualTo(1));
//             Assert.That(result.Errors[0].ErrorCode, Is.EqualTo("NotEmptyValidator"));
//         });
//     }
//
//     [Test]
//     public async Task ValidateSurname_Failure_Too_Long()
//     {
//         // arrange
//         var user = InitializeUser();
//         user.Surname = new string('A', 101);
//
//         // act
//         var result = await _userValidator.ValidateAsync(user);
//
//         // assert
//         Assert.Multiple(() => {
//             Assert.That(!result.IsValid);
//             Assert.That(result.Errors, Has.Count.EqualTo(1));
//             Assert.That(result.Errors[0].ErrorCode, Is.EqualTo("MaximumLengthValidator"));
//         });
//     }
//
//     #endregion
//
//     #region ImageRules
//
//     [Test]
//     public async Task ValidateFile_Success()
//     {
//         // arrange
//
//         const string content = "file-content";
//         const string fileName = "test-file.png";
//         var stream = new MemoryStream();
//         var writer = new StreamWriter(stream);
//         await writer.WriteAsync(content);
//         await writer.FlushAsync();
//         stream.Position = 0;
//
//         var user = InitializeUser();
//         user.Image = new FormFile(stream, 0, stream.Length, "id_from_form", fileName);
//
//         // act
//         var result = await _userValidator.ValidateAsync(user);
//
//         //assert
//         Assert.That(result.IsValid);
//     }
//
//     [Test]
//     public async Task ValidateFile_Failure_Wrong_Extension()
//     {
//         // arrange
//         const string content = "file-content";
//         const string fileName = "test-file.txt";
//         var stream = new MemoryStream();
//         var writer = new StreamWriter(stream);
//         await writer.WriteAsync(content);
//         await writer.FlushAsync();
//         stream.Position = 0;
//
//         var user = InitializeUser();
//         user.Image = new FormFile(stream, 0, stream.Length, "id_from_form", fileName);
//
//         // act
//         var result = await _userValidator.ValidateAsync(user);
//
//         //assert
//         Assert.Multiple(() => {
//             Assert.That(!result.IsValid);
//             Assert.That(result.Errors, Has.Count.EqualTo(1));
//             Assert.That(result.Errors[0].ErrorMessage, Does.Contain("Unacceptable extension"));
//         });
//     }
//
//     [Test]
//     public async Task ValidateFile_Failure_Too_Large()
//     {
//         // arrange
//         var content = new string('A', 1024 * 1024 + 1);
//         const string fileName = "test-file.png";
//         var stream = new MemoryStream();
//         var writer = new StreamWriter(stream);
//         await writer.WriteAsync(content);
//         await writer.FlushAsync();
//         stream.Position = 0;
//
//         var user = InitializeUser();
//         user.Image = new FormFile(stream, 0, stream.Length, "id_from_form", fileName);
//
//         // act
//         var result = await _userValidator.ValidateAsync(user);
//
//         //assert
//         Assert.Multiple(() => {
//             Assert.That(!result.IsValid);
//             Assert.That(result.Errors, Has.Count.EqualTo(1));
//             Assert.That(result.Errors[0].ErrorMessage, Does.Contain("File too large"));
//         });
//     }
//
//     #endregion
// }

