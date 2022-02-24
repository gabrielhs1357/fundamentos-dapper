// using Blog.Models;
using Dapper.Contrib.Extensions;
using Microsoft.Data.SqlClient;

namespace Blog.Repositories
{
    // Classe genérica que aceita apenas outras classes (que serão as Models)
    public class Repository<T> where T : class
    {
        private readonly SqlConnection _connection;

        public Repository(SqlConnection connection)
        {
            _connection = connection;
        }

        public IEnumerable<T> Get()
            // GetAll => SELECT * FROM
            => _connection.GetAll<T>();

        public T Get(int id)
            => _connection.Get<T>(id);

        public void Create(T model)
            => _connection.Insert<T>(model);

        public void Update(T model)
            => _connection.Update<T>(model);

        public void Delete(T model)
            => _connection.Delete<T>(model);

        public void Delete(int id)
        {
            var model = Get(id);

            _connection.Delete<T>(model);
        }
    }
}
