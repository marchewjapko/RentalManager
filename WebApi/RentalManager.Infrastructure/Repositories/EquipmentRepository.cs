using Microsoft.EntityFrameworkCore;
using RentalManager.Core.Domain;
using RentalManager.Core.Repositories;
using RentalManager.Global.Queries;
using RentalManager.Global.Queries.Sorting;
using RentalManager.Infrastructure.ExceptionHandling.Exceptions;
using RentalManager.Infrastructure.Repositories.DbContext;

namespace RentalManager.Infrastructure.Repositories;

public class EquipmentRepository(AppDbContext appDbContext) : IEquipmentRepository
{
    public async Task<Equipment> AddAsync(Equipment equipment)
    {
        var result = appDbContext.Equipments.Add(equipment);
        await appDbContext.SaveChangesAsync();

        return result.Entity;
    }

    public async Task DeleteAsync(int id)
    {
        var result = await appDbContext.Equipments.FirstOrDefaultAsync(x => x.Id == id);

        if (result == null)
        {
            throw new EquipmentNotFoundException(id);
        }

        appDbContext.Equipments.Remove(result);
        await appDbContext.SaveChangesAsync();
    }

    public async Task<Equipment> GetAsync(int id)
    {
        var result = await appDbContext.Equipments.FirstOrDefaultAsync(x => x.Id == id);

        if (result == null)
        {
            throw new EquipmentNotFoundException(id);
        }

        return result;
    }

    public async Task<ICollection<Equipment>> GetAsync(ICollection<int> ids)
    {
        var result = await appDbContext.Equipments.Where(x => ids.Any(a => a == x.Id))
            .ToListAsync();

        if (result.Count != ids.Count)
        {
            var missingIds = ids.Except(result.Select(x => x.Id));

            throw new EquipmentNotFoundException(missingIds);
        }

        return result;
    }

    public async Task<IEnumerable<Equipment>> BrowseAllAsync(QueryEquipment queryEquipment)
    {
        var result = appDbContext.Equipments.Where(x => x.IsActive)
            .AsQueryable();

        result = FilterClients(result, queryEquipment);

        result = SortClients(result, queryEquipment);

        return await result.ToListAsync();
    }

    public async Task<Equipment> UpdateAsync(Equipment equipment, int id)
    {
        var equipmentToUpdate = appDbContext.Equipments.FirstOrDefault(x => x.Id == id);

        if (equipmentToUpdate == null)
        {
            throw new EquipmentNotFoundException(id);
        }

        equipmentToUpdate.Name = equipment.Name;
        equipmentToUpdate.Price = equipment.Price;
        equipmentToUpdate.UpdatedTs = DateTime.Now;
        await appDbContext.SaveChangesAsync();

        return equipmentToUpdate;
    }

    public async Task Deactivate(int id)
    {
        var result = await appDbContext.Equipments.FirstOrDefaultAsync(x => x.Id == id);

        if (result == null)
        {
            throw new EquipmentNotFoundException(id);
        }

        result.IsActive = false;
        await appDbContext.SaveChangesAsync();
    }

    private static IQueryable<Equipment> FilterClients(IQueryable<Equipment> equipments,
        QueryEquipment queryEquipment)
    {
        if (queryEquipment.Name != null)
        {
            equipments = equipments.Where(x => x.Name.Contains(queryEquipment.Name));
        }

        if (queryEquipment.AddedFrom != null)
        {
            equipments =
                equipments.Where(x => x.CreatedTs.Date > queryEquipment.AddedFrom.Value.Date);
        }

        if (queryEquipment.AddedTo != null)
        {
            equipments =
                equipments.Where(x => x.CreatedTs.Date < queryEquipment.AddedTo.Value.Date);
        }

        if (queryEquipment.OnlyActive)
        {
            equipments = equipments.Where(x => x.IsActive);
        }

        return equipments;
    }

    private static IQueryable<Equipment> SortClients(IQueryable<Equipment> equipments,
        QueryEquipment queryEquipment)
    {
        if (queryEquipment.Descending)
        {
            equipments = queryEquipment.SortEquipmentBy switch
            {
                SortEquipmentBy.Id => equipments.OrderByDescending(x => x.Id),
                SortEquipmentBy.Name => equipments.OrderByDescending(x => x.Name),
                SortEquipmentBy.DateAdded => equipments.OrderByDescending(x => x.CreatedTs),
                SortEquipmentBy.Price => equipments.OrderByDescending(x => x.Price),
                _ => equipments.OrderByDescending(x => x.CreatedTs)
            };
        }
        else
        {
            equipments = queryEquipment.SortEquipmentBy switch
            {
                SortEquipmentBy.Id => equipments.OrderBy(x => x.Id),
                SortEquipmentBy.Name => equipments.OrderBy(x => x.Name),
                SortEquipmentBy.DateAdded => equipments.OrderBy(x => x.CreatedTs),
                SortEquipmentBy.Price => equipments.OrderBy(x => x.Price),
                _ => equipments.OrderBy(x => x.CreatedTs)
            };
        }

        equipments = equipments.Skip(queryEquipment.Position);
        equipments = equipments.Take(queryEquipment.PageSize);

        return equipments;
    }
}