using Código.Model;

namespace Código.Data
{
    public class TelaLogin
    {
        private Usuario usuario;
        private int tentativas = 0;
        private EmailService emailService;
        private bool contaBloqueada = false;

        public TelaLogin(Usuario usuario)
        {
            this.usuario = usuario;
            
            this.emailService = new EmailService(
                smtpServer: "smtp.gmail.com",      // Servidor SMTP
                smtpPort: 587,                     // Porta (587 para TLS)
                emailRemetente: "dantzinff@gmail.com", 
                senhaRemetente: "fuke qjco hwda fshd",     
                nomeRemetente: "Sistema de Login"     
            );
            
        }

        public async Task LoginAsync(string senha, DateTime dataNascimento)
        {
            if (contaBloqueada)
            {
                Console.WriteLine("🚫 Conta bloqueada. Use a recuperação de senha.");
                return;
            }

            if (usuario.VerificarLogin(senha) && usuario.DataNascimento.Date == dataNascimento.Date)
            {
                Console.WriteLine($"✅ Login bem-sucedido! Bem-vindo, {usuario.Nome}!");
                tentativas = 0; // Reseta tentativas
            }
            else
            {
                tentativas++;
                Console.WriteLine($"❌ Credenciais incorretas. Tentativa {tentativas}/3");
                
                if (tentativas >= 3)
                {
                    Console.WriteLine("\n⚠️ MÁXIMO DE TENTATIVAS ATINGIDO!");
                    await RecuperacaoDeAcessoAsync();
                }
            }
        }

        public async Task RecuperacaoDeAcessoAsync()
        {
            contaBloqueada = true;
            
            Console.WriteLine($"\n🔐 PROCESSO DE RECUPERAÇÃO DE SENHA");
            Console.WriteLine(new string('=', 50));
            Console.WriteLine($"👤 Usuário: {usuario.Nome}");
            Console.WriteLine($"📧 Email: {usuario.Email}");
            Console.WriteLine(new string('=', 50));
            
            Console.WriteLine("\n📨 Preparando envio do email de recuperação...");
            
            // Enviar email REAL
            bool emailEnviado = await emailService.EnviarEmailRecuperacaoAsync(
                usuario.Email, 
                usuario.Nome
            );
            
            if (emailEnviado)
            {
                Console.WriteLine("\n✅ Email enviado com sucesso!");
                Console.WriteLine("\n📋 PRÓXIMOS PASSOS:");
                Console.WriteLine("1. Verifique sua caixa de entrada (e pasta de SPAM)");
                Console.WriteLine("2. Clique no link de recuperação no email");
                Console.WriteLine("3. Siga as instruções para criar uma nova senha");
                Console.WriteLine("4. Volte ao sistema e faça login com a nova senha");
                
                // Simula desbloqueio após recuperação
                Console.WriteLine("\n⏳ Aguardando confirmação de recuperação...");
                await Task.Delay(3000); // Aguarda 3 segundos (simulação)
                contaBloqueada = false;
                tentativas = 0;
                Console.WriteLine("🔄 Conta desbloqueada. Tente fazer login novamente.");
            }
            else
            {
                Console.WriteLine("\n❌ Falha no envio do email.");
                Console.WriteLine("Entre em contato com o suporte técnico.");
            }
        }
        
        // Método para teste direto
        public async Task TestarEnvioEmailAsync()
        {
            Console.WriteLine("\n🧪 TESTE DIRETO DE ENVIO DE EMAIL");
            Console.WriteLine(new string('=', 50));
            await RecuperacaoDeAcessoAsync();
        }
    }
}
