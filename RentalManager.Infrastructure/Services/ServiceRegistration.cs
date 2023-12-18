using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using RentalManager.Core.Repositories;
using RentalManager.Infrastructure.Commands.AgreementCommands;
using RentalManager.Infrastructure.Commands.ClientCommands;
using RentalManager.Infrastructure.Commands.EquipmentCommands;
using RentalManager.Infrastructure.Commands.PaymentCommands;
using RentalManager.Infrastructure.Commands.UserCommands;
using RentalManager.Infrastructure.Repositories;
using RentalManager.Infrastructure.Services.Implementation;
using RentalManager.Infrastructure.Services.Interfaces;
using RentalManager.Infrastructure.Validators.AgreementValidators;
using RentalManager.Infrastructure.Validators.ClientValidators;
using RentalManager.Infrastructure.Validators.EquipmentValidators;
using RentalManager.Infrastructure.Validators.PaymentValidators;
using RentalManager.Infrastructure.Validators.UserValidators;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Enums;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace RentalManager.Infrastructure.Services;

public static class ServiceRegistration
{
    public static void RegisterApiServices(this IServiceCollection services)
    {
        services.AddScoped<IClientRepository, ClientRepository>();
        services.AddScoped<IClientService, ClientService>();

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserService, UserService>();

        services.AddScoped<IEquipmentRepository, EquipmentRepository>();
        services.AddScoped<IEquipmentService, EquipmentService>();

        services.AddScoped<IAgreementRepository, AgreementRepository>();
        services.AddScoped<IAgreementService, AgreementService>();

        services.AddScoped<IPaymentRepository, PaymentRepository>();
        services.AddScoped<IPaymentService, PaymentService>();
    }

    public static void RegisterValidatorServices(this IServiceCollection services)
    {
        services.AddFluentValidationAutoValidation(configuration => {
            configuration.ValidationStrategy = ValidationStrategy.All;
            configuration.EnableFormBindingSourceAutomaticValidation = true;
        });

        services.AddScoped<IValidator<CreateUser>, CreateUserValidator>();
        services.AddScoped<IValidator<UpdateUser>, UpdateUserValidator>();

        services.AddScoped<IValidator<CreateEquipment>, CreateEquipmentValidator>();
        services.AddScoped<IValidator<UpdateEquipment>, UpdateEquipmentValidator>();

        services.AddScoped<IValidator<CreatePayment>, CreatePaymentValidator>();
        services.AddScoped<IValidator<UpdatePayment>, UpdatePaymentValidator>();

        services.AddScoped<IValidator<CreateClient>, CreateClientValidator>();
        services.AddScoped<IValidator<UpdateClient>, UpdateClientValidator>();

        services.AddScoped<IValidator<CreatePayment>, CreatePaymentValidator>();
        services.AddScoped<IValidator<UpdatePayment>, UpdatePaymentValidator>();

        services.AddScoped<IValidator<CreateAgreement>, CreateAgreementValidator>();
        services.AddScoped<IValidator<UpdateAgreement>, UpdateAgreementValidator>();

        services.AddFluentValidationAutoValidation();
    }
}