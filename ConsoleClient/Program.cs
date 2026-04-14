using PR_5_DataLibrary.Infrastructure;
using PR_5_DataLibrary.Models;
using PR_5_DataLibrary.Repositories;
using PR_5_DataLibrary.Services;

// Налаштування кодування для коректного відображення символів в консолі
Console.OutputEncoding = System.Text.Encoding.UTF8;

// ─── Шляхи до даних ───────────────────────────────────────
// Піднімаємося на 3 рівні вгору від bin/Debug/netX.X, щоб потрапити в корінь ConsoleClient
var projectRoot = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", ".."));
var dataPath = Path.Combine(projectRoot, "GameData");

// ─── Ініціалізація інфраструктури ─────────────────────────
var storage = new FileStorageProvider(dataPath);
var serializer = new JsonDataSerializer();

// Репозиторії (по одному на файл)
var saveRepo = new JsonRepository<PlayerSave>(storage, serializer, "saves.json");
var memoryRepo = new JsonRepository<MemoryFragment>(storage, serializer, "memories.json");
var eventRepo = new JsonRepository<GameEvent>(storage, serializer, "events.json");

// Сервіси
var saveService = new PlayerSaveService(saveRepo);
var memoryService = new MemoryService(memoryRepo);
var eventService = new GameEventService(eventRepo);

// ─── Симуляція: Початок нової гри ─────────────────────────
Console.WriteLine("=== Ashen Forgotten — Save System Demo ===\n");

var newSave = new PlayerSave
{
    SlotName = "Slot 1",
    CurrentZone = 1,
    CurrentCheckpoint = "checkpoint_zone1_start",
    Health = 100,
    MaxHealth = 100,
    MemoryFragmentsTotal = 5,
    MemoryFragmentsCollected = 0,
    CreatedAt = DateTime.Now
};

await saveService.AddAsync(newSave);
Console.WriteLine($"[+] Створено новий слот: \"{newSave.SlotName}\" (Id: {newSave.Id})");

// ─── Симуляція: Наповнення світу спогадами ────────────────
var fragments = new List<MemoryFragment>
{
    new() { Type = MemoryType.Note, Zone = MemoryZone.AshCapital, Title = "Commander's Order", RevealsTruth = false },
    new() { Type = MemoryType.Ghost, Zone = MemoryZone.AshCapital, Title = "Fallen Soldier", RevealsTruth = false },
    new() { Type = MemoryType.Item, Zone = MemoryZone.AshCapital, Title = "Kein's Research Page", RevealsTruth = true }
};

foreach (var f in fragments) await memoryService.AddAsync(f);
Console.WriteLine($"[+] Додано фрагменти спогадів у базу.");

// ─── Симуляція: Геймплей (Збір предметів та події) ────────
// 1. Збираємо спогади
await memoryService.CollectAsync(fragments[0].Id);
await memoryService.CollectAsync(fragments[2].Id); 

// 2. Оновлюємо локальний об'єкт 'save', щоб списки не були порожніми
var save = (await saveService.GetByIdAsync(newSave.Id))!;

// Синхронізуємо дані
save.MemoryFragmentsCollected = (await memoryService.GetCollectedAsync()).Count;

// 3. Тріггер візії
save.SeenVisions.Add(1); // Додаємо в локальний список
await eventService.LogAsync(save.Id, GameEventType.VisionTriggered, "Vision #1: Kein says 'Find me.'", zone: 1);
Console.WriteLine("[+] Візія #1 активована.");

// 4. Смерті та поразки
await eventService.LogDeathAsync(save.Id, zone: 1);
await eventService.LogDeathAsync(save.Id, zone: 1);

// 5. Перемога над босом
save.Health = 35;
save.CurrentCheckpoint = "checkpoint_zone1_boss";
if (!save.DefeatedBosses.Contains("Warden"))
    save.DefeatedBosses.Add("Warden"); // Додаємо в локальний список

await eventService.LogBossDefeatedAsync(save.Id, "Warden", zone: 1, attempts: 3);
save.CollectedRelics.Add("WardenShield");
save.EquippedRelics.Add("WardenShield");

// 6. ФІНАЛЬНЕ ЗБЕРЕЖЕННЯ (записуємо всі зміни локального об'єкта у файл)
await saveService.SaveProgressAsync(save);
Console.WriteLine("[+] Прогрес збережено (Боси, Візії, ХП оновлено).");

// ─── Summary (Вивід результатів) ─────────────────────────
Console.WriteLine("\n=== Session Summary ===");

// Завантажуємо свіжі дані з файлу для перевірки
var finalSave = (await saveService.GetByIdAsync(save.Id))!;
var deaths = await eventService.GetDeathCountAsync(save.Id);
var isEndingA = await saveService.IsEndingAAsync(save.Id);
var truthMems = await memoryService.GetTruthFragmentsAsync();

Console.WriteLine($"Slot:          {finalSave.SlotName}");
Console.WriteLine($"Zone:          {finalSave.CurrentZone}");
Console.WriteLine($"HP:            {finalSave.Health}/{finalSave.MaxHealth}");
Console.WriteLine($"Bosses:        {(finalSave.DefeatedBosses.Any() ? string.Join(", ", finalSave.DefeatedBosses) : "None")}");
Console.WriteLine($"Memories:      {finalSave.MemoryFragmentsCollected}/{finalSave.MemoryFragmentsTotal} ({finalSave.MemoryPercentage:F1}%)");
Console.WriteLine($"Truth hints:   {truthMems.Count(t => t.IsCollected)}/{truthMems.Count} found");
Console.WriteLine($"Deaths:        {deaths}");
Console.WriteLine($"Visions seen:  {(finalSave.SeenVisions.Any() ? string.Join(", ", finalSave.SeenVisions.Select(v => $"#{v}")) : "None")}");
Console.WriteLine($"Ending:        {(isEndingA ? "A — Hero walks free" : "B — Kein completes the ritual")}");
Console.WriteLine($"Last saved:    {finalSave.LastSavedAt:HH:mm:ss}");

Console.WriteLine($"\nData folder:   {dataPath}");