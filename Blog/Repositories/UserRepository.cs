using Blog.Models;
using Dapper.Contrib.Extensions;
using Microsoft.Data.SqlClient;

namespace Blog.Repositories
{
    public class UserRepository
    {
        // readonly => não pode ser mais alterada
        // A readonly field cannot be assigned to (except in a constructor or init-only setter of the type in which the field is defined or a variable initializer)
        private readonly SqlConnection _connection;

        public UserRepository(SqlConnection connection)
            => _connection = connection;

        public IEnumerable<User> Get()
        // GetAll => SELECT * FROM
            => _connection.GetAll<User>();

        public User Get(int id)
            => _connection.Get<User>(id);

        public void Create(User user)
            => _connection.Insert<User>(user);
    }
}
