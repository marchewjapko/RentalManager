namespace RentalManager.Infrastructure.Commands
{
    public class CreateRentalAgreement
    {
        public int EmployeeId { get; set; }
        public bool IsActive { get; set; }
        public int ClientId { get; set; }
        public List<int> RentalEquipmentIds { get; set; }
        public string? Comment { get; set; }
        public int Deposit { get; set; }
        public int? TransportFrom { get; set; }
        public int TransportTo { get; set; }
        public DateTime DateAdded { get; set; }
        public List<CreatePayment> Payments { get; set; }
    }
}
