using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using RentalManager.Core.Repositories;
using RentalManager.Infrastructure.Models.Commands.AgreementCommands;
using RentalManager.Infrastructure.Models.Commands.ClientCommands;
using RentalManager.Infrastructure.Models.Commands.EquipmentCommands;
using RentalManager.Infrastructure.Models.Commands.PaymentCommands;
using RentalManager.Infrastructure.Repositories;
using RentalManager.Infrastructure.Services.AgreementService;
using RentalManager.Infrastructure.Services.ClientService;
using RentalManager.Infrastructure.Services.EquipmentService;
using RentalManager.Infrastructure.Services.PaymentService;
using RentalManager.Infrastructure.Services.UserService;
using RentalManager.Infrastructure.Validators.AgreementValidators;
using RentalManager.Infrastructure.Validators.ClientValidators;
using RentalManager.Infrastructure.Validators.EquipmentValidators;
using RentalManager.Infrastructure.Validators.PaymentValidators;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Enums;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;

namespace RentalManager.Infrastructure.Services;

public static class ServiceRegistration
{
    public static void RegisterApiServices(this IServiceCollection services)
    {
        services.AddScoped<IClientRepository, ClientRepository>();
        services.AddScoped<IClientService, ClientService.ClientService>();

        services.AddScoped<IUserService, UserService.UserService>();

        services.AddScoped<IEquipmentRepository, EquipmentRepository>();
        services.AddScoped<IEquipmentService, EquipmentService.EquipmentService>();

        services.AddScoped<IAgreementRepository, AgreementRepository>();
        services.AddScoped<IAgreementService, AgreementService.AgreementService>();

        services.AddScoped<IPaymentRepository, PaymentRepository>();
        services.AddScoped<IPaymentService, PaymentService.PaymentService>();
    }

    public static void RegisterValidatorServices(this IServiceCollection services)
    {
        services.AddFluentValidationAutoValidation(configuration => {
            configuration.ValidationStrategy = ValidationStrategy.All;
            configuration.EnableFormBindingSourceAutomaticValidation = true;
        });

        services.AddScoped<IValidator<CreateEquipmentCommand>, CreateEquipmentValidator>();
        services.AddScoped<IValidator<UpdateEquipmentCommand>, UpdateEquipmentValidator>();

        services.AddScoped<IValidator<CreatePaymentCommand>, CreatePaymentValidator>();
        services.AddScoped<IValidator<UpdatePaymentCommand>, UpdatePaymentValidator>();

        services.AddScoped<IValidator<CreateClientCommand>, CreateClientValidator>();
        services.AddScoped<IValidator<UpdateClientCommand>, UpdateClientValidator>();

        services.AddScoped<IValidator<CreatePaymentCommand>, CreatePaymentValidator>();
        services.AddScoped<IValidator<UpdatePaymentCommand>, UpdatePaymentValidator>();

        services.AddScoped<IValidator<CreateAgreementCommand>, CreateAgreementValidator>();
        services.AddScoped<IValidator<UpdateAgreementCommand>, UpdateAgreementValidator>();

        services.AddFluentValidationAutoValidation();
    }
}