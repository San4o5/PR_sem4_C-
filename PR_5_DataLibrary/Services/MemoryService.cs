using PR_5_DataLibrary.Models;

namespace PR_5_DataLibrary.Services;

public class MemoryService
{
    // Underlying data access layer
    private readonly IRepository<MemoryFragment> _repository;

    public MemoryService(IRepository<MemoryFragment> repository)
    {
        _repository = repository;
    }

    // Returns all memory fragments in the game
    public async Task<List<MemoryFragment>> GetAllAsync()
        => await _repository.GetAllAsync();

    // Returns one fragment by id
    public async Task<MemoryFragment?> GetByIdAsync(int id)
        => await _repository.GetByIdAsync(id);

    // Adds new fragment definition to storage
    public async Task AddAsync(MemoryFragment fragment)
        => await _repository.AddAsync(fragment);

    // Marks fragment as collected and records timestamp
    public async Task CollectAsync(int fragmentId)
    {
        var fragment = await _repository.GetByIdAsync(fragmentId);
        if (fragment == null) return;

        fragment.IsCollected = true;
        fragment.CollectedAt = DateTime.Now;

        await _repository.UpdateAsync(fragment);
    }

    // Returns only fragments already collected by player
    public async Task<List<MemoryFragment>> GetCollectedAsync()
    {
        var all = await _repository.GetAllAsync();
        return all.Where(f => f.IsCollected).ToList();
    }

    // Returns fragments for a specific zone
    public async Task<List<MemoryFragment>> GetByZoneAsync(MemoryZone zone)
    {
        var all = await _repository.GetAllAsync();
        return all.Where(f => f.Zone == zone).ToList();
    }

    // Returns fragments tied to a specific character 
    public async Task<List<MemoryFragment>> GetByCharacterAsync(string character)
    {
        var all = await _repository.GetAllAsync();
        return all.Where(f => f.RelatedCharacter == character).ToList();
    }

    // Returns only fragments that hint at Kein's true plan
    public async Task<List<MemoryFragment>> GetTruthFragmentsAsync()
    {
        var all = await _repository.GetAllAsync();
        return all.Where(f => f.RevealsTruth).ToList();
    }

    // Removes fragment by id
    public async Task DeleteAsync(int id)
        => await _repository.DeleteAsync(id);
}