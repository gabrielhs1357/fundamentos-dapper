using Blog;
using Blog.Screens.TagScreens;
using Microsoft.Data.SqlClient;

const string CONNECTION_STRING = @"Server=localhost,1433;Database=Blog;User ID=sa;Password=1q2w3e4r@#$; TrustServerCertificate=True";

Database.Connection = new SqlConnection(CONNECTION_STRING);
Database.Connection.Open();

Load();

Console.ReadKey();

Database.Connection.Close();

void Load()
{
    Console.Clear();
    Console.WriteLine("Meu Blog");
    Console.WriteLine("---------");
    Console.WriteLine("O que deseja fazer?\n\n");
    Console.WriteLine("1 - Gestão de usuário");
    Console.WriteLine("2 - Gestão de perfil");
    Console.WriteLine("3 - Gestão de categoria");
    Console.WriteLine("4 - Gestão de tag");
    Console.WriteLine("5 - Gestão de perfil/usuário");
    Console.WriteLine("6 - Gestão de post/tag");
    Console.WriteLine("7 - Relatórios\n\n");

    var option = short.Parse(Console.ReadLine());

    switch (option)
    {
        case 4:
            MenuTagScreen.Load();
            break;
        default: Load(); break;
    }
}
