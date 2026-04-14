using PR_5_DataLibrary.Models;

namespace PR_5_DataLibrary.Services;

public class GameEventService
{
    // Underlying data access layer
    private readonly IRepository<GameEvent> _repository;
    
    public GameEventService(IRepository<GameEvent> repository)
    {
        _repository = repository;
    }
    
    // Returns all events across all save
    public async Task<List<GameEvent>> GetAllAsync()
        => await _repository.GetAllAsync();
    
    // Returns all events for a specific save slot
    public async Task<List<GameEvent>> GetBySaveSlotAsync(int saveSlot)
    {
        var all = await _repository.GetAllAsync();
        return all.Where(e => e.SaveSlotId == saveSlot).ToList();
    }
    
    // Returns events filtered by type (e.g. only deaths)
    public async Task<List<GameEvent>> GetByPlayerAsync(int saveSlotId, GameEventType type)
    {
        var all = await GetBySaveSlotAsync(saveSlotId);
        return all.Where(e => e.EventType == type).ToList();
    }
    
    // Logs a new event with current timestamp
    public async Task LogAsync(int saveSlotId, GameEventType type, string description, int zone,
        string? metadata = null)
    {
        var gameEvent = new GameEvent
        {
            SaveSlotId = saveSlotId,
            EventType = type,
            Description = description,
            Zone = zone,
            OccurredAt = DateTime.Now,
            Metadata = metadata
        };
        
        await _repository.AddAsync(gameEvent);
    }
    
    // Shortcut: logs checkpoint reached
    public async Task LogCheckpointAsync(int saveSlotId, string checkpointName, int zone)
        => await LogAsync(saveSlotId, GameEventType.Checkpoint, $"Reached: {checkpointName}", zone);
 
    // Shortcut: logs player death
    public async Task LogDeathAsync(int saveSlotId, int zone)
        => await LogAsync(saveSlotId, GameEventType.PlayerDeath, "Player died", zone);
 
    // Shortcut: logs boss kill with optional attempt count
    public async Task LogBossDefeatedAsync(int saveSlotId, string bossName, int zone, int attempts = 1)
        => await LogAsync(saveSlotId, GameEventType.BossDefeated,
            $"{bossName} defeated", zone,
            $"{{\"attempts\":{attempts}}}");
 
    // Counts how many times player died in this run
    public async Task<int> GetDeathCountAsync(int saveSlotId)
    {
        var deaths = await GetByPlayerAsync(saveSlotId, GameEventType.PlayerDeath);
        return deaths.Count;
    }
    
    // Removes all events for a save slot (used on slot deletion)
    public async Task DeleteBySaveSlotAsync(int saveSlotId)
    {
        var all = await _repository.GetAllAsync();
        var toDelete = all.Where(e => e.SaveSlotId == saveSlotId).ToList();
        foreach (var ev in toDelete)
            await _repository.DeleteAsync(ev.Id);
    }
}