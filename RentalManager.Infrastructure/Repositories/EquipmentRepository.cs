using Microsoft.EntityFrameworkCore;
using RentalManager.Core.Domain;
using RentalManager.Core.Repositories;
using RentalManager.Global.Queries;
using RentalManager.Infrastructure.Exceptions;
using RentalManager.Infrastructure.Repositories.DbContext;

namespace RentalManager.Infrastructure.Repositories;

public class EquipmentRepository(AppDbContext appDbContext) : IEquipmentRepository
{
    public async Task AddAsync(Equipment equipment)
    {
        appDbContext.Equipment.Add(equipment);
        await appDbContext.SaveChangesAsync();
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

    public async Task<IEnumerable<Equipment>> BrowseAllAsync(QueryEquipment queryEquipment)
    {
        var result = appDbContext.Equipment.Where(x => x.IsActive)
            .AsQueryable();

        if (queryEquipment.Name != null)
        {
            result = result.Where(x => x.Name.Contains(queryEquipment.Name));
        }

        if (queryEquipment.From != null)
        {
            result = result.Where(x => x.CreatedTs.Date > queryEquipment.From.Value.Date);
        }

        if (queryEquipment.To != null)
        {
            result = result.Where(x => x.CreatedTs.Date < queryEquipment.To.Value.Date);
        }

        if (queryEquipment.OnlyActive)
        {
            result = result.Where(x => x.IsActive);
        }

        return await Task.FromResult(result.AsEnumerable());
    }

    public async Task UpdateAsync(Equipment equipment, int id)
    {
        var equipmentToUpdate = appDbContext.Equipment.FirstOrDefault(x => x.Id == id);

        if (equipmentToUpdate == null)
        {
            throw new EquipmentNotFoundException(id);
        }

        equipmentToUpdate.Name = equipment.Name;
        equipmentToUpdate.Price = equipment.Price;
        equipmentToUpdate.Image = equipment.Image;
        equipmentToUpdate.UpdatedTs = DateTime.Now;
        await appDbContext.SaveChangesAsync();
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