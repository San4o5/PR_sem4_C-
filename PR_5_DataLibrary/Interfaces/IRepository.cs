namespace PR_5_DataLibrary;

// Generic CRUD contract for any entity 
public interface IRepository<T> where T : class
{
    // Returns all records
    Task<List<T>> GetAllAsync();
    
    // Returns one record by id
    Task<T?> GetByIdAsync(int id);
    
    // Adds new record
    Task AddAsync(T entity);
    
    // Replaces existing record
    Task UpdateAsync(T entity);
    
    // Removes record by id
    Task DeleteAsync(int id);
}