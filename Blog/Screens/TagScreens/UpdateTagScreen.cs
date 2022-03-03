using Blog.Models;
using Blog.Repositories;

namespace Blog.Screens.TagScreens
{
    public static class UpdateTagScreen
    {
        public static void Load()
        {
            Console.Clear();
            Console.WriteLine("Atualizando uma tag");
            Console.WriteLine("-------------------");

            Console.Write("Id: ");
            var id = Console.ReadLine();
            Console.Write("Nome: ");
            var name = Console.ReadLine();
            Console.Write("Slug: ");
            var slug = Console.ReadLine();

            Update(new Tag() { Id = int.Parse(id), Name = name, Slug = slug });

            Console.ReadKey();
            MenuTagScreen.Load();
        }

        private static void Update(Tag tag)
        {
            try
            {
                var repository = new Repository<Tag>(Database.Connection);

                repository.Update(tag);

                Console.WriteLine("Tag atualizada com sucesso");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Falha ao atualizar a tag.");
                Console.WriteLine(ex.Message);
            }
        }
    }
}
