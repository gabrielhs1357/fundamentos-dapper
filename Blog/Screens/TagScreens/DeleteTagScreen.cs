using Blog.Models;
using Blog.Repositories;

namespace Blog.Screens.TagScreens
{
    public static class DeleteTagScreen
    {
        public static void Load()
        {
            Console.Clear();
            Console.WriteLine("Atualizando uma tag");
            Console.WriteLine("-------------------");

            Console.Write("Id: ");
            var id = Console.ReadLine();

            Delete(int.Parse(id));

            Console.ReadKey();
            MenuTagScreen.Load();
        }

        private static void Delete(int id)
        {
            try
            {
                var repository = new Repository<Tag>(Database.Connection);

                repository.Delete(id);

                Console.WriteLine("Tag excluída com sucesso");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Falha ao excluir a tag.");
                Console.WriteLine(ex.Message);
            }
        }
    }
}
