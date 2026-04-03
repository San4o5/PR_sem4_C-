namespace ModelVersioning;

public class Player
{
    public string Name { get; set; }
    public int Level { get; set; } = 1; // значення за замовчуванням для старих JSON
}