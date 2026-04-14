namespace PR_5_DataLibrary.Models;

// Three memory types
public enum MemoryType
{
    Item,   // physical object found in world
    Ghost,  // NPC spirit encounter
    Note    // written record or inscription
}

// Maps to the five game zones
public enum MemoryZone
{
    AshCapital = 1,
    PoisonedForest = 2,
    AbandonedCatacombs = 3,
    MageAcademyRuins = 4,
    AshHeart = 5
}

public class MemoryFragment
{
    // Unique fragment id
    public int Id { get; set; }
    
    // Item / Ghost / Note
    public MemoryType Type { get; set; }
    
    // Zone where fragment is located
    public MemoryZone Zone { get; set; }
    
    // Short display name
    public string Title { get; set; } = string.Empty;
    
    // Full memory text shown to player
    public string Content { get; set; } = string.Empty;
    
    // Boss or character this memory belongs to 
    public string RelatedCharacter { get; set; } = string.Empty;
    
    // Whether player has picked this up
    public bool IsCollected { get; set; }
    
    // Null until player collects it
    public DateTime? CollectedAt { get; set; }
    
    // Marks key memories
    public bool RevealsTruth { get; set; }
}