using System;
namespace greenhouse_app.Interfaces
{
    public interface IRepository<T> : IDisposable
    where T : class
    {
        IEnumerable<T> GetLoadedProgramList(); // получение всех объектов
        T GetLoadedProgram(int id); // получение одного объекта по id
        void Create(T item); // создание объекта
        void Update(T item); // обновление объекта
        void Delete(int id); // удаление объекта по id
        void Save();  // сохранение изменений
    }
}

