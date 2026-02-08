using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace Código.Data
{
    public class EmailService
    {
        private readonly string _smtpServer;
        private readonly int _smtpPort;
        private readonly string _emailRemetente;
        private readonly string _senhaRemetente;
        private readonly string _nomeRemetente;

        public EmailService(string smtpServer, int smtpPort, string emailRemetente, 
                          string senhaRemetente, string nomeRemetente)
        {
            _smtpServer = smtpServer;
            _smtpPort = smtpPort;
            _emailRemetente = emailRemetente;
            _senhaRemetente = senhaRemetente;
            _nomeRemetente = nomeRemetente;
        }

        public EmailService(string emailRemetente, string senhaRemetente)
            : this("smtp.gmail.com", 587, emailRemetente, senhaRemetente, "Sistema de Login")
        {
        }

        public async Task<bool> EnviarEmailRecuperacaoAsync(string emailDestinatario, string nomeUsuario)
        {
            try
            {
                var mensagem = new MimeMessage();
                mensagem.From.Add(new MailboxAddress(_nomeRemetente, _emailRemetente));
                mensagem.To.Add(new MailboxAddress(nomeUsuario, emailDestinatario));
                mensagem.Subject = "Recuperação de Senha";
                
                var builder = new BodyBuilder();
                builder.TextBody = $"Olá {nomeUsuario},\n\nPara recuperar sua senha, clique no link: https://recuperar.com";
                
                mensagem.Body = builder.ToMessageBody();
                
                using (var cliente = new SmtpClient())
                {
                    await cliente.ConnectAsync(_smtpServer, _smtpPort, SecureSocketOptions.StartTls);
                    await cliente.AuthenticateAsync(_emailRemetente, _senhaRemetente);
                    await cliente.SendAsync(mensagem);
                    await cliente.DisconnectAsync(true);
                }
                
                Console.WriteLine($"✅ Email enviado para {emailDestinatario}");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Erro: {ex.Message}");
                return false;
            }
        }
    }
}