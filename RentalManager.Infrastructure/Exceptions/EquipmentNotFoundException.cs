namespace RentalManager.Infrastructure.Exceptions;

public class EquipmentNotFoundException : Exception
{
    public EquipmentNotFoundException(int id) : base($"Equipment with id {id} not found")
    {
    }

    public EquipmentNotFoundException(List<int> ids) : base(
        $"Equipment with ids {string.Join(", ", ids)} not found")
    {
    }
}