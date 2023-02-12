namespace RentalManager.Infrastructure.DTO
{
    public class RentalAgreementDTO
    {
        public int Id { get; set; }
        public EmployeeDTO Employee { get; set; }
        public bool IsActive { get; set; }
        public ClientDTO Client { get; set; }
        public IEnumerable<RentalEquipmentDTO> RentalEquipment { get; set; }
        public IEnumerable<PaymentDTO> Payments { get; set; }
        public string? Comment { get; set; }
        public int Deposit { get; set; }
        public int? TransportFrom { get; set; }
        public int TransportTo { get; set; }
        public DateTime DateAdded { get; set; }
    }
}
