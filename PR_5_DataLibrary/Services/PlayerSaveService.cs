using PR_5_DataLibrary.Models;

namespace PR_5_DataLibrary.Services;

public class PlayerSaveService
{
    // Underling data access layer
    private readonly IRepository<PlayerSave> _repository;

    public PlayerSaveService(IRepository<PlayerSave> repository)
    {
        _repository = repository;
    }

    // Returns all save slots
    public async Task<List<PlayerSave>> GetAllAsync()
        => await _repository.GetAllAsync();
    
    // Returns one slot by id
    public async Task<PlayerSave?> GetByIdAsync(int id)
        => await _repository.GetByIdAsync(id);
    
    // Creates new save slot with defaults
    public async Task AddAsync(PlayerSave save)
    {
        save.CreatedAt = DateTime.Now;
        save.LastSavedAt = DateTime.Now;
        save.Health = save.MaxHealth;
        await _repository.AddAsync(save);
    }
    
    // Updates existing slot and refreshes save timestamp
    public async Task SaveProgressAsync(PlayerSave save)
    {
        save.LastSavedAt = DateTime.Now;
        await _repository.UpdateAsync(save);
    }
    
    // Removes save slot permanently
    public async Task DeleteAsync(int id)
        => await _repository.DeleteAsync(id);
    
    // Checks if player has enough memories for Ending A
    public async Task<bool> IsEndingAAsync(int saveID)
    {
        var save = await _repository.GetByIdAsync(saveID);
        return save != null && save.MemoryPercentage >= 70f;
    }
    
    // Adds boss to defeated list and updates save
    public async Task MarkBossDefeatedAsync(int saveID, string bossName)
    {
        var save = await _repository.GetByIdAsync(saveID);
        if (save == null) return;
        
        if  (!save.DefeatedBosses.Contains(bossName))
            save.DefeatedBosses.Add(bossName);
        
        await SaveProgressAsync(save);
    }
    
    // Records that a vision was shown to player
    public async Task MarkVisionSeenAsync(int saveID, int visionId)
    {
        var save = await _repository.GetByIdAsync(saveID);
        if (save == null) return;
        
        if (!save.SeenVisions.Contains(visionId)) 
            save.SeenVisions.Add(visionId); 
        
        await SaveProgressAsync(save);
    }
}
