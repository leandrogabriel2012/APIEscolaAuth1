using APIEscolaAuth1.Models;

namespace APIEscolaAuth1.Repositories.Interfaces;

public interface IRepository<T>
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T?> GetAsync(int id);
    T Create(T entity);
    T Update(T entity);
    T Delete(T entity);
}
