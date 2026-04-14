using System.Reflection;
using PR_5_DataLibrary.Infrastructure;

namespace PR_5_DataLibrary.Repositories;

public class JsonRepository<T> : IRepository<T> where T : class
{
    private readonly FileStorageProvider _storage;
    private readonly IDataSerializer _serializer;
    
    // JSON filename for this entity type (e.g. "Saves.json")
    private readonly string _fileName;

    public JsonRepository(FileStorageProvider storage, IDataSerializer serializer, string fileName)
    {
        _storage = storage;
        _serializer = serializer;
        _fileName = fileName;
    }
    
    // Reads and deserializes all records
    public async Task<List<T>> GetAllAsync()
    {
        var json = await _storage.ReadAsync(_fileName);
        return _serializer.Deserialize<List<T>>(json) ?? new List<T>();
    }
    
    // Finds single record by its Id property
    public async Task<T?> GetByIdAsync(int id)
    {
        var all = await GetAllAsync();
        var idProp = GetIdProperty();
        return all.FirstOrDefault(x => (int?)idProp?.GetValue(x) == id);
    }
    
    // Auto-increments Id and appends record
    public async Task AddAsync(T entity)
    {
        var all = await GetAllAsync();
        var idProp = GetIdProperty();
 
        if (idProp != null)
        {
            // Find current max id and add 1
            var maxId = all.Count > 0
                ? all.Max(x => (int)(idProp.GetValue(x) ?? 0))
                : 0;
 
            idProp.SetValue(entity, maxId + 1);
        }
 
        all.Add(entity);
        await SaveAllAsync(all);
    }
 
    // Replaces record with same Id
    public async Task UpdateAsync(T entity)
    {
        var all = await GetAllAsync();
        var idProp = GetIdProperty();
        var entityId = (int?)idProp?.GetValue(entity);
 
        var index = all.FindIndex(x => (int?)idProp?.GetValue(x) == entityId);
 
        if (index >= 0)
            all[index] = entity;
 
        await SaveAllAsync(all);
    }
 
    // Removes record with matching Id
    public async Task DeleteAsync(int id)
    {
        var all = await GetAllAsync();
        var idProp = GetIdProperty();
        all.RemoveAll(x => (int?)idProp?.GetValue(x) == id);
        await SaveAllAsync(all);
    }
 
    // Serializes and writes full list to file
    private async Task SaveAllAsync(List<T> data)
    {
        var json = _serializer.Serialize(data);
        await _storage.WriteAsync(_fileName, json);
    }
 
    // Gets Id property via reflection (works for any model)
    private PropertyInfo? GetIdProperty()
    {
        return typeof(T).GetProperty("Id");
    }
}