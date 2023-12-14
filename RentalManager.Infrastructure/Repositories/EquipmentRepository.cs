using Microsoft.EntityFrameworkCore;
using RentalManager.Core.Domain;
using RentalManager.Core.Repositories;
using RentalManager.Infrastructure.Exceptions;
using RentalManager.Infrastructure.Repositories.DbContext;

namespace RentalManager.Infrastructure.Repositories;

public class EquipmentRepository(AppDbContext appDbContext) : IEquipmentRepository
{
    public async Task<Equipment> AddAsync(Equipment equipment)
    {
        try
        {
            appDbContext.Equipment.Add(equipment);
            await appDbContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new Exception("Unable to add rental equipment\n" + ex.Message);
        }

        return await Task.FromResult(equipment);
    }

    public async Task DeleteAsync(int id)
    {
        var result = await appDbContext.Equipment.FirstOrDefaultAsync(x => x.Id == id);

        if (result == null)
        {
            throw new EquipmentNotFoundException(id);
        }

        appDbContext.Equipment.Remove(result);
        await appDbContext.SaveChangesAsync();
    }

    public async Task<Equipment> GetAsync(int id)
    {
        var result =
            await Task.FromResult(appDbContext.Equipment.FirstOrDefault(x => x.Id == id));

        if (result == null)
        {
            throw new EquipmentNotFoundException(id);
        }

        return result;
    }

    public async Task<IEnumerable<Equipment>> GetAsync(List<int> ids)
    {
        var result =
            await Task.FromResult(
                appDbContext.Equipment.Where(x => ids.Any(a => a == x.Id)));

        if (result == null)
        {
            throw new EquipmentNotFoundException(ids);
        }

        return result;
    }

    public async Task<IEnumerable<Equipment>> BrowseAllAsync(string? name = null,
        DateTime? from = null,
        DateTime? to = null)
    {
        var result = appDbContext.Equipment.Where(x => x.IsActive)
            .AsQueryable();
        if (name != null)
        {
            result = result.Where(x => x.Name.Contains(name));
        }

        if (from != null)
        {
            result = result.Where(x => x.CreatedTs.Date > from.Value.Date);
        }

        if (to != null)
        {
            result = result.Where(x => x.CreatedTs.Date < to.Value.Date);
        }

        return await Task.FromResult(result.AsEnumerable());
    }

    public async Task<Equipment> UpdateAsync(Equipment equipment, int id)
    {
        var equipmentToUpdate = appDbContext.Equipment.FirstOrDefault(x => x.Id == id);

        if (equipmentToUpdate == null)
        {
            throw new EquipmentNotFoundException(id);
        }

        equipmentToUpdate.Name = equipment.Name;
        equipmentToUpdate.Price = equipment.Price;
        equipmentToUpdate.Image = equipment.Image;
        await appDbContext.SaveChangesAsync();

        return await Task.FromResult(equipmentToUpdate);
    }

    public async Task Deactivate(int id)
    {
        var result = await appDbContext.Equipment.FirstOrDefaultAsync(x => x.Id == id);

        if (result == null)
        {
            throw new EquipmentNotFoundException(id);
        }

        result.IsActive = false;
        await appDbContext.SaveChangesAsync();
    }
}