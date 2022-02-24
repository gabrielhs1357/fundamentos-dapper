using Dapper.Contrib.Extensions;

namespace Blog.Models
{
    // Notação do Dapper.Contrib.Extensions;
    // Sem usar essa notação, o Dapper automaticamente tenta executar os comandos colocando o nome das classes / tabelas no plural
    [Table("[User]")]
    public class User
    {
        public User()
            => Roles = new List<Role>();

        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Bio { get; set; }
        public string Image { get; set; }
        public string Slug { get; set; }
        // Indica que as Roles não serão criadas a partir da criação do User
        [Write(false)]
        public List<Role> Roles { get; set; }
    }
}
