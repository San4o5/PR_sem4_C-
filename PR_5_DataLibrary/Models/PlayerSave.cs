namespace PR_5_DataLibrary.Models;

public class PlayerSave
{
    // Unique save slot id
    public int Id { get; set; }
    
    // Display name for the slot
    public string SlotName { get; set; } = string.Empty;
    
    // Current zone number
    public int CurrentZone { get; set; }
    
    // Latest reached checkpoint key
    public string CurrentCheckpoint { get; set; } = string.Empty;
    
    // Current and max health
    public int Health { get; set; }
    public int MaxHealth { get; set; }
    
    // Relics currently equipped
    public List<string> EquippedRelics { get; set; } = new();
    
    // All relics ever picked up
    public List<string> CollectedRelics { get; set; } = new();
    
    // Total memories available in current zone
    public int MemoryFragmentsTotal { get; set; }
    
    // How many the player found
    public int MemoryFragmentsCollected { get; set; }
    
    // Auto-calculated: determines ending A or B (>=70 -> Ending A)
    public float MemoryPercentage => 
        MemoryFragmentsTotal == 0 ? 0f : 
            (float)MemoryFragmentsCollected / MemoryFragmentsTotal * 100f;
    
    // Vision ids the player has already seen
    public List<int> SeenVisions { get; set; } = new();
    
    // Boss names that were defeated
    public List<string> DefeatedBosses { get; set; } = new();
    
    // Slot creation time
    public DateTime CreatedAt { get; set; }
    
    // Last time the game was saved
    public DateTime LastSavedAt { get; set; }
}