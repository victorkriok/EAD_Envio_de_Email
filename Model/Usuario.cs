using System;

namespace Código.Model
{
    public class Usuario
    {
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Senha { get; set; } = String.Empty;
        public DateTime DataNascimento { get; set; } = DateTime.MinValue;

        public Usuario() { }

        // Método para verificar se o login é válido
        public bool VerificarLogin(string senha)
        {
            return this.Senha == senha;
        }
    }
}