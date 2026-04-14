namespace PR_5_DataLibrary.Models;

// All significant in-game moments worth logging
public enum GameEventType
{
    Checkpoint,       // player reached a save point
    PlayerDeath,      // player died
    BossDefeated,     // boss fight won
    VisionTriggered,  // vision was shown
    MemoryCollected,  // fragment picked up
    RelicObtained     // relic received from boss
}

public class GameEvent
{
    // Unique event id 
    public int Id { get; set; }
    
    // Which save slot this event belongs to
    public int SaveSlotId { get; set; }
    
    // Type of event that occurred
    public GameEventType EventType { get; set; }
    
    // Human-readable event description 
    public string Description { get; set; } = string.Empty;
    
    // Zone number where event happened
    public int Zone { get; set; }
    
    // Exact time of event
    public DateTime OccurredAt { get; set; }
    
    // Optional extra data as JSON string (e.g. attempts count, time spent)
    public string? Metadata { get; set; }
    
}
