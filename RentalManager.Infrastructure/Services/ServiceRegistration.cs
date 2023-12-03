using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using RentalManager.Core.Repositories;
using RentalManager.Infrastructure.Commands.AgreementCommands;
using RentalManager.Infrastructure.Commands.ClientCommands;
using RentalManager.Infrastructure.Commands.EmployeeCommands;
using RentalManager.Infrastructure.Commands.EquipmentCommands;
using RentalManager.Infrastructure.Commands.PaymentCommands;
using RentalManager.Infrastructure.Repositories;
using RentalManager.Infrastructure.Services.Implementation;
using RentalManager.Infrastructure.Services.Interfaces;
using RentalManager.Infrastructure.Validators;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Enums;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;

namespace RentalManager.Infrastructure.Services;

public static class ServiceRegistration
{
    public static void RegisterApiServices(IServiceCollection services)
    {
        services.AddScoped<IClientRepository, ClientRepository>();
        services.AddScoped<IClientService, ClientService>();

        services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        services.AddScoped<IEmployeeService, EmployeeService>();

        services.AddScoped<IEquipmentRepository, EquipmentRepository>();
        services.AddScoped<IEquipmentService, EquipmentService>();

        services.AddScoped<IAgreementRepository, AgreementRepository>();
        services.AddScoped<IAgreementService, AgreementService>();

        services.AddScoped<IPaymentRepository, PaymentRepository>();
        services.AddScoped<IPaymentService, PaymentService>();
    }

    public static void RegisterValidatorServices(IServiceCollection services)
    {
        services.AddFluentValidationAutoValidation(configuration => {
            configuration.ValidationStrategy = ValidationStrategy.All;
            configuration.EnableFormBindingSourceAutomaticValidation = true;
        });

        services.AddScoped<IValidator<CreateEmployee>, EmployeeValidator>();
        services.AddScoped<IValidator<UpdateEmployee>, EmployeeValidator>();

        services.AddScoped<IValidator<CreateEquipment>, EquipmentValidator>();
        services.AddScoped<IValidator<UpdateEquipment>, EquipmentValidator>();

        services.AddScoped<IValidator<CreatePayment>, PaymentValidator>();
        services.AddScoped<IValidator<UpdatePayment>, PaymentValidator>();

        services.AddScoped<IValidator<CreateClient>, ClientValidator>();
        services.AddScoped<IValidator<UpdateClient>, ClientValidator>();

        services.AddScoped<IValidator<CreatePayment>, PaymentValidator>();
        services.AddScoped<IValidator<UpdatePayment>, PaymentValidator>();        
        
        services.AddScoped<IValidator<CreateAgreement>, AgreementValidator>();
        services.AddScoped<IValidator<UpdateAgreement>, AgreementValidator>();

        services.AddFluentValidationAutoValidation();
    }
}