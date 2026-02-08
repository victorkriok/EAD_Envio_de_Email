using Código.Data;
using Código.Model;

public class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("=== TESTE DE LOGIN ===\n");
        
        Usuario usuario = new Usuario
        {
            Nome = "Victor",
            Email = "vkriok@gmail.com",
            Senha = "12345",
            DataNascimento = new DateTime(2000, 1, 1)
        };

        TelaLogin tela = new TelaLogin(usuario);
        
        Console.WriteLine("Tentativa 1:");
        await tela.LoginAsync("errado", new DateTime(2000, 1, 1));
        
        Console.WriteLine("\nTentativa 2:");
        await tela.LoginAsync("errado", new DateTime(2000, 1, 1));
        
        Console.WriteLine("\nTentativa 3:");
        await tela.LoginAsync("errado", new DateTime(2000, 1, 1));
        
        Console.WriteLine("\nTentativa 4:");
        await tela.LoginAsync("12345", new DateTime(2000, 1, 1));
        
        Console.WriteLine("\n=== FIM ===");
        Console.ReadKey();
    }
}
