using RentalManager.Core.Domain;
using RentalManager.Infrastructure.Commands;
using System.Text.RegularExpressions;

namespace RentalManager.Infrastructure.DTO
{
    public static class ObjectConversion
    {
        private static string ToUpperCheckForNull(this string str)
        {
            if (!String.IsNullOrEmpty(str))
            {
                return str.ToUpper();
            }
            return str;
        }
        #region Client Conversions
        public static Client ToDomain(this CreateClient createClient)
        {
            return new Client()
            {
                Name = createClient.Name,
                Surname = createClient.Surname,
                PhoneNumber = createClient.PhoneNumber,
                Email = createClient.Email,
                IdCard = ToUpperCheckForNull(createClient.IdCard),
                City = createClient.City,
                Street = createClient.Street,
                StreetNumber = createClient.StreetNumber,
                DateAdded = DateTime.Now
            };
        }
        public static ClientDTO ToDTO(this Client client)
        {
            var clientDTO = new ClientDTO()
            {
                Id = client.Id,
                Name = client.Name,
                Surname = client.Surname,
                PhoneNumber = Regex.Replace(Regex.Replace(client.PhoneNumber, @"\s+", ""), ".{3}", "$0 ").TrimEnd(' '),
                Email = client.Email,
                IdCard = ToUpperCheckForNull(client.IdCard),
                City = client.City,
                Street = client.Street,
                StreetNumber = client.StreetNumber,
                DateAdded = client.DateAdded,
            };
            return clientDTO;
        }
        public static Client ToDomain(this UpdateClient updateClient)
        {
            var result = new Client()
            {
                Name = updateClient.Name,
                Surname = updateClient.Surname,
                PhoneNumber = Regex.Replace(updateClient.PhoneNumber, ".{3}", "$0 ").TrimEnd(' '),
                Email = updateClient.Email,
                IdCard = ToUpperCheckForNull(updateClient.IdCard),
                City = updateClient.City,
                Street = updateClient.Street,
                StreetNumber = updateClient.StreetNumber,
            };
            return result;
        }
        public static Client ToDomain(this ClientDTO clientDTO)
        {
            var result = new Client()
            {
                Id = clientDTO.Id,
                Name = clientDTO.Name,
                Surname = clientDTO.Surname,
                PhoneNumber = Regex.Replace(clientDTO.PhoneNumber, ".{3}", "$0 ").TrimEnd(' '),
                Email = clientDTO.Email,
                IdCard = ToUpperCheckForNull(clientDTO.IdCard),
                City = clientDTO.City,
                Street = clientDTO.Street,
                StreetNumber = clientDTO.StreetNumber,
                DateAdded = clientDTO.DateAdded
            };
            return result;
        }
        #endregion
        #region Employee Conversions
        public static Employee ToDomain(this CreateEmployee createEmployee)
        {
            return new Employee()
            {
                Name = createEmployee.Name,
                Surname = createEmployee.Surname,
                DateAdded = DateTime.Now
            };
        }
        public static EmployeeDTO ToDTO(this Employee employee)
        {
            return new EmployeeDTO()
            {
                Id = employee.Id,
                Name = employee.Name,
                Surname = employee.Surname,
                DateAdded = employee.DateAdded
            };
        }
        public static Employee ToDomain(this UpdateEmployee updateEmployee)
        {
            var result = new Employee()
            {
                Name = updateEmployee.Name,
                Surname = updateEmployee.Surname
            };
            return result;
        }
        #endregion
        #region RentalEquipment Conversions
        public static RentalEquipment ToDomain(this CreateRentalEquipment createRentalEquipment)
        {
            return new RentalEquipment()
            {
                Name = createRentalEquipment.Name,
                MonthlyPrice = createRentalEquipment.MonthlyPrice,
                DateAdded = DateTime.Now
            };
        }
        public static RentalEquipmentDTO ToDTO(this RentalEquipment rentalEquipment)
        {
            return new RentalEquipmentDTO()
            {
                Id = rentalEquipment.Id,
                Name = rentalEquipment.Name,
                MonthlyPrice = rentalEquipment.MonthlyPrice,
                DateAdded= rentalEquipment.DateAdded
            };
        }
        public static RentalEquipment ToDomain(this UpdateRentalEquipment updateRentalEquipment)
        {
            var result = new RentalEquipment()
            {
                Name = updateRentalEquipment.Name,
                MonthlyPrice = updateRentalEquipment.MonthlyPrice
            };
            return result;
        }
        public static RentalEquipment ToDomain(this RentalEquipmentDTO rentalEquipmentDTO)
        {
            var result = new RentalEquipment()
            {
                Id = rentalEquipmentDTO.Id,
                Name = rentalEquipmentDTO.Name,
                MonthlyPrice = rentalEquipmentDTO.MonthlyPrice,
                DateAdded = rentalEquipmentDTO.DateAdded
            };
            return result;
        }
        #endregion
        #region RentalAgreement Conversions
        public static RentalAgreement ToDomain(this CreateRentalAgreement createRentalAgreement)
        {
            return new RentalAgreement()
            {
                EmployeeId = createRentalAgreement.EmployeeId,
                IsActive = createRentalAgreement.IsActive,
                ClientId = createRentalAgreement.ClientId,
                Comment = createRentalAgreement.Comment,
                Deposit = createRentalAgreement.Deposit,
                TransportFrom = createRentalAgreement.TransportFrom,
                TransportTo = createRentalAgreement.TransportTo,
                ValidUntil = createRentalAgreement.ValidUntil,
                DateAdded = createRentalAgreement.DateAdded
            };
        }
        public static RentalAgreementDTO ToDTO(this RentalAgreement rentalAgreement)
        {
            return new RentalAgreementDTO()
            {
                Id = rentalAgreement.Id,
                Employee = rentalAgreement.Employee.ToDTO(),
                IsActive = rentalAgreement.IsActive,
                Client = rentalAgreement.Client.ToDTO(),
                RentalEquipment = rentalAgreement.RentalEquipment.Select(x => x.ToDTO()),
                Payments = rentalAgreement.Payments.Select(x => x.ToDTO()),
                Comment = rentalAgreement.Comment,
                Deposit = rentalAgreement.Deposit,
                TransportFrom = rentalAgreement.TransportFrom,
                TransportTo = rentalAgreement.TransportTo,
                ValidUntil = rentalAgreement.ValidUntil,
                DateAdded = rentalAgreement.DateAdded,
            };
        }
        public static RentalAgreement ToDomain(this UpdateRentalAgreement updateRentalAgreement)
        {
            var result = new RentalAgreement()
            {
                EmployeeId = updateRentalAgreement.EmployeeId,
                IsActive = updateRentalAgreement.IsActive,
                ClientId = updateRentalAgreement.ClientId,
                Comment = updateRentalAgreement.Comment,
                Deposit = updateRentalAgreement.Deposit,
                TransportFrom = updateRentalAgreement.TransportFrom,
                TransportTo = updateRentalAgreement.TransportTo,
                ValidUntil = updateRentalAgreement.ValidUntil,
                DateAdded = updateRentalAgreement.DateAdded,
            };
            return result;
        }
        #endregion
        #region Payment Conversions
        public static Payment ToDomain(this CreatePayment createPayment)
        {
            return new Payment()
            {
                RentalAgreementId = createPayment.RentalAgreementId,
                Method = createPayment.Method,
                Amount = createPayment.Amount,
                DateAdded = DateTime.Now
            };
        }
        public static PaymentDTO ToDTO(this Payment payment)
        {
            return new PaymentDTO()
            {
                Id = payment.Id,
                RentalAgreementId = payment.RentalAgreementId,
                Method = payment.Method,
                Amount = payment.Amount,
                DateAdded = payment.DateAdded
            };
        }
        public static Payment ToDomain(this UpdatePayment updatePayment)
        {
            return new Payment()
            {
                RentalAgreementId = updatePayment.RentalAgreementId,
                Method = updatePayment.Method,
                Amount = updatePayment.Amount
            };
        }
        #endregion
    }
}
