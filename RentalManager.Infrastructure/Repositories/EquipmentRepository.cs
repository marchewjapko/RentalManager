using Microsoft.EntityFrameworkCore;
using RentalManager.Core.Domain;
using RentalManager.Core.Repositories;
using RentalManager.Infrastructure.Exceptions;

namespace RentalManager.Infrastructure.Repositories;

public class EquipmentRepository : IEquipmentRepository
{
    private readonly AppDbContext _appDbContext;

    public EquipmentRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<Equipment> AddAsync(Equipment equipment)
    {
        try
        {
            _appDbContext.Equipment.Add(equipment);
            await _appDbContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new Exception("Unable to add rental equipment\n" + ex.Message);
        }

        return await Task.FromResult(equipment);
    }

    public async Task DeleteAsync(int id)
    {
        var result = await _appDbContext.Equipment.FirstOrDefaultAsync(x => x.Id == id);

        if (result == null)
        {
            throw new EquipmentNotFoundException(id);
        }

        _appDbContext.Equipment.Remove(result);
        await _appDbContext.SaveChangesAsync();
    }

    public async Task<Equipment> GetAsync(int id)
    {
        var result =
            await Task.FromResult(_appDbContext.Equipment.FirstOrDefault(x => x.Id == id));

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
                _appDbContext.Equipment.Where(x => ids.Any(a => a == x.Id)));

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
        var result = _appDbContext.Equipment.AsQueryable();
        if (name != null)
        {
            result = result.Where(x => x.Name.Contains(name));
        }

        if (from != null)
        {
            result = result.Where(x => x.DateAdded.Date > from.Value.Date);
        }

        if (to != null)
        {
            result = result.Where(x => x.DateAdded.Date < to.Value.Date);
        }

        return await Task.FromResult(result.AsEnumerable());
    }

    public async Task<Equipment> UpdateAsync(Equipment equipment, int id)
    {
        var z = _appDbContext.Equipment.FirstOrDefault(x => x.Id == id);

        if (z == null)
        {
            throw new EquipmentNotFoundException(id);
        }

        z.Name = equipment.Name;
        z.Price = equipment.Price;
        z.Image = equipment.Image;
        await _appDbContext.SaveChangesAsync();

        return await Task.FromResult(z);
    }
}